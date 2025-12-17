using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Models {
    public class SeekerProfile {
        [Key] public int Id { get; set; }

        [Required] public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }


        public string Age { get; set; } = string.Empty!;
        [Required, StringLength(15)] public string ContactNumber { get; set; } = string.Empty!;
        [Required] public EducationLevel ELevel { get; set; } = EducationLevel.None;
        [Required] public string WorkXP { get; set; } = string.Empty!;
        [Required] public string Notes { get; set; } = string.Empty!;
    }
}