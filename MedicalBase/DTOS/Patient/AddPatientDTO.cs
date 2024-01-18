using System.ComponentModel.DataAnnotations;

namespace MedicalBase.DTOS.Patient
{
    public class AddPatientDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Pesel { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
