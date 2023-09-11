namespace JuniorWeb.Api.DTO
{
    public class CreatePatientDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int JMBG { get; set; }
        public int HospitalId { get; set; }
        public string? Dignosis { get; set; }
        public int RoomNumber { get; set; }
        public int DoctorId { get; set; }



    }
}
