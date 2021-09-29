using AutoMapper;
using FluentValidation;
/*using Microsoft.AspNetCore.Http;*/
using System;
using System.Linq.Expressions;
using TeusControleLite.Domain.Models.CommonModels;
using TeusControleLite.Application.Interfaces.Services.BaseServices;
using TeusControleLite.Application.Interfaces.Repositories.BaseRepositories;

// todo: resolver problema ao atualizar dados, metodo update esta perdendo informações de propriedades nao passadas por parametro
// deve atualizar somente as informações passadas, nao alterando/limpando as colunas que não foram passadas
namespace TeusControleLite.Application.Services
{
    /// <summary>
    /// Classe de serviço genérica para entidades associativas. CRUD genérico.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class BaseDoubleService<TEntity> : IBaseDoubleService<TEntity> where TEntity : BaseDoubleEntity, new()
    {
        private readonly IBaseDoubleRepository<TEntity> _baseRepository;
        /*private readonly IHttpContextAccessor _httpContextAccessor;*/
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor de classe base para service de chave composta
        /// </summary>
        /// <param name="baseRepository"></param>
        /// <param name="mapper"></param>
        public BaseDoubleService(
            IBaseDoubleRepository<TEntity> baseRepository,
            /*IHttpContextAccessor httpContextAccessor,*/
            IMapper mapper
        )
        {
            _baseRepository = baseRepository;
            /*_httpContextAccessor = httpContextAccessor;*/
            _mapper = mapper;
        }

        /// <summary>
        /// Exclui logicamente um registro a partir do id e id2
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        public void LogicalDelete(
            long id,
            long id2
        )
        {
            if (!_baseRepository.Any(x =>
                x.Id == id &&
                x.Id2 == id2 &&
                !x.Deleted
            ))
                throw new Exception("Registro não encontrado.");

            var entity = new TEntity
            {
                Id = id,
                Id2 = id2,
                Deleted = true
            };

            entity.LastChange = DateTime.Now;
            _baseRepository.UpdateFields(
                entity, 
                b => b.Deleted, 
                b => b.LastChange
            );
        }

        public TOutputModel Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());

            /*entity.CreatedBy = long.Parse(_httpContextAccessor
                .HttpContext.User.FindFirst(
                    CustomClaimTypes.Id
                )
                .Value
            );*/
            
            entity.CreatedDate = DateTime.Now;
            _baseRepository.Insert(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        /// <summary>
        /// Cria um novo registro
        /// </summary>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public TOutputModel Add<TOutputModel>(TEntity inputModel)
            where TOutputModel : class
        {

            /*inputModel.CreatedBy = long.Parse(_httpContextAccessor
                .HttpContext.User.FindFirst(
                    CustomClaimTypes.Id
                )
                .Value
            );*/
            
            inputModel.CreatedDate = DateTime.Now;
            _baseRepository.Insert(inputModel);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(inputModel);

            return outputModel;
        }

        /// <summary>
        /// Exclui fisicamente um registro a partir do id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        public void PhysicalDelete(long id, long id2) => _baseRepository.PhysicalDelete(id, id2);

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <typeparam name="TInputModel"></typeparam>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(
                entity,
                Activator.CreateInstance<TValidator>()
            );
            /*entity.CreatedBy = long.Parse(_httpContextAccessor
                .HttpContext.User.FindFirst(
                    CustomClaimTypes.Id
                )
                .Value
            );*/

            entity.LastChange = DateTime.Now;
            _baseRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        /// <summary>
        /// Atualiza alguns campos
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedProperties"></param>
        public void UpdateSomeFields(
            TEntity entity,
            params Expression<Func<TEntity, object>>[] updatedProperties
        )
        {
            _baseRepository.UpdateFields(
                entity,
                updatedProperties
            );
        }

        /// <summary>
        /// Valida o objeto a ser cadastrado
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validator"></param>
        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
