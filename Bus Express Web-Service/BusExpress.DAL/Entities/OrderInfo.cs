namespace BusExpress.DAL.Entities
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string LName_FName { get; set; }
        public int PlaceNumber { get; set; }
        public string Phone { get; set; }
        public string OrderNumber { get; set; }
        public string MoneyAmount { get; set; }
        public bool  IsOrdered { get; set; }
    }
}
