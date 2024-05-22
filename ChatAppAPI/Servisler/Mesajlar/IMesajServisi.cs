using ChatAppAPI.Models;
using ChatAppAPI.Servisler.Mesajlar.DTOs;

namespace ChatAppAPI.Servisler.Mesajlar
{
    public interface IMesajServisi
    {
        Task MesajEkle(MesajGonderDTO messageDto);
        Task<IEnumerable<MesajGetirDTO>> MesajlariGetir(string aliciKullaniciAdi, CancellationToken cancellationToken);
        Task MesajlariGorulduYap(IEnumerable<Mesaj> mesajlar, CancellationToken cancellationToken);
        Task<IEnumerable<MesajlasilanKullanicilariGetirDTO>> MesajlasilanKullanicilariGetir(CancellationToken cancellationToken);

    }
}
