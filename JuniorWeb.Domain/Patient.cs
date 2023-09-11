namespace JuniorWeb.Domain
{
    public class Patient : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int JMBG { get; set; }
        public int HospitalId { get; set; }
        public int RoomId { get; set; }
        public int DoctorId { get; set; }

        public virtual Hospital Hospital { get; set; } = null!;
        public virtual ICollection<Diagnosis> Diagnosis { get; set; } = new List<Diagnosis>();
        public virtual Room Room { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;

    }
}
