using System;
using System.Collections.Generic;
using System.Text;
using Theater.Domain.Core.Models;
using Theater.Domain.Interfaces;

namespace Theater.Infrastructure.Data.Repositories
{
    public class PerformanceRepository : IRepository<Performance>
    {
        private readonly TheaterContext db;

        public PerformanceRepository(TheaterContext context)
        {
            db = context;
        }
        public void Create(Performance performance)
        {
            db.Performances.Add(performance);
            db.SaveChanges();
        }
        public IEnumerable<Performance> GetList()
        {
            return db.Performances;
        }

        public Performance Get(int id)
        {
            return db.Performances.Find(id);
        }

        public void Delete(int id)
        {
            Performance performance = db.Performances.Find(id);
            if (performance != null)
                db.Performances.Remove(performance);
            db.SaveChanges();
        }
    }
}
