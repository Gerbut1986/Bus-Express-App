namespace BusExpress.PL.Models.ADO
{
    using Models;
    using System.Configuration;
    using System.Data.SqlClient;

    public class ServiceOrderInfos
    {
        readonly string addQuery, updQuery, delQuery;
        SqlConnection conn;
        SqlCommand cmd;

        public ServiceOrderInfos()
        {
            addQuery =
                "insert into OrderInfos([From],[To],LName_FName,PlaceNumber,Phone,OrderNumber,MoneyAmount)" +
                " values(@From,@To,@LName_FName,@PlaceNumber,@Phone,@OrderNumber,@MoneyAmount)";
            updQuery = "update OrderInfos set [From]=@From,[To]=@To,LName_FName=@LName_FName,PlaceNumber=@PlaceNumber,Phone=@Phone,OrderNumber=@OrderNumber,MoneyAmount=@MoneyAmount " +
                "where Id=@Id";
            delQuery = "delete from OrderInfos where Id=@Id";
        }

        public string Create(OrderInfo model)
        {
            using (conn = new
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(addQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@From", model.From);
                    cmd.Parameters.AddWithValue("@To", model.To);
                    cmd.Parameters.AddWithValue("@LName_FName", model.LName_FName);
                    cmd.Parameters.AddWithValue("@PlaceNumber", model.PlaceNumber);
                    cmd.Parameters.AddWithValue("@Phone", model.Phone);
                    cmd.Parameters.AddWithValue("@OrderNumber", model.OrderNumber);
                    cmd.Parameters.AddWithValue("@MoneyAmount", model.MoneyAmount);
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }

        public string Update(OrderInfo model)
        {
            using (conn = new
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(updQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@From", model.From);
                    cmd.Parameters.AddWithValue("@To", model.To);
                    cmd.Parameters.AddWithValue("@LName_FName", model.LName_FName);
                    cmd.Parameters.AddWithValue("@PlaceNumber", model.PlaceNumber);
                    cmd.Parameters.AddWithValue("@Phone", model.Phone);
                    cmd.Parameters.AddWithValue("@OrderNumber", model.OrderNumber);
                    cmd.Parameters.AddWithValue("@MoneyAmount", model.MoneyAmount);
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
