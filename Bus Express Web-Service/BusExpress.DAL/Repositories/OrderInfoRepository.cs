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

    public class OrderInfoRepository : IRepository<OrderInfo>
    {
        private readonly DataContext db;

        public OrderInfoRepository(DataContext db) => this.db = db;

        public void Create(OrderInfo entity)
        {
            db.OrderInfoes.Add(entity);
            db.SaveChanges();
        }

        public void Delete(OrderInfo entity)
        {
            db.OrderInfoes.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var found = await db.OrderInfoes.FindAsync(id);
            if (found != null)
                db.OrderInfoes.Remove(found);
            else throw new Exception("Not found...");
        }

        public OrderInfo Get(int id)
        {
            return db.OrderInfoes.Find(id);
        }

        public IQueryable<OrderInfo> GetAll()
        {
            return db.OrderInfoes;
        }

        public async Task<List<OrderInfo>> GetAllAsync()
        {
            return await db.OrderInfoes.ToListAsync();
        }

        public void Update(OrderInfo entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
