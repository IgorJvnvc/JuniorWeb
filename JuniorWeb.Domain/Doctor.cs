namespace JuniorWeb.Domain
{
    public class Doctor : Entity
    {
        public string? FristName { get; set; }
        public string? LastName { get; set; }


        public virtual Specialization Specialization { get; set; } = null!;
        public virtual ICollection<Diagnosis> Diagnosiss { get; set; } = new List<Diagnosis>();
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
