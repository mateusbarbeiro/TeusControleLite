using TeusControleLite.Application.Interfaces.Repository;
using TeusControleLite.Domain.Models;
using TeusControleLite.Infrastructure.Context;

namespace TeusControleLite.Infrastructure.Repository
{
    /// <summary>
    /// Repositório para produtos
    /// </summary>
    public class ProductsRepository : BaseRepository<Products>, IProductsRepository
    {
        public ProductsRepository(ApiContext context) : base (context)
        {

        }
    }
}