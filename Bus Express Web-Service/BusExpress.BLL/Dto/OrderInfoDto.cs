namespace BusExpress.BLL.Dto
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderInfos")]
    public class OrderInfoDto : Interfaces.IModel
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string LName_FName { get; set; }
        public int PlaceNumber { get; set; }
        public string Phone { get; set; }
        public string OrderNumber { get; set; }
        public string MoneyAmount { get; set; }
        public bool IsOrdered { get; set; }

        public override string ToString()
        {
            var isIrdered = IsOrdered ? "+" : "x";
            return $"{Id},{From},{To},{LName_FName},{PlaceNumber}," +
                   $"{Phone},{OrderNumber},{MoneyAmount},{isIrdered}";
        }
    }
}
