namespace ChatAppAPI.Servisler.Kullanicilar.DTOs
{
    public class KullaniciGetirDTO
    {
        public required string KullaniciAdi { get; set; }
        public required string Isim { get; set; }
        public required string Soyisim { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
