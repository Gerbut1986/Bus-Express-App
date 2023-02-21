namespace BusExpress.PL.Models.ADO
{
    using Models;
    using System.Configuration;
    using System.Data.SqlClient;

    public class ServiceSchemaPlaces
    {
        readonly string addQuery, updQuery, delQuery;
        SqlConnection conn;
        SqlCommand cmd;

        public ServiceSchemaPlaces()
        {
            addQuery =
                "insert into SchemaPlaces(BusNameNumberc," +
                "Is1Place,Is2Place,Is3Place,Is4Place,Is5Place,Is6Place,Is7Place,Is8Place,Is9Place,Is10Place," +
                "Is11Place,Is12Place,Is13Place,Is14Place,Is15Place,Is16Place,Is17Place,Is18Place,Is19Place,Is20Place," +
                "Is21Place,Is22Place,Is23Place,Is24Place,Is25Place,Is26Place,Is27Place,Is28Place,Is29Place,Is30Place," +
                "Is31Place,Is32Place,Is33Place,Is34Place,Is35Place,Is36Place,Is37Place,Is38Place,Is39Place,Is40Place," +
                "Is41Place,Is42Place,Is43Place,Is44Place,Is45Place,Is46Place,Is47Place,Is48Place,Is49Place,Is50Place," +
                "Is51Place,Is52Place,Is53Place,Is54Place,Is55Place,GoDate)" +
                " values(@BusNameNumber," +
                "@Is1Place,@Is2Place,@Is3Place,@Is4Place,@Is5Place,@Is6Place,@Is7Place,@Is8Place,@Is9Place,@Is10Place," +
                "@Is11Place,@Is12Place,@Is13Place,@Is14Place,@Is15Place,@Is16Place,@Is17Place,@Is18Place,@Is19Place,@Is20Place," +
                "@Is21Place,@Is22Place,@Is23Place,@Is24Place,@Is25Place,@Is26Place,@Is27Place,@Is28Place,@Is29Place,@Is30Place," +
                "@Is31Place,@Is32Place,@Is33Place,@Is34Place,@Is35Place,@Is36Place,@Is37Place,@Is38Place,@Is39Place,@Is40Place," +
                "@Is41Place,@Is42Place,@Is43Place,@Is44Place,@Is45Place,@Is46Place,@Is47Place,@Is48Place,@Is49Place,@Is50Place," +
                "@Is51Place,@Is52Place,@Is53Place,@Is54Place,@Is55Place,@GoDate)";
            updQuery = "update SchemaPlaces set Booking_Date=@Booking_Date,Booking_Route=@Booking_Route,Qty=@Qty,Tax=@Tax,Total=@Total,Payment_Method=@Payment_Method,C_FName=@C_FName,C_LName=@C_LName,C_Phone=@C_Phone,C_Email=@C_Email,C_Notes=@C_Notes " +
                "where Id=@Id";
            delQuery = "delete from SchemaPlaces where Id=@Id";
        }

        public string Create(SchemaPlace model)
        {
            using (conn = new
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(addQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@BusNameNumber", model.BusNameNumber);
                    cmd.Parameters.AddWithValue("@GoDate", model.GoDate);
                    cmd.Parameters.AddWithValue("@Is1Place", model.Is1Place);
                    cmd.Parameters.AddWithValue("@Is2Place", model.Is2Place);
                    cmd.Parameters.AddWithValue("@Is3Place", model.Is3Place);
                    cmd.Parameters.AddWithValue("@Is4Place", model.Is4Place);
                    cmd.Parameters.AddWithValue("@Is5Place", model.Is5Place);
                    cmd.Parameters.AddWithValue("@Is6Place", model.Is6Place);
                    cmd.Parameters.AddWithValue("@Is7Place", model.Is7Place);
                    cmd.Parameters.AddWithValue("@Is8Place", model.Is8Place);
                    cmd.Parameters.AddWithValue("@Is9Place", model.Is9Place);
                    cmd.Parameters.AddWithValue("@Is10Place", model.Is10Place);
                    cmd.Parameters.AddWithValue("@Is11Place", model.Is11Place);
                    cmd.Parameters.AddWithValue("@Is12Place", model.Is12Place);
                    cmd.Parameters.AddWithValue("@Is13Place", model.Is13Place);
                    cmd.Parameters.AddWithValue("@Is14Place", model.Is14Place);
                    cmd.Parameters.AddWithValue("@Is15Place", model.Is15Place);
                    cmd.Parameters.AddWithValue("@Is16Place", model.Is16Place);
                    cmd.Parameters.AddWithValue("@Is17Place", model.Is17Place);
                    cmd.Parameters.AddWithValue("@Is18Place", model.Is18Place);
                    cmd.Parameters.AddWithValue("@Is19Place", model.Is19Place);
                    cmd.Parameters.AddWithValue("@Is20Place", model.Is20Place);
                    cmd.Parameters.AddWithValue("@Is21Place", model.Is21Place);
                    cmd.Parameters.AddWithValue("@Is22Place", model.Is22Place);
                    cmd.Parameters.AddWithValue("@Is23Place", model.Is23Place);
                    cmd.Parameters.AddWithValue("@Is24Place", model.Is24Place);
                    cmd.Parameters.AddWithValue("@Is25Place", model.Is25Place);
                    cmd.Parameters.AddWithValue("@Is26Place", model.Is26Place);
                    cmd.Parameters.AddWithValue("@Is27Place", model.Is27Place);
                    cmd.Parameters.AddWithValue("@Is28Place", model.Is28Place);
                    cmd.Parameters.AddWithValue("@Is29Place", model.Is29Place);
                    cmd.Parameters.AddWithValue("@Is30Place", model.Is30Place);
                    cmd.Parameters.AddWithValue("@Is31Place", model.Is31Place);
                    cmd.Parameters.AddWithValue("@Is32Place", model.Is32Place);
                    cmd.Parameters.AddWithValue("@Is33Place", model.Is33Place);
                    cmd.Parameters.AddWithValue("@Is34Place", model.Is34Place);
                    cmd.Parameters.AddWithValue("@Is35Place", model.Is35Place);
                    cmd.Parameters.AddWithValue("@Is36Place", model.Is36Place);
                    cmd.Parameters.AddWithValue("@Is37Place", model.Is37Place);
                    cmd.Parameters.AddWithValue("@Is38Place", model.Is38Place);
                    cmd.Parameters.AddWithValue("@Is39Place", model.Is39Place);
                    cmd.Parameters.AddWithValue("@Is40Place", model.Is40Place);
                    cmd.Parameters.AddWithValue("@Is41Place", model.Is41Place);
                    cmd.Parameters.AddWithValue("@Is42Place", model.Is42Place);
                    cmd.Parameters.AddWithValue("@Is43Place", model.Is43Place);
                    cmd.Parameters.AddWithValue("@Is44Place", model.Is44Place);
                    cmd.Parameters.AddWithValue("@Is45Place", model.Is45Place);
                    cmd.Parameters.AddWithValue("@Is46Place", model.Is46Place);
                    cmd.Parameters.AddWithValue("@Is47Place", model.Is47Place);
                    cmd.Parameters.AddWithValue("@Is48Place", model.Is48Place);
                    cmd.Parameters.AddWithValue("@Is49Place", model.Is49Place);
                    cmd.Parameters.AddWithValue("@Is50Place", model.Is50Place);
                    cmd.Parameters.AddWithValue("@Is51Place", model.Is51Place);
                    cmd.Parameters.AddWithValue("@Is52Place", model.Is52Place);
                    cmd.Parameters.AddWithValue("@Is53Place", model.Is53Place);
                    cmd.Parameters.AddWithValue("@Is54Place", model.Is54Place);
                    cmd.Parameters.AddWithValue("@Is55Place", model.Is55Place);              
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }

        public string Update(SchemaPlace model)
        {
            using (conn = new
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand(updQuery, conn))
                {
                    #region All Params:
                    cmd.Parameters.AddWithValue("@BusNameNumber", model.BusNameNumber);
                    cmd.Parameters.AddWithValue("@GoDate", model.GoDate);
                    cmd.Parameters.AddWithValue("@Is1Place", model.Is1Place);
                    cmd.Parameters.AddWithValue("@Is2Place", model.Is2Place);
                    cmd.Parameters.AddWithValue("@Is3Place", model.Is3Place);
                    cmd.Parameters.AddWithValue("@Is4Place", model.Is4Place);
                    cmd.Parameters.AddWithValue("@Is5Place", model.Is5Place);
                    cmd.Parameters.AddWithValue("@Is6Place", model.Is6Place);
                    cmd.Parameters.AddWithValue("@Is7Place", model.Is7Place);
                    cmd.Parameters.AddWithValue("@Is8Place", model.Is8Place);
                    cmd.Parameters.AddWithValue("@Is9Place", model.Is9Place);
                    cmd.Parameters.AddWithValue("@Is10Place", model.Is10Place);
                    cmd.Parameters.AddWithValue("@Is11Place", model.Is11Place);
                    cmd.Parameters.AddWithValue("@Is12Place", model.Is12Place);
                    cmd.Parameters.AddWithValue("@Is13Place", model.Is13Place);
                    cmd.Parameters.AddWithValue("@Is14Place", model.Is14Place);
                    cmd.Parameters.AddWithValue("@Is15Place", model.Is15Place);
                    cmd.Parameters.AddWithValue("@Is16Place", model.Is16Place);
                    cmd.Parameters.AddWithValue("@Is17Place", model.Is17Place);
                    cmd.Parameters.AddWithValue("@Is18Place", model.Is18Place);
                    cmd.Parameters.AddWithValue("@Is19Place", model.Is19Place);
                    cmd.Parameters.AddWithValue("@Is20Place", model.Is20Place);
                    cmd.Parameters.AddWithValue("@Is21Place", model.Is21Place);
                    cmd.Parameters.AddWithValue("@Is22Place", model.Is22Place);
                    cmd.Parameters.AddWithValue("@Is23Place", model.Is23Place);
                    cmd.Parameters.AddWithValue("@Is24Place", model.Is24Place);
                    cmd.Parameters.AddWithValue("@Is25Place", model.Is25Place);
                    cmd.Parameters.AddWithValue("@Is26Place", model.Is26Place);
                    cmd.Parameters.AddWithValue("@Is27Place", model.Is27Place);
                    cmd.Parameters.AddWithValue("@Is28Place", model.Is28Place);
                    cmd.Parameters.AddWithValue("@Is29Place", model.Is29Place);
                    cmd.Parameters.AddWithValue("@Is30Place", model.Is30Place);
                    cmd.Parameters.AddWithValue("@Is31Place", model.Is31Place);
                    cmd.Parameters.AddWithValue("@Is32Place", model.Is32Place);
                    cmd.Parameters.AddWithValue("@Is33Place", model.Is33Place);
                    cmd.Parameters.AddWithValue("@Is34Place", model.Is34Place);
                    cmd.Parameters.AddWithValue("@Is35Place", model.Is35Place);
                    cmd.Parameters.AddWithValue("@Is36Place", model.Is36Place);
                    cmd.Parameters.AddWithValue("@Is37Place", model.Is37Place);
                    cmd.Parameters.AddWithValue("@Is38Place", model.Is38Place);
                    cmd.Parameters.AddWithValue("@Is39Place", model.Is39Place);
                    cmd.Parameters.AddWithValue("@Is40Place", model.Is40Place);
                    cmd.Parameters.AddWithValue("@Is41Place", model.Is41Place);
                    cmd.Parameters.AddWithValue("@Is42Place", model.Is42Place);
                    cmd.Parameters.AddWithValue("@Is43Place", model.Is43Place);
                    cmd.Parameters.AddWithValue("@Is44Place", model.Is44Place);
                    cmd.Parameters.AddWithValue("@Is45Place", model.Is45Place);
                    cmd.Parameters.AddWithValue("@Is46Place", model.Is46Place);
                    cmd.Parameters.AddWithValue("@Is47Place", model.Is47Place);
                    cmd.Parameters.AddWithValue("@Is48Place", model.Is48Place);
                    cmd.Parameters.AddWithValue("@Is49Place", model.Is49Place);
                    cmd.Parameters.AddWithValue("@Is50Place", model.Is50Place);
                    cmd.Parameters.AddWithValue("@Is51Place", model.Is51Place);
                    cmd.Parameters.AddWithValue("@Is52Place", model.Is52Place);
                    cmd.Parameters.AddWithValue("@Is53Place", model.Is53Place);
                    cmd.Parameters.AddWithValue("@Is54Place", model.Is54Place);
                    cmd.Parameters.AddWithValue("@Is55Place", model.Is55Place);
                    #endregion
                    var exec = cmd.ExecuteNonQuery();
                    return exec == 1 ? "Success!" : "..Faild..";
                }
            }
        }

        public string UpdateOnePlc(int plcNum, bool isRemove = false)
        {
            using (conn = new
                SqlConnection(ConfigurationManager.
                ConnectionStrings["Transfer_App.Properties.Settings.TransferDBConnectionString"].
                ConnectionString))
            {
                conn.Open();
                using (cmd = new SqlCommand($"update SchemaPlaces set Is{plcNum}Place=@Is{plcNum}Place" +
                    " where Id=1", conn))
                {
                    bool b = isRemove ? false : true;
                    cmd.Parameters.AddWithValue($"@Is{plcNum}Place", b);
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
