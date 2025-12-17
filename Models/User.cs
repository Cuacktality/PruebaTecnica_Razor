using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models {
    public class User {
        [Key] public int Id { get; set; }
        [Required, StringLength(100)] public string FullName { get; set; } = string.Empty!;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty!;
        [Required, DataType(DataType.Password)] public string Pass { get; set; } = string.Empty!;
        [Required] public MyRole URole { get; set; } = MyRole.None;

        public virtual SeekerProfile? SeekerProfile { get; set; }
        public virtual EmployerProfile? EmployerProfile { get; set; }

    }
}
