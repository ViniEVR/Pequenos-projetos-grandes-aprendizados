using Microsoft.EntityFrameworkCore;
using CursoEFCore.domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Linq;

namespace CursoEFCore.Data
{
    public class ApplicationContext : DbContext
    {
        
       private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());

        public DbSet<Pedido> Pedidos {get; set;}
        public DbSet<Produto> Produtos {get; set;}
        public DbSet<Cliente> Clientes {get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseLoggerFactory(_logger)
            .EnableSensitiveDataLogging()
            .UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore; Integrated Security = true",p=>p.EnableRetryOnFailure(
                maxRetryCount: 10, 
                maxRetryDelay: TimeSpan.FromSeconds(5), 
                errorNumbersToAdd: null).MigrationsHistoryTable("curso_ef_core"));
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
                    Fazendo implementação por implementação

            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());

            */

            //Fazendo todas implementações de uma vez direto do Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);
            
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p=>p.ClrType == typeof(string));

                foreach(var propety in properties)
                {
                    if(string.IsNullOrEmpty(propety.GetColumnType()) && !propety.GetMaxLength().HasValue)
                    {
                        //propety.SetMaxLength(100);
                        propety.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }

    }
}