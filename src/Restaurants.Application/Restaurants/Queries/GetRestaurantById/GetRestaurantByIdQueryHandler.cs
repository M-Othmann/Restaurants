using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository,
    IBlobStorageService blobStorageService) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting restaurant {request.Id}");

        var res = await restaurantsRepository.GetRestaurant(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());


        var restuarantDto = mapper.Map<RestaurantDto>(res);

        restuarantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(res.LogoUrl);

        return restuarantDto;
    }
}
