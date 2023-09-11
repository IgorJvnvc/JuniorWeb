using JuniorWeb.Domain;

namespace JuniorWeb.Api.DTO
{
    public class UpdatePatientDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int RoomId { get; set; }
        public int DoctorId { get; set; }
        public IEnumerable<Diagnosis>? Diagnoses { get; set; }
    }

    public class UpdateDiagnosis
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
