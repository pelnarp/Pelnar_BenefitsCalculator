using Api.Dtos.Dependent;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController: EntityControllerBase<Dependent, GetDependentDto>
{
    public DependentsController(IRepository<Dependent> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    override public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        return await base.Get(id);
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    override public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        return await base.GetAll();
    }
}
