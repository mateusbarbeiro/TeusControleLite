using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeusControleLite.Application.Interfaces.Services;
using TeusControleLite.Infrastructure.Dtos;
using TeusControleLite.Domain.Dtos;

namespace TeusControleLite.Infrastructure.Controllers
{
    /// <summary>
    /// Controlador para CRUD de produtos
    /// </summary>
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;
        
        /// <summary>
        /// Endpoints para produtos
        /// </summary>
        /// <param name="service"></param>
        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Inserir um novo produto
        /// </summary>
        /// <param name="sentProduct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] CreateProductsModel sentProduct)
        {
            return Ok(_service.Insert(sentProduct));
        }

        /// <summary>
        /// Atualizar um produto
        /// </summary>
        /// <param name="sentProduct"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] UpdateProductsModel sentProduct)
        {
            return Ok(_service.Update(sentProduct));
        }

        /// <summary>
        /// Buscar um produto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById([FromHeader] long id)
        {
            return Ok(_service.GetById(id));
        }

        /// <summary>
        /// Busca todos os produtos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromBody] PaginatedInputModel pagingParams)
        {
            return Ok(await _service.Get(pagingParams));
        }

        /// <summary>
        /// Excluir um produto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromHeader] long id)
        {
            return Ok(_service.DeleteById(id));
        }

        /// <summary>
        /// Desfaz exclusão
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Undelete")]
        public IActionResult Undelete([FromHeader] int id)
        {
            return Ok(_service.Undelete(id));
        }
    }
}
