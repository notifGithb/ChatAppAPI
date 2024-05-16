using AutoMapper;
using ChatAppAPI.Models;
using ChatAppAPI.Servisler.Kullanicilar.DTOs;
using ChatAppAPI.Servisler.Mesajlar.DTOs;
using ChatAppAPI.Servisler.OturumYonetimi.DTOs;

namespace ChatAppAPI.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Mesaj, MesajGonderDTO>().ReverseMap();
            CreateMap<Mesaj, MesajlariGetirDTO>()
                .ForMember(dest => dest.GondericiAdi, opt => opt.MapFrom(src => src.Gonderen.Isim))
                .ForMember(dest => dest.GondericiSoyadi, opt => opt.MapFrom(src => src.Gonderen.Soyisim))
                .ForMember(dest => dest.AliciAdi, opt => opt.MapFrom(src => src.Alici.Isim))
                .ForMember(dest => dest.AliciSoyadi, opt => opt.MapFrom(src => src.Alici.Soyisim))
                .ForMember(dest => dest.GonderilmeTarihi, opt => opt.MapFrom(src => src.GonderilmeZamani.ToShortDateString()))
                .ForMember(dest => dest.GonderilmeSaati, opt => opt.MapFrom(src => src.GonderilmeZamani.ToShortTimeString()));


            CreateMap<MesajOutbox, GonderilememisMesajlariGetirDTO>().ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Mesaj.Text));


            CreateMap<Kullanici, KullaniciGetirDTO>().ReverseMap();
            CreateMap<Kullanici, KullaniciGirisDto>().ReverseMap();
            CreateMap<Kullanici, KullaniciKayitDto>().ReverseMap();
        }
    }
}
