﻿using RestauranteTarea.Models.Entities;

namespace RestauranteTarea.Repositories
{
    public class Repository<T> where T : class
    {
        public NeatContext Context { get; }
        public Repository(NeatContext context)
        {
            Context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual T? Get(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }
    }
}
