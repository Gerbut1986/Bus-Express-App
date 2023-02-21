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

    public class SchemaPlaceRepository : IRepository<SchemaPlace>
    {
        private readonly DataContext db;

        public SchemaPlaceRepository(DataContext db) => this.db = db;

        public void Create(SchemaPlace entity)
        {
            db.SchemaPlaces.Add(entity);
            db.SaveChanges();
        }

        public void Delete(SchemaPlace entity)
        {
            db.SchemaPlaces.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var found = await db.SchemaPlaces.FindAsync(id);
            if (found != null)
                db.SchemaPlaces.Remove(found);
            else throw new Exception("Not found...");
        }

        public SchemaPlace Get(int id)
        {
            return db.SchemaPlaces.Find(id);
        }

        public IQueryable<SchemaPlace> GetAll()
        {
            return db.SchemaPlaces;
        }

        public async Task<List<SchemaPlace>> GetAllAsync()
        {
            return await db.SchemaPlaces.ToListAsync();
        }

        public void Update(SchemaPlace entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
