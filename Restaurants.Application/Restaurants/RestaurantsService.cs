using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
    ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
{
    public async Task<int> Create(CreateRestaurantDto dto)
    {
        logger.LogInformation("Creating a new restaurant");

        var restaurant = mapper.Map<Restaurant>(dto);

        int id = await restaurantsRepository.Create(restaurant);

        return id;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var resResult = await restaurantsRepository.GetAllAsync();

        //var restaurantDto = resResult.Select(RestaurantDto.FromEntity);
        var restaurantDto = mapper.Map<IEnumerable<RestaurantDto>>(resResult);


        return restaurantDto!;
    }

    public async Task<RestaurantDto?> GetRestaurant(int id)
    {
        logger.LogInformation($"Getting restaurant {id}");

        var res = await restaurantsRepository.GetRestaurant(id);

        //var restuarantDto = RestaurantDto.FromEntity(res);
        var restuarantDto = mapper.Map<RestaurantDto?>(res);


        return restuarantDto;
    }
}
