using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Models {
    public class JobApplication {
        [Key] public int Id { get; set; }
        public int SeekerId { get; set; }
        [ForeignKey("SeekerId")]
        public virtual SeekerProfile Seeker { get; set; } = null!;
        public int JobOfferId { get; set; }
        [ForeignKey("JobOfferId")] public virtual JobOffer JobOffer { get; set; } = null!;

        [Required] public MyStatus Status { get; set; } = MyStatus.None;
        [DataType(DataType.Date)] public DateTime AppliedAt { get; set; } = DateTime.Now;
    }
}
