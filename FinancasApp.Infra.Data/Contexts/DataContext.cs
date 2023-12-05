using FinancasApp.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Infra.Data.Contexts
{
    /// <summary>
    /// Conexão e configuração do BD no EntityFramework
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Método para configurar a string de conexão do BD
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDFinancasApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        /// <summary>
        /// Método para adicionar as classes de mapeamento ORM do projeto 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new MovimentacaoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}
