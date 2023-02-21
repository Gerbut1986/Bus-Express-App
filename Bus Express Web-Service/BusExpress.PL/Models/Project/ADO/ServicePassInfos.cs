namespace BusExpress.PL.Models.ADO
{
    using Models;
    using System.Configuration;
    using System.Data.SqlClient;

    public class ServicePassInfos
    {
        readonly string addQuery, updQuery, delQuery;
        SqlConnection conn;
        SqlCommand cmd;

        public ServicePassInfos() 
        {
            addQuery = 
                "insert into PassInfos(Booking_Date,Booking_Route,Qty,Tax,Total,Payment_Method,C_FName,C_LName,C_Phone,C_Email,C_Notes)" +
                " values(@Booking_Date,@Booking_Route,@Qty,@Tax,@Total,@Payment_Method,@C_FName,@C_LName,@C_Phone,@C_Email,@C_Notes)";
            updQuery = "update PassInfos set Booking_Date=@Booking_Date,Booking_Route=@Booking_Route,Qty=@Qty,Tax=@Tax,Total=@Total,Payment_Method=@Payment_Method,C_FName=@C_FName,C_LName=@C_LName,C_Phone=@C_Phone,C_Email=@C_Email,C_Notes=@C_Notes " +
                "where Id=@Id";
            delQuery = "delete from PassInfos where Id=@Id";
        }

        public string Create(PassInfo model)
        {
            using (conn = new 
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString)) 
            {
                conn.Open();
                using(cmd = new SqlCommand(addQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Booking_Date", model.Booking_Date);
                    cmd.Parameters.AddWithValue("@Booking_Route", model.Booking_Route);
                    cmd.Parameters.AddWithValue("@Qty", model.Qty);
                    cmd.Parameters.AddWithValue("@Tax", model.Tax);
                    cmd.Parameters.AddWithValue("@Total", model.Total);
                    cmd.Parameters.AddWithValue("@Payment_Method", model.Payment_Method);
                    cmd.Parameters.AddWithValue("@C_FName", model.C_FName);
                    cmd.Parameters.AddWithValue("@C_LName", model.C_LName);
                    cmd.Parameters.AddWithValue("@C_Phone", model.C_Phone);
                    cmd.Parameters.AddWithValue("@C_Email", model.C_Email);
                    cmd.Parameters.AddWithValue("@C_Notes", model.C_Notes);
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }

        public string Update(PassInfo model)
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
                    cmd.Parameters.AddWithValue("@Booking_Date", model.Booking_Date);
                    cmd.Parameters.AddWithValue("@Booking_Route", model.Booking_Route);
                    cmd.Parameters.AddWithValue("@Qty", model.Qty);
                    cmd.Parameters.AddWithValue("@Tax", model.Tax);
                    cmd.Parameters.AddWithValue("@Total", model.Total);
                    cmd.Parameters.AddWithValue("@Payment_Method", model.Payment_Method);
                    cmd.Parameters.AddWithValue("@C_FName", model.C_FName);
                    cmd.Parameters.AddWithValue("@C_LName", model.C_LName);
                    cmd.Parameters.AddWithValue("@C_Phone", model.C_Phone);
                    cmd.Parameters.AddWithValue("@C_Email", model.C_Email);
                    cmd.Parameters.AddWithValue("@C_Notes", model.C_Notes);
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
