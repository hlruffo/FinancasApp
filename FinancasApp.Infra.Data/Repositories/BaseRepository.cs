using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Repositories
{
    /// <summary>
    /// Classe generica para repositorio de banco de dados
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        /// <summary>
        /// Gravar registro em uma tabela do banco de dados
        /// </summary>        
        public void Add(T entity)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(entity);
                dataContext.SaveChanges(); 
            }
        }
        /// <summary>
        /// Remover registro de uma tabela do banco de dados
        /// </summary>

        public void Delete(T entity)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Remove(entity);
                dataContext.SaveChanges();
            }
        }


        /// <summary>
        /// Consultar todos os elementos de uma entidade no banco de dados
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<T> GetAll()
        {
            using(var dataContext = new DataContext())
            {
                return dataContext.Set<T>().ToList();
            }
        }

        /// <summary>
        /// Consultar 1 registro por id numa tabela do Banco de dados
        /// </summary>
        
        public T? GetById(Guid id)
        {
            using(var dataContext = new DataContext())
            {
                return dataContext?.Set<T>().Find(id);
            }
        }

        /// <summary>
        /// Atualizar registro em uma tabela de banco de dados
        /// </summary>
            public void Update(T entity)
        {
                using (var dataContext = new DataContext())
                {
                    dataContext.Update(entity);
                    dataContext.SaveChanges();
                }
                
        }
    }
}
