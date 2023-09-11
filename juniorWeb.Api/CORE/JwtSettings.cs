namespace JuniorWeb.Api.CORE
{
    public class JwtSettings
    {
        public int Minutes { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }

    }
}
