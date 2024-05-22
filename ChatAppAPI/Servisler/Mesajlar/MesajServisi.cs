using AutoMapper;
using ChatAppAPI.Context;
using ChatAppAPI.Models;
using ChatAppAPI.Servisler.Kullanicilar;
using ChatAppAPI.Servisler.Mesajlar.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ChatAppAPI.Servisler.Mesajlar
{
    public class MesajServisi(ChatAppDbContext context, IMapper mapper, IKullaniciServisi kullaniciServisi) : IMesajServisi
    {
        public async Task MesajEkle(MesajGonderDTO messageDto)
        {
            Kullanici? alici = await context.Kullanicis
                .Where(k => k.KullaniciAdi == messageDto.AliciAdi)
                .AsNoTracking()
                .FirstOrDefaultAsync() ?? throw new Exception("Alıcı Bulunamadı");

            Kullanici? gönderici = await context.Kullanicis
                .Where(k => k.KullaniciAdi == kullaniciServisi.MevcutKullaniciAdi)
                .AsNoTracking()
                .FirstOrDefaultAsync() ?? throw new Exception("Gönderici Bulunamadı.");

            Mesaj mesaj = new()
            {
                Text = messageDto.Text,
                GonderilmeZamani = messageDto.GonderilmeZamani,
                GonderenId = gönderici.Id,
                AliciId = alici.Id
            };

            await context.Mesajs.AddAsync(mesaj);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MesajGetirDTO>> MesajlariGetir(string aliciKullaniciAdi, CancellationToken cancellationToken)
        {
            string? gonderenKullaniciAdi = kullaniciServisi.MevcutKullaniciAdi ?? throw new Exception("Kullanıcı Bulunamadı");

            if (!await context.Kullanicis.AnyAsync(k => k.KullaniciAdi == aliciKullaniciAdi, cancellationToken)) throw new Exception("Alıcı Kullanıcı Bulunamadı");

            IEnumerable<Mesaj> mesajlar = await context.Mesajs
                .Include(m => m.Gonderen)
                .Include(m => m.Alici)
                .Where(m =>
                    m.Gonderen.KullaniciAdi == gonderenKullaniciAdi &&
                    m.Alici.KullaniciAdi == aliciKullaniciAdi ||
                    m.Gonderen.KullaniciAdi == aliciKullaniciAdi &&
                    m.Alici.KullaniciAdi == gonderenKullaniciAdi)
                .OrderBy(m => m.GonderilmeZamani)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!mesajlar.Any()) throw new Exception("Mesaj Bulunamadı");

            await MesajlariGorulduYap(mesajlar, cancellationToken);

            return mapper.Map<IEnumerable<MesajGetirDTO>>(mesajlar);
        }

        public async Task MesajlariGorulduYap(IEnumerable<Mesaj> mesajlar, CancellationToken cancellationToken)
        {
            foreach (var mesaj in mesajlar)
            {
                mesaj.GorulmeDurumu = true;
            }
            context.Mesajs.UpdateRange(mesajlar);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<MesajlasilanKullanicilariGetirDTO>> MesajlasilanKullanicilariGetir(CancellationToken cancellationToken)
        {
            Kullanici? kullanici = await context.Kullanicis
                .Where(k => k.KullaniciAdi == kullaniciServisi.MevcutKullaniciAdi)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Kullanıcı Bulunamadı");

            var mesajGonderilenKullanicilar = await context.Mesajs.Include(m => m.Gonderen).Include(m => m.Alici)
                .Where(m => m.GonderenId == kullanici.Id || m.AliciId == kullanici.Id)
                .Select(m => m.Alici)
                .Distinct()
                .Select(m => new MesajlasilanKullanicilariGetirDTO
                {
                    KullaniciAdi = m.KullaniciAdi,
                    ProfileImageUrl = m.ProfileImageUrl,

                    SonGonderilenMesaj = m.AlinanMesajlar
                    .Where(msg => msg.GonderenId == kullanici.Id || msg.AliciId == kullanici.Id)
                    .OrderByDescending(msg => msg.GonderilmeZamani)
                    .Select(msg => msg.Text)
                    .FirstOrDefault(),

                    SonGonderilenMesajTarihi = m.AlinanMesajlar
                    .Where(msg => msg.GonderenId == kullanici.Id || msg.AliciId == kullanici.Id)
                    .OrderByDescending(msg => msg.GonderilmeZamani)
                    .Select(msg => msg.GonderilmeZamani)
                    .FirstOrDefault(),

                    GorulmeyenMesajSayisi = m.AlinanMesajlar
                    .Count(msg => !msg.GorulmeDurumu && msg.AliciId == kullanici.Id),
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!mesajGonderilenKullanicilar.Any()) throw new Exception("Kullanıcı Bulunamadı");

            return mapper.Map<IEnumerable<MesajlasilanKullanicilariGetirDTO>>(mesajGonderilenKullanicilar);
        }
    }
}
