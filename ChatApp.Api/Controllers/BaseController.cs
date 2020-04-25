using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ChatApp.Api.Filters;
using ChatApp.Bl.Dto;
using ChatApp.Core.BaseModel.BaseEntity;
using ChatApp.Core.BaseModel.BaseEntityDto;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.Repositories;
using ChatApp.Model.UnitOfWorks;

namespace ChatApp.Api.Controllers
{

    public interface IBaseController {
        Type TypeDto { get; set; }
        IMapper _mapper { get; set; }
        IValidatorFactory _validationFactory { get; set; }
        UnprocessableEntityObjectResult UnprocessableEntity(object error);
    }

    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BaseController<TEntity, TEntityDto> : ControllerBase, IBaseController
        where TEntity : class, IBaseEntity
        where TEntityDto : class, IBaseEntityDto
    {
        public IMapper _mapper { get; set; }
        public IValidatorFactory _validationFactory { get; set; }

        protected readonly IUnitOfWork<IChatAppDbContext> _uow;
        protected readonly IRepository<TEntity> _repository;

        public Type TypeDto { get; set; }

        public BaseController(IMapper mapper, IUnitOfWork<IChatAppDbContext> uow, IValidatorFactory validationFactory)
        {
            _mapper = mapper;
            _uow = uow;
            _validationFactory = validationFactory;
            _repository = _uow.GetRepository<TEntity>();
            TypeDto = typeof(List<TEntityDto>);
        }

        /// <summary>
        /// Get all by query options.
        /// </summary>
        /// <returns>A list of records.</returns>
        [HttpGet]
        public virtual IActionResult Get()
        {
            var list =  _repository.GetAll();
            return Ok(list);
        }


        /// <summary>
        /// Get a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific record.</returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            TEntity entity = _repository.GetByIdAsNoTracking(id);

            if (entity is null)
                return NotFound();

            TEntity result = await Task.FromResult(entity);

            TEntityDto dto = _mapper.Map<TEntityDto>(result);

            return Ok(dto);
        }

        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <returns>A newly created record.</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntityDto entityDto)
        {
            TEntity entity = _mapper.Map<TEntity>(entityDto);

            _repository.Add(entity);
            await _uow.Commit();

            entityDto = _mapper.Map<TEntityDto>(entity);

            return CreatedAtAction(WebRequestMethods.Http.Get, new { id = entityDto.Id }, entityDto);
        }

        /// <summary>
        /// Updates a record.
        /// </summary>
        /// <returns>No Content.</returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute]int id, [FromBody]TEntityDto entityDto)
        {
            if (entityDto.Id != id)
                return BadRequest();

            TEntity entity = _repository.GetById(id);
            if (entity is null)
                return NotFound();

            _mapper.Map(entityDto, entity);

            _repository.Update(entity);

            await _uow.Commit();

            return Ok(_mapper.Map(entity, entityDto));
        }


        /// <summary>
        /// Deletes a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A deleted record.</returns>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            TEntity entity = _repository.GetById(id);

            if (entity is null)
                return NotFound();

            _repository.Delete(entity);
            await _uow.Commit();

            TEntityDto entityDto = _mapper.Map<TEntityDto>(entity);

            return Ok(entityDto);
        }

    }
}