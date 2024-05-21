﻿using AutoMapper;
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
                .ForMember(dest => dest.GonderilmeTarihi, opt => opt.MapFrom(src => src.GonderilmeZamani.ToShortDateString()))
                .ForMember(dest => dest.GonderilmeSaati, opt => opt.MapFrom(src => src.GonderilmeZamani.ToShortTimeString()));


            CreateMap<Kullanici, KullaniciGetirDTO>().ReverseMap();
            CreateMap<Kullanici, KullaniciGirisDto>().ReverseMap();
            CreateMap<Kullanici, KullaniciKayitDto>().ReverseMap();
        }
    }
}
