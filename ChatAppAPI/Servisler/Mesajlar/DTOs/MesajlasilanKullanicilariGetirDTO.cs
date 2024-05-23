namespace ChatAppAPI.Servisler.Mesajlar.DTOs
{
    public class MesajlasilanKullanicilariGetirDTO
    {
        public required string KullaniciAdi { get; set; }
        public string? ProfilResmiUrl { get; set; }
        public string? SonGonderilenMesaj { get; set; }
        public DateTime SonGonderilenMesajTarihi { get; set; }
        public required string SonMesajGonderenAdi { get; set; }
        public int GorulmeyenMesajSayisi { get; set; } = 0;

    }
}
