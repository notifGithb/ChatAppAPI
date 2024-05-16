namespace ChatAppAPI.Servisler.Mesajlar.DTOs
{
    public class MesajlariGorulduYapDTO
    {
        public required string GonderenId { get; set; }
        public required string AliciId { get; set; }
    }
}
