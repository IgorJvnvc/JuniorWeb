namespace JuniorWeb.Api.DTO
{
    public class AddDiagnosisDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DiagnosedAt { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
