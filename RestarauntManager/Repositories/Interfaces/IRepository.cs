using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        List<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        void Delete(int id);
        bool Update(T entity);
    }
}
