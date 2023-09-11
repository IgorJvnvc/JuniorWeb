namespace JuniorWeb.Domain
{
    public class Diagnosis : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DiagnosedAt { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    }
}
