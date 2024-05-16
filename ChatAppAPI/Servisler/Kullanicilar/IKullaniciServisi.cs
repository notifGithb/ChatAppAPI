using ChatAppAPI.Servisler.Kullanicilar.DTOs;

namespace ChatAppAPI.Servisler.Kullanicilar
{
    public interface IKullaniciServisi
    {
        Task<KullaniciGetirDTO> MevcutKullaniciGetir(string kullaniciId);
        Task<IEnumerable<KullaniciGetirDTO>> TumDigerKullanicilariGetir(string kullaniciId);
    }
}
