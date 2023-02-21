namespace BusExpress.DAL.Interfaces
{
    using Entities;

    public interface IUnitOfWork : System.IDisposable
    {
        IRepository<PassInfo> PassInfos { get; }
        IRepository<OrderInfo> OrderInfos { get; }
        IRepository<Destination> Destinations { get; }
        IRepository<SchemaPlace> SchemaPlaces { get; }
    }
}
