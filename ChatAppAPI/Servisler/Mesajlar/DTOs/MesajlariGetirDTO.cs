namespace ChatAppAPI.Servisler.Mesajlar.DTOs
{
    public class MesajlariGetirDTO
    {
        public string GonderenId { get; set; }
        public string GondericiAdi { get; set; }
        public string GondericiSoyadi { get; set; }

        public string AliciAdi { get; set; }
        public string AliciSoyadi { get; set; }
        public string Text { get; set; }
        public string GonderilmeTarihi { get; set; }
        public string GonderilmeSaati{ get; set; }

    }
}
