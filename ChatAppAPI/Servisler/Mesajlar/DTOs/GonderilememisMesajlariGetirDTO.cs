namespace ChatAppAPI.Servisler.Mesajlar.DTOs
{
    public class GonderilememisMesajlariGetirDTO
    {
        public int Id { get; set; }
        public string GonderenId { get; set; }
        public string AliciId { get; set; }
        public int MesajId { get; set; }
        public string Text { get; set; }
    }
}
