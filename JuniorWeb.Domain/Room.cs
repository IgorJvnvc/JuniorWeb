namespace JuniorWeb.Domain
{
    public class Room : Entity
    {
        public int Beds { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public string? ForWhatTypeOfPatient { get; set; }
        public bool IsFull { get; set; }
        public int HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; } = null!;
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
