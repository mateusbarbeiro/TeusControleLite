using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeusControleLite.Application.Interfaces.Repository;
using TeusControleLite.Application.Interfaces.Services;
using TeusControleLite.Application.Validators;
using TeusControleLite.Domain.Models;
using TeusControleLite.Infrastructure.Dtos;
using TeusControleLite.Application.Interfaces.Repositories.BaseRepositories;
using TeusControleLite.Domain.Dtos;

namespace TeusControleLite.Application.Services
{
    /// <summary>
    /// Serciços da entidade produtos
    /// </summary>
    public class ProductsService : BaseService<Products>, IProductsService
    {
        private IProductsRepository _baseRepository;

        /// <summary>
        /// Construtor classe de serviços da entidade produtos
        /// </summary>
        /// <param name="baseRepository"></param>
        /// <param name="mapper"></param>
        public ProductsService(
            /*IHttpContextAccessor httpContextAccessor,*/
            IProductsRepository baseRepository, 
            IMapper mapper
        ) : base(
            (IBaseRepository<Products>)baseRepository,
            /*httpContextAccessor,*/
            mapper
        )
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Insere um novo produto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ResponseMessages<object> Insert(CreateProductsModel product)
        {
            try
            {
                var data = Add<CreateProductsModel, ProductsModel, ProductsValidator>(product);
                return new ResponseMessages<object>(
                    status: true, 
                    data: data, 
                    message: "Produto cadastrado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false, 
                    message: $"Ocorreu um erro: {ex}"
                );
            }
        }

        /// <summary>
        /// Atualiza um registro de produto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ResponseMessages<object> Update(UpdateProductsModel product)
        {
            try
            {
                var data = Update<UpdateProductsModel, ProductsModel, ProductsValidator>(product);
                // ToDo: persistir alterações em uma tabela para logs
                return new ResponseMessages<object>(
                    status: true,
                    message: "Produto alterado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Ocorreu um erro: { ex.Message }"
                );  
            }
        }

        /// <summary>
        /// Busca registro por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessages<object> GetById(long id)
        {
            try
            {
                if (!_baseRepository.Any(x => 
                    x.Id == id &&
                    !x.Deleted
                ))
                    throw new Exception("Registro não encontrado.");
                
                var data = _baseRepository
                    .Query(x => x.Id == id)
                    .Select(s => new
                    {
                        s.Id,
                        s.Gtin,
                        s.Description,
                        s.Price,
                        s.BrandName,
                        s.GpcCode,
                        s.GpcDescription,
                        s.NcmCode,
                        s.NcmDescription,
                        s.NcmFullDescription,
                        s.Thumbnail,
                        s.InStock,
                        s.Active,
                        s.CreatedDate,
                    })
                    .FirstOrDefault();

                return new ResponseMessages<object>(
                    status: true,
                    message: "Produto encontrado.",
                    data: data
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }

        /// <summary>
        /// Deleta um registro de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessages<object> DeleteById(long id)
        {
            try
            {
                LogicalDelete(id);

                return new ResponseMessages<object>(
                    status: true,
                    message: "Produto deletado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }

        /// <summary>
        /// Busca lista de todos produtos levando em consideração parametros de filtragem e ordenação
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        public async Task<ResponseMessages<object>> Get(PaginatedInputModel pagingParams)
        {
            try
            {
                var products = await GetPaged(pagingParams);

                return new ResponseMessages<object>(
                    status: true,
                    message: "Busca realizada com sucesso.",
                    data: products
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }

        /// <summary>
        /// Desfaz exclusão
        /// </summary>
        /// <returns></returns>
        public ResponseMessages<object> Undelete(int id)
        {
            try
            {
                UpdateSomeFields(
                    new Products
                    {
                        Id = id,
                        Deleted = false
                    },
                    u => u.Deleted
                );

                return new ResponseMessages<object>(
                    status: true,
                    message: "Alteração desfeita com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }
    }
}
