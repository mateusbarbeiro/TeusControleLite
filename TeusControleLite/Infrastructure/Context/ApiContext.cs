using Microsoft.EntityFrameworkCore;
using TeusControleLite.Domain.Models;

namespace TeusControleLite.Infrastructure.Context
{
    /// <summary>
    /// Contexto da api
    /// Classe de acesso a dados
    /// </summary>
    public partial class ApiContext : DbContext
    {
        /// <summary>
        /// Construtor da classe contexto da api
        /// </summary>
        public ApiContext()
        {
        }

        /// <summary>
        /// Construtor da classe contexto da api
        /// </summary>
        /// <param name="options"></param>
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        /// <summary>
        /// Entidade produtos
        /// </summary>
        public virtual DbSet<Products> Products { get; set; }

        /// <summary>
        /// Configuração do contexto da base de dados
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductsMapping());
        }
    }
}
