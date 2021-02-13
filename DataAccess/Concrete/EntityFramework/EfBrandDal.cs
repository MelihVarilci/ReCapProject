using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal:IBrandDal
    {
        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
        {
            // dispose
            using (ReCapContext context = new ReCapContext())
            {
                return filter == null ? context.Set<Brand>().ToList() : context.Set<Brand>().Where(filter).ToList();
            }
        }

        public Brand Get(Expression<Func<Brand, bool>> filter)
        {
            using (ReCapContext context = new ReCapContext())
            {
                return context.Set<Brand>().SingleOrDefault(filter);
            }
        }

        public void Add(Brand entity)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Brand entity)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Brand entity)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
