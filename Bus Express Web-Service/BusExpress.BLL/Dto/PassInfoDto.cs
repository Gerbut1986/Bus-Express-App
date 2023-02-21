namespace BusExpress.BLL.Dto
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PassInfos")]
    public class PassInfoDto : Interfaces.IModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required! Please fill it in.")]
        [DataType(DataType.Date)]
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

        public override string ToString()
        {
            return $"{Id},{Booking_Date},{Booking_Route},{Qty},{Tax}," +
                   $"{Total},{Payment_Method},{C_FName},{C_LName}," +
                   $"{C_Phone},{C_Email},{C_Notes}";
        }
    }
}
