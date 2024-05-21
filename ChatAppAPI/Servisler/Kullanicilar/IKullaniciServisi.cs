using ChatAppAPI.Servisler.Kullanicilar.DTOs;

namespace ChatAppAPI.Servisler.Kullanicilar
{
    public interface IKullaniciServisi
    {
        string? MevcutKullaniciAdi { get; }
        KullaniciGetirDTO KullaniciGetir(string kullaniciAdi);
        Task<KullaniciGetirDTO> MevcutKullaniciGetir();
        Task<IEnumerable<KullaniciGetirDTO>> TumDigerKullanicilariGetir();
    }
}
