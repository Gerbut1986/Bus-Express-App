namespace BusExpress.BLL.Services
{
    using BusExpress.BLL.Dto;
    using System.Data.SqlClient;
    using BusExpress.BLL.Interfaces;

    public class PassInfoService : IADOService
    { 
        readonly string addQuery, updQuery, delQuery;
        SqlConnection conn;
        SqlCommand cmd;

        public PassInfoService()
        {
            addQuery =
                "insert into PassInfoes(Booking_Date,Booking_Route,Qty,Tax,Total,Payment_Method,C_FName,C_LName,C_Phone,C_Email,C_Notes)" +
                " values(@Booking_Date,@Booking_Route,@Qty,@Tax,@Total,@Payment_Method,@C_FName,@C_LName,@C_Phone,@C_Email,@C_Notes)";
            updQuery = "update PassInfoes set " +
                "Booking_Date=@Booking_Date," +
                "Booking_Route=@Booking_Route," +
                "Qty=@Qty," +
                "Tax=@Tax," +
                "Total=@Total," +
                "Payment_Method=@Payment_Method," +
                "C_FName=@C_FName," +
                "C_LName=@C_LName," +
                "C_Phone=@C_Phone," +
                "C_Email=@C_Email," +
                "C_Notes=@C_Notes " +
                "where Id=@Id";
            delQuery = "delete from PassInfoes where Id=@Id";
        }

        public string Create(IModel entity, string connStr)
        {
            var model = entity as PassInfoDto;
            if (entity == null) return $"Not pass compatible model...You should to pass {nameof(PassInfoDto)} model.";
            using (conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (cmd = new SqlCommand(addQuery, conn))
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

        public string Update(IModel entity, string connStr)
        {
            var model = entity as PassInfoDto;
            if (entity == null) return $"Not pass compatible model...You should to pass {nameof(PassInfoDto)} model.";
            using (conn = new SqlConnection(connStr))
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
                    cmd.Parameters.AddWithValue("@C_Phone", model.C_Phone == null ? "" : model.C_Phone);
                    cmd.Parameters.AddWithValue("@C_Email", model.C_Email == null ? "" : model.C_Email);
                    cmd.Parameters.AddWithValue("@C_Notes", model.C_Notes == null ? "" : model.C_Notes);
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
