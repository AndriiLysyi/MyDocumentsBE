using MyDocuments.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using MyDocuments.DAL.EF;

namespace MyDocuments.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected DbSet<T> DbSet;
        protected DocumentContext Context;

        public Repository(DocumentContext context)
        {
            DbSet = context.Set<T>();
            Context = context;
        }


        public async Task<T> Get(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(T item)
        {
            DbSet.Add(item);
        }

        public void Update(T item)
        {
            DbSet.Attach(item);
            var entry = Context.Entry(item);
            entry.State = EntityState.Modified;

        }

        public void Remove(T item)
        {
            DbSet.Remove(item);
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }


    }
}