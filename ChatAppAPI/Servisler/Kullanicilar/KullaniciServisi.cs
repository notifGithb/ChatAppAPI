using AutoMapper;
using ChatAppAPI.Context;
using ChatAppAPI.Servisler.Kullanicilar.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ChatAppAPI.Servisler.Kullanicilar
{
    public class KullaniciServisi : IKullaniciServisi
    {
        private readonly ChatAppDbContext _context;
        private readonly IMapper _mapper;

        public KullaniciServisi(ChatAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<KullaniciGetirDTO> MevcutKullaniciGetir(string kullaniciId)
        {
            return _mapper.Map<KullaniciGetirDTO>(await _context.Kullanicis.FindAsync(kullaniciId));
        }

        public async Task<IEnumerable<KullaniciGetirDTO>> TumDigerKullanicilariGetir(string kullaniciId)
        {
            return _mapper.Map<IEnumerable<KullaniciGetirDTO>>(await _context.Kullanicis.Where(k => k.Id != kullaniciId).ToListAsync());
        }
    }
}
