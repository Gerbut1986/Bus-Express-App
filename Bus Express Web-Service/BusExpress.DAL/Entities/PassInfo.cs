namespace BusExpress.DAL.Entities
{
    public class PassInfo
    {
        public int Id { get; set; }
        public System.DateTime Booking_Date { get; set; }
        public string Booking_Route { get; set; }
        public int Qty { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
        public string Payment_Method { get; set; }
        public string C_FName { get; set; }
        public string C_LName { get; set; }
        public string C_Phone { get; set; }
        public string C_Email { get; set; }
        public string C_Notes { get; set; }
    }
}
