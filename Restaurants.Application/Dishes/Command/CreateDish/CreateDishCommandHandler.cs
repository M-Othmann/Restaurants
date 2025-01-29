using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Command.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger
    , IRestaurantsRepository restaurantsRepository, IDishRepository dishRepository,
    IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);

        var restaurant = await restaurantsRepository.GetRestaurant(request.RestaurantId);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        return await dishRepository.Create(dish);



    }
}
