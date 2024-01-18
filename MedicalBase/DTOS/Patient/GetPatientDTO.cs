namespace MedicalBase.DTOS.Patient
{
    public class GetPatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "Michał";
        public string LastName { get; set; } = "Misiek";
        public string Pesel { get; set; } = "12345678901";
        public string PhoneNumber { get; set; } = "123456789";
    }
}
