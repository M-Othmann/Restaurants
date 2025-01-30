using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Command.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");

        var searchPhraseLower = request.SearchPhrase.ToLower();
        var resResult = (await restaurantsRepository.GetAllAsync())
                            .Where(r => r.Name.ToLower().Contains(searchPhraseLower)
                                || r.Description.ToLower().Contains(searchPhraseLower));

        //var restaurantDto = resResult.Select(RestaurantDto.FromEntity);
        var restaurantDto = mapper.Map<IEnumerable<RestaurantDto>>(resResult);


        return restaurantDto!;
    }
}
