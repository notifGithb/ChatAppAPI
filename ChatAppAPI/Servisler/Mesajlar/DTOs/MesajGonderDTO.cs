namespace ChatAppAPI.Servisler.Mesajlar.DTOs
{
    public class MesajGonderDTO
    {
        public string Text { get; set; }
        public DateTime GonderilmeZamani { get; set; }
        public string GonderenId { get; set; }
        public string AliciId { get; set; }
        public bool GonderilmeDurumu { get; set; } = false;
    }
}
