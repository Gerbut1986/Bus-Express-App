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

    public class PassInfoRepository : IRepository<PassInfo>
    {
        private readonly DataContext db;

        public PassInfoRepository(DataContext db) => this.db = db;

        public void Create(PassInfo entity)
        {
            db.PassInfoes.Add(entity);
            db.SaveChanges();
        }

        public void Delete(PassInfo entity)
        {
            db.PassInfoes.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var found = await db.PassInfoes.FindAsync(id);
            if (found != null)
                db.PassInfoes.Remove(found);
            else throw new Exception("Not found...");
        }

        public PassInfo Get(int id)
        {
            return db.PassInfoes.Find(id);
        }

        public IQueryable<PassInfo> GetAll()
        {
            return db.PassInfoes;
        }

        public async Task<List<PassInfo>> GetAllAsync()
        {
            return await db.PassInfoes.ToListAsync();
        }

        public void Update(PassInfo entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
