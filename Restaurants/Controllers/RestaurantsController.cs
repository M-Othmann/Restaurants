using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Command.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());


        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurant([FromRoute] int id)
    {
        var result = await mediator.Send(new GetRestaurantByIdQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {
        int id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetRestaurant), new { id }, null);
    }


}
