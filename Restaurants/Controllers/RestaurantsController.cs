using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Command.CreateRestaurant;
using Restaurants.Application.Restaurants.Command.DeleteRestaurant;
using Restaurants.Application.Restaurants.Command.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);


        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto?>> GetRestaurant([FromRoute] int id)
    {
        var result = await mediator.Send(new GetRestaurantByIdQuery(id));

        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateResturantByIdCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));




        return NoContent();
    }


    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {

        int id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetRestaurant), new { id }, null);
    }


}
