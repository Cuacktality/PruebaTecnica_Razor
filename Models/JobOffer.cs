using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Models {
    public class JobOffer {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty!;
        [Required] public string Description { get; set; } = string.Empty!;
        [Required, DataType(DataType.Date)] public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required] public JobMode Mode { get; set; } = JobMode.None;
        [Required] public Payment Payment { get; set; } = Payment.None;
        [Required] public string Salary { get; set; } = string.Empty!;
        public int EmployerId { get; set; }
        [ForeignKey("EmployerId")] public virtual EmployerProfile? Employer { get; set; }
    }
}