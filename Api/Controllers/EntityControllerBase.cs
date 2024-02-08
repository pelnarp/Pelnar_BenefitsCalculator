using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

/// <summary>
/// This is generic entity controller which simplifies implementation of controllers which return basic entities
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TDto"></typeparam>
public abstract class EntityControllerBase<T, TDto> : ControllerBase where T : WithId
{
    private readonly IRepository<T> repository;
    private readonly IMapper mapper;

    public EntityControllerBase(IRepository<T> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    [SwaggerOperation(Summary = $"Get entity by id")]
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<ApiResponse<TDto>>> Get(int id)
    {
        var item = await repository.Get(id);

        if (item == null)
        {
            return NotFound(ApiResponse<TDto>.NotFoundResponse());
        }

        // We could try/catch here, but I would let it fail and handle by a higher level aspect
        var data = mapper.Map<TDto>(item); // better on a single line for better debuggingn when needed
        return ApiResponse<TDto>.SuccessResponse(data);

    }

    [SwaggerOperation(Summary = "Get all entities")]
    [HttpGet("")]
    public virtual async Task<ActionResult<ApiResponse<List<TDto>>>> GetAll()
    {
        var employees = await repository.GetAll();

        // We could try/catch here, but I would let it fail and handle by a higher level aspect
        var data = mapper.Map<List<TDto>>(employees); // better on a single line for better debuggingn when needed
        return ApiResponse<List<TDto>>.SuccessResponse(data);
    }
}
