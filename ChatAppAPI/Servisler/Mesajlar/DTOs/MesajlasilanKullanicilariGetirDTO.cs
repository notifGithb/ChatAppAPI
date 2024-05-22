namespace ChatAppAPI.Servisler.Mesajlar.DTOs
{
    public class MesajlasilanKullanicilariGetirDTO
    {
        public required string KullaniciAdi { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int GorulmeyenMesajSayisi { get; set; } = 0;
        public string? SonGonderilenMesaj { get; set; }
        public DateTime SonGonderilenMesajTarihi { get; set; }

    }
}
