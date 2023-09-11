using JuniorWeb.Domain;

namespace JuniorWeb.Api.CORE
{
    public class JwtUser : IApplicationUser
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
    public class AnonimousUser : IApplicationUser
    {
        public string? Name => "Anonymous";

        public string? Password => "1234";

        public string? Email => "anonymous@gmail.com";
    }
}
