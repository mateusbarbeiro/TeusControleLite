using System.Threading.Tasks;
using TeusControleLite.Application.Interfaces.Services.BaseServices;
using TeusControleLite.Domain.Dtos;
using TeusControleLite.Domain.Models;
using TeusControleLite.Infrastructure.Dtos;

namespace TeusControleLite.Application.Interfaces.Services
{
    /// <summary>
    /// Inteface para serviço de usuários. Declaração de métodos
    /// </summary>
    public interface IProductsService : IBaseService<Products>
    {
        /// <summary>
        /// Insere um novo usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ResponseMessages<object> Insert(CreateProductsModel user);

        /// <summary>
        /// Atualiza um registro de usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ResponseMessages<object> Update(UpdateProductsModel user);

        /// <summary>
        /// Busca um registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseMessages<object> GetById(long id);

        /// <summary>
        /// Deleta um registro de usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseMessages<object> DeleteById(long id);

        /// <summary>
        /// Busca lista de todos produtos levando em consideração parametros de filtragem e ordenação
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns
        Task<ResponseMessages<object>> Get(PaginatedInputModel pagingParams);

        /// <summary>
        /// Desfaz exclusão
        /// </summary>
        /// <returns></returns>
        ResponseMessages<object> Undelete(int id);
    }
}
