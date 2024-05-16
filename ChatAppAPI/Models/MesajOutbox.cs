using System.ComponentModel.DataAnnotations;

namespace ChatAppAPI.Models
{
    public class MesajOutbox
    {
        [Key]
        public int Id { get; set; }
        public required string GonderenId { get; set; }
        public required string AliciId { get; set; }
        public int MesajId { get; set; }
        public Mesaj Mesaj { get; set; } = null!;
    }
}
