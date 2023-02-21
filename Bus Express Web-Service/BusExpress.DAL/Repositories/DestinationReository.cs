namespace BusExpress.DAL.Repositories
{
    using System;
    using System.Linq;
    using BusExpress.DAL.EF;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using BusExpress.DAL.Entities;
    using BusExpress.DAL.Interfaces;
    using System.Collections.Generic;

    public class DestinationReository : IRepository<Destination>
    {
        private readonly DataContext db;

        public DestinationReository(DataContext db) => this.db = db;

        public void Create(Destination entity)
        {
            db.Destinations.Add(entity);
            db.SaveChanges();
        }

        public void Delete(Destination entity)
        {
            db.Destinations.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var found = await db.Destinations.FindAsync(id);
            if (found != null)
                db.Destinations.Remove(found);
            else throw new Exception("Not found...");
        }

        public Destination Get(int id)
        {
            return db.Destinations.Find(id);
        }

        public IQueryable<Destination> GetAll()
        {
            return db.Destinations;
        }

        public async Task<List<Destination>> GetAllAsync()
        {
            return await db.Destinations.ToListAsync();
        }

        public void Update(Destination entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
