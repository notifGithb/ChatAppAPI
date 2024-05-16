using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppAPI.Models
{
    public class Mesaj
    {
        public Mesaj()
        {
            MesajOutboxes = new HashSet<MesajOutbox>();
        }


        [Key]
        public int Id { get; set; }
        public required string Text { get; set; }
        public DateTime GonderilmeZamani { get; set; }

        public bool GorulmeDurumu { get; set; } = false;

        public bool GonderilmeDurumu { get; set; } = true;

        public required string GonderenId { get; set; }

        public Kullanici Gonderen { get; set; } = null!;

        public required string AliciId { get; set; }

        public Kullanici Alici { get; set; } = null!;

        public ICollection<MesajOutbox> MesajOutboxes { get; set; }

    }
}
