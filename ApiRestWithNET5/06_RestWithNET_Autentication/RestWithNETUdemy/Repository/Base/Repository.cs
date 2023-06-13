using Microsoft.EntityFrameworkCore;
using RestWithNETUdemy.Model.Base;
using RestWithNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithNETUdemy.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected MySQLContext _context;
        private DbSet<T> dataset;

        public Repository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(i => i.Id.Equals(id));
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public T Update(T item)
        {
            try
            {
                var result = dataset.SingleOrDefault(i => i.Id.Equals(item.Id));

                if (result != null)
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                else { 
                    return null;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            try
            {
                var result = dataset.SingleOrDefault(i => i.Id.Equals(id));

                if (result != null)
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(i => i.Id.Equals(id));
        }       
    }
}
