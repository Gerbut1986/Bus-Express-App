namespace Transfer_App.Models.EF
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() 
            : base("Transfer_App.Properties.Settings.TransferDBConnectionString")
        {
        }
        
        public virtual DbSet<PassInfo> PassInfos { get; set; }
        public virtual DbSet<OrderInfo> OrderInfos { get; set; }
        public virtual DbSet<SchemaPlace> SchemaPlaces { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
    }
}
