using AutoMapper;
using ChatAppAPI.Context;
using ChatAppAPI.Models;
using ChatAppAPI.Servisler.Mesajlar.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ChatAppAPI.Servisler.Mesajlar
{
    public class MesajServisi : IMesajServisi
    {
        private readonly ChatAppDbContext _context;
        private readonly IMapper _mapper;
        public MesajServisi(ChatAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task MesajEkle(MesajGonderDTO messageDto)
        {
            Mesaj mesaj = _mapper.Map<Mesaj>(messageDto) ?? throw new Exception();

            await _context.Mesajs.AddAsync(mesaj);
            await _context.SaveChangesAsync();
        }

        public async Task<List<object>> MesajlasilanKullanicilariGetir(string mevcutKullaniciId)
        {
            Kullanici? kullanici = await _context.Kullanicis.FindAsync(mevcutKullaniciId);

            if (kullanici == null) throw new Exception("Kullanici Bulunamadi");

            var messagingUsers = await _context.Mesajs.Where(m => m.GonderenId == kullanici.Id || m.AliciId == kullanici.Id)
                .Select(m => m.GonderenId == kullanici.Id ? m.AliciId : m.GonderenId)
                .Distinct()
                .Where(id => id != kullanici.Id)
                .Select(id => new
                {
                    User = _context.Kullanicis.Where(u => u.Id == id).Select(u => new
                    {
                        AliciId = u.Id,
                        ReceiverUserName = u.KullaniciAdi,
                        ReceiverProfileImageUrl = u.ProfileImageUrl
                    }).FirstOrDefault(),
                    LastMessage = _context.Mesajs.Where(msg => msg.GonderenId == kullanici.Id && msg.AliciId == id || msg.GonderenId == id && msg.AliciId == kullanici.Id)
                    .OrderByDescending(msg => msg.GonderilmeZamani)
                    .Select(msg => new
                    {
                        msg.GonderenId,
                        msg.Text,
                        msg.GonderilmeZamani
                    })
                    .FirstOrDefault(),
                    UnreadMessageCount = _context.Mesajs.Count(msg => msg.AliciId == kullanici.Id && msg.GonderenId == id && !msg.GorulmeDurumu)
                })
                .ToListAsync();

            return messagingUsers.Cast<object>().ToList();
        }

        public async Task<List<MesajlariGetirDTO>> MesajlarıGetir(KullaniciMesajlariGetirDTO getUserMessageDto)
        {
            var deneme = await _context.Mesajs
                .Include(m => m.Gonderen)
                .Include(m => m.Alici)
                .Where(m => m.Gonderen.KullaniciAdi == getUserMessageDto.GonderenKullaniciAdi && m.Alici.KullaniciAdi == getUserMessageDto.AliciKullaniciAdi ||
                m.Gonderen.KullaniciAdi == getUserMessageDto.AliciKullaniciAdi && m.Alici.KullaniciAdi == getUserMessageDto.GonderenKullaniciAdi)
                .OrderBy(m => m.GonderilmeZamani)
                .ToListAsync();

            return _mapper.Map<List<MesajlariGetirDTO>>(deneme);
        }

        //public async Task<List<GonderilememisMesajlariGetirDTO>> GonderilemeyenMesajlariGetir(string AliciId)
        //{
        //    var outboxMessages = await _context.MesajOutboxes.Where(m => m.AliciId == AliciId).ToListAsync();

        //    return _mapper.Map<List<GonderilememisMesajlariGetirDTO>>(outboxMessages);
        //}

        public async Task<bool> MesajlariGorulduYap(MesajlariGorulduYapDTO setUserMessages)
        {
            var userMessage = await _context.Mesajs
                .Where(m =>
                m.GonderenId == setUserMessages.GonderenId &&
                m.AliciId == setUserMessages.AliciId &&
                m.GorulmeDurumu == false)
                .ToListAsync();

            if (userMessage == null) return false;

            foreach (var user in userMessage)
            {
                user.GorulmeDurumu = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task MesajiGonderildiYap(int messageId)
        //{
        //    var message = await _context.Mesajs.FindAsync(messageId);
        //    var messageOutbox = await _context.MesajOutboxes.FindAsync(messageId);
        //    message.GonderilmeDurumu = true;
        //    _context.MesajOutboxes.Remove(messageOutbox);
        //    await _context.SaveChangesAsync();
        //}
    }
}
