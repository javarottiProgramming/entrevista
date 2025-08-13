using Projeto.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Core.Infrastructure.Database
{
    /// <summary>
    /// Interface que representa as operações CRUD básicas para uma entidade.
    /// </summary>
    public interface ICrudService<T>
        where T : IEntity
    {
        public T GetById(int id);
        public void Insert(T entity);

        public void Update(T entity);
        public void Update(List<T> entity);

        public void DeleteById(int id);

        public List<T> GetAll();

    }
}