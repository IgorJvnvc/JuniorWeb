namespace JuniorWeb.Domain
{
    public class HospitalType : Entity
    {
        public bool Private { get; set; }

        public virtual Hospital? Hospital { get; set; }
    }
}
