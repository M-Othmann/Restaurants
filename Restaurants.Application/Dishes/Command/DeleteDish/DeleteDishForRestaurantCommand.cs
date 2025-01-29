using MediatR;

namespace Restaurants.Application.Dishes.Command.DeleteDish;

public class DeleteDishForRestaurantCommand(int restaurantId, int dishId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
}
