using ChatAppAPI.Servisler.Mesajlar.DTOs;

namespace ChatAppAPI.Servisler.Mesajlar
{
    public interface IMesajServisi
    {
        Task MesajEkle(MesajGonderDTO messageDto);
        Task<List<MesajlariGetirDTO>> MesajlarıGetir(KullaniciMesajlariGetirDTO getUserMessageDto);
        Task<bool> MesajlariGorulduYap(MesajlariGorulduYapDTO setUserMessages);
        Task<List<object>> MesajlasilanKullanicilariGetir(string mevcutKullaniciId);
        Task<List<GonderilememisMesajlariGetirDTO>> GonderilemeyenMesajlariGetir(string aliciId);
        Task MesajiGonderildiYap(int messageId);
    }
}
