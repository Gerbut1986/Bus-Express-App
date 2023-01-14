namespace Transfer_App.Models.ADO
{
    using Models;
    using System.Configuration;
    using System.Data.SqlClient;

    public class ServiceDestinations
    {
        readonly string addQuery, updQuery, delQuery;
        SqlConnection conn;
        SqlCommand cmd;

        public ServiceDestinations()
        {
            addQuery =
                "insert into Destinations(Name)" +
                " values(@Name)";
            updQuery = "update Destinations set Name=@Name where Id=@Id";
            delQuery = "delete from Destinations where Id=@Id";
        }

        public string Create(Destination model)
        {
            using (conn = new
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(addQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }

        public string Update(Destination model)
        {
            using (conn = new
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(updQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }

        public string Delete(int id)
        {
            using (conn = new
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(delQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }
    }
}
