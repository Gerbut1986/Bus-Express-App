namespace BusExpress.DAL.EF
{
    using Entities;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext(string conn) : base(conn)
        {
        }

        public virtual DbSet<PassInfo> PassInfoes { get; set; }
        public virtual DbSet<OrderInfo> OrderInfoes { get; set; }
        public virtual DbSet<SchemaPlace> SchemaPlaces { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
    }
}
