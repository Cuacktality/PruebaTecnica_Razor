using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Models {
    public class EmployerProfile {
        [Key] public int Id { get; set; }

        [Required] public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [Required] public string Location { get; set; } = null!;
        [Required] public string Industry { get; set; } = null!;
        [Required] public int EmployeeCount { get; set; } = 0;

        public virtual ICollection<JobOffer>? JobOffers { get; set; }
    }
}