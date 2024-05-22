using ChatAppAPI.Servisler.Kullanicilar.DTOs;

namespace ChatAppAPI.Servisler.Kullanicilar
{
    public interface IKullaniciServisi
    {
        string? MevcutKullaniciAdi { get; }
        Task<KullaniciGetirDTO> KullaniciGetir(string kullaniciAdi);
        Task<KullaniciGetirDTO> MevcutKullaniciGetir(CancellationToken cancellationToken);
        Task<IEnumerable<KullaniciGetirDTO>> TumDigerKullanicilariGetir(CancellationToken cancellationToken);
    }
}
