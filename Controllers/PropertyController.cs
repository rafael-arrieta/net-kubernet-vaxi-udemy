using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetKubernet.Data.Properties;
using NetKubernet.Dtos.PropertyDtos;
using NetKubernet.Middleware;
using NetKubernet.Models;

namespace NetKubernet.Controllers;

[Route("api/[controller]")]
[ApiController]

public class PropertyController : ControllerBase
{
    private readonly IPropertyRepository _repository;

    private IMapper _mapper;
    public PropertyController(
        IPropertyRepository repository,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyResponseDto>>> GetProperties(){
        var properties = await _repository.GetAllProperties();
        return Ok(_mapper.Map<IEnumerable<PropertyResponseDto>>(properties));
    }

    [HttpGet("{id}", Name="GetInmuebleById")]
    public async Task<ActionResult <PropertyResponseDto>> GetPropertyById(int id){
        var property = await _repository.GetPropertyById(id);

        if(property is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.NotFound,
                new {message = $"the property ID {id} is not found in the database"}
            );
        }
        return Ok(_mapper.Map<PropertyResponseDto>(property));
    }

    [HttpPost]
    public async Task <ActionResult<PropertyResponseDto>> 
    CreateProperty( [FromBody] PropertyRequestDto property)
    {
        var propertyModel = _mapper.Map<Property>(property);
        await _repository.CreateProperty(propertyModel);
        await _repository.SaveChanges();

        var propertyResponse = _mapper.Map<PropertyResponseDto>(propertyModel);

        return CreatedAtRoute(nameof(GetPropertyById), 
        new {propertyResponse.Id}, propertyResponse);
    }

    [HttpDelete("{id}")]
    private async Task<ActionResult> DeleteProperty(int id)
    {
        await _repository.DeleteProperty(id);
        await _repository.SaveChanges();

        return Ok();
    }
}