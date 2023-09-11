namespace JuniorWeb.Api.DTO
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IEnumerable<DiagnosesDto>? Diagnoses { get; set; }
    }

    public class DiagnosesDto
    {
        public string? Name { get; set; }
    }

}
