namespace JuniorWeb.Domain
{
    public class Hospital : Entity
    {
        public string? Location { get; set; }
        public string? Name { get; set; }
        public bool Covid19 { get; set; }
        public int HospitalTypeId { get; set; }

        public virtual HospitalType HospitalType { get; set; } = null!;
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
