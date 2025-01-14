using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Command.UpdateRestaurant;

public class UpdateResturantByIdCommandHandler(ILogger<UpdateResturantByIdCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<UpdateResturantByIdCommand, bool>
{
    public async Task<bool> Handle(UpdateResturantByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating restaurant with id: {request.Id}");

        var restaurant = await restaurantsRepository.GetRestaurant(request.Id);

        if (restaurant is null)
            return false;

        mapper.Map(request, restaurant);

        /*        restaurant.Name = request.Name;
                restaurant.Description = request.Description;
                restaurant.HasDelivery = request.HasDelivery;*/

        await restaurantsRepository.SaveChanges();
        return true;
    }
}
