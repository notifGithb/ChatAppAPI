namespace ChatAppAPI.Servisler.Mesajlar.DTOs
{
    public class KullaniciMesajlariGetirDTO
    {
        public required string GonderenKullaniciAdi { get; set; }

        public required string AliciKullaniciAdi { get; set; }
    }
}
