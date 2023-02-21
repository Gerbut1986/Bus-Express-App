namespace BusExpress.DAL.UOF
{
    using EF;
    using Interfaces;
    using BusExpress.DAL.Entities;
    using BusExpress.DAL.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext Db;
        private PassInfoRepository passRepo;
        private OrderInfoRepository orderRepo;
        private DestinationReository destRepo;
        private SchemaPlaceRepository schemaRepo;

        public UnitOfWork(string conn)
        {
            Db = new DataContext(conn);
        }

        public IRepository<OrderInfo> OrderInfos
        {
            get
            {
                if (orderRepo == null)
                    orderRepo = new OrderInfoRepository(Db);
                return orderRepo;
            }
        }

        public IRepository<PassInfo> PassInfos
        {
            get
            {
                if (passRepo == null)
                    passRepo = new PassInfoRepository(Db);
                return passRepo;
            }
        }

        public IRepository<SchemaPlace> SchemaPlaces
        {
            get
            {
                if (schemaRepo == null)
                    schemaRepo = new SchemaPlaceRepository(Db);
                return schemaRepo;
            }
        }

        public IRepository<Destination> Destinations
        {
            get
            {
                if (destRepo == null)
                    destRepo = new DestinationReository(Db);
                return destRepo;
            }
        }

        #region Dispose:
        bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    Db.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
        #endregion
    }
}
