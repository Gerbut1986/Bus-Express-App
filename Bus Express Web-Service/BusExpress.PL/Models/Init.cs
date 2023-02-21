namespace BusExpress.PL.Models
{
    using System.Configuration;
    public class Init
    {
        public static string GetConnectStr => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; 
    }

    public class FromToModel
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}