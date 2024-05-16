using AutoMapper;
using ChatAppAPI.Context;
using ChatAppAPI.Models;
using ChatAppAPI.Servisler.OturumYonetimi.DTOs;
using ChatAppAPI.Servisler.OturumYonetimi.JWT;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ChatAppAPI.Servisler.OturumYonetimi
{
    public class OturumYonetimi : IOturumYonetimi
    {
        private readonly IMapper _mapper;
        private readonly IJwtServisi _jwtServisi;
        private readonly ChatAppDbContext _context;

        public OturumYonetimi(IMapper mapper, IJwtServisi jwtServisi, ChatAppDbContext context)
        {
            _mapper = mapper;
            _jwtServisi = jwtServisi;
            _context = context;
        }

        public async Task<string?> GirisYap(KullaniciGirisDto model)
        {
            try
            {
                var byteArray = Encoding.Default.GetBytes(model.KullaniciSifresi);
                var hashedSifre = Convert.ToBase64String(SHA256.HashData(byteArray));

                model.KullaniciSifresi = hashedSifre;
                model.KullaniciAdi = model.KullaniciAdi.Trim().ToLower();

                Kullanici? kullanici = await _context.Kullanicis.Where(k => k.KullaniciAdi == model.KullaniciAdi && k.KullaniciSifresi == model.KullaniciSifresi).FirstOrDefaultAsync();
                if (kullanici == null)
                {
                    return null;
                }

                string token = _jwtServisi.JwtTokenOlustur(kullanici);

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task KayitOl(KullaniciKayitDto model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "Model boş olamaz.");
                }

                var mevcutKullanici = await _context.Kullanicis.Where(k => k.KullaniciAdi == model.KullaniciAdi).FirstOrDefaultAsync();

                if (mevcutKullanici != null)
                {
                    throw new ArgumentException("Bu kullanıcı adı mevcut.", model.KullaniciAdi);
                }
                model.KullaniciAdi = model.KullaniciAdi.Trim().ToLower();
                Kullanici yeniKullanici = _mapper.Map<Kullanici>(model);
                yeniKullanici.Id = Guid.NewGuid().ToString();
                var byteArray = Encoding.Default.GetBytes(yeniKullanici.KullaniciSifresi);
                var hashedSifre = Convert.ToBase64String(SHA256.HashData(byteArray));

                yeniKullanici.KullaniciSifresi = hashedSifre;

                await _context.Kullanicis.AddAsync(yeniKullanici);
                await _context.SaveChangesAsync();

            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }
    }
}
