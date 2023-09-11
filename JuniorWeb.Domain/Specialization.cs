namespace JuniorWeb.Domain
{
    public class Specialization : Entity
    {
        public string? Name { get; set; }

        public virtual ICollection<Doctor>? Doctors { get; set; } = new List<Doctor>();
    }
}
