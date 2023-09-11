namespace JuniorWeb.Api.Errors
{


    public interface IExceptionLogger
    {
        string LogError(Exception ex);
    }

    public class ConsoleExceptionLogger : IExceptionLogger
    {
        public string LogError(Exception ex)
        {
            var guid = Guid.NewGuid().ToString();
            Console.WriteLine(DateTime.UtcNow + " Error ID: " + guid + " Message: " + ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine(ex.InnerException != null ? ex.InnerException : "");
            return guid;
        }
    }

}

