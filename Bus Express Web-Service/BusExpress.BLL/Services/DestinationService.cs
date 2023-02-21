namespace BusExpress.BLL.Services
{
    using BusExpress.BLL.Dto;
    using System.Data.SqlClient;
    using BusExpress.BLL.Interfaces;

    public class DestinationService : IADOService
    {
        readonly string addQuery, updQuery, delQuery;
        SqlConnection conn;
        SqlCommand cmd;

        public DestinationService()
        {
            addQuery =
                "insert into Destinations(Name)" +
                " values(@Name)";
            updQuery = "update Destinations set Name=@Name " +
                "where Id=@Id";
            delQuery = "delete from Destinations where Id=@Id";
        }

        public string Create(IModel entity, string connStr)
        {
            var model = entity as DestinationDto;
            if (entity == null) return $"Not pass compatible model...You should to pass {nameof(DestinationDto)} model.";
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (cmd = new SqlCommand(addQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }

        public string Update(IModel entity, string connStr)
        {
            var model = entity as DestinationDto;
            if (entity == null) return $"Not pass compatible model...You should to pass {nameof(DestinationDto)} model.";
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (cmd = new SqlCommand(updQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }

        public string Delete(int id, string connStr)
        {
            using (conn = new SqlConnection(connStr))
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
