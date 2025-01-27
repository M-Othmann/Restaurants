using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Command.UpdateRestaurant;

public class UpdateResturantByIdCommandHandler(ILogger<UpdateResturantByIdCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<UpdateResturantByIdCommand>
{
    public async Task Handle(UpdateResturantByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating restaurant with id: {request.Id}");

        var restaurant = await restaurantsRepository.GetRestaurant(request.Id);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        mapper.Map(request, restaurant);

        /*        restaurant.Name = request.Name;
                restaurant.Description = request.Description;
                restaurant.HasDelivery = request.HasDelivery;*/

        await restaurantsRepository.SaveChanges();

    }
}
