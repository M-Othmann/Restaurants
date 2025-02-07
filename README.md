# Restaurants Api
---

The Restaurants API is a web application built with ASP.NET Core following the principles of Clean Architecture that allows users to manage restaurants and their associated dishes. It provides endpoints for creating, reading, updating, and deleting restaurants and dishes, as well as user authentication and authorization.

---

## Project Structure
---
- `src/Restaurants.API`: The main API project. This is the entry point of the application.
- `src/Restaurants.Application`: Contains the application logic. This layer is responsible for the application's behavior and policies.
- `src/Restaurants.Domain`: Contains enterprise logic and types. This is the core layer of the application.
- `src/Restaurants.Infrastructure`: Contains infrastructure-related code such as database and file system interactions. This layer supports the higher layers.
- `tests/Restaurants.API.Tests`: Contains unit tests for the API.

---

## Technologies used
---
- **ASP.NET Core**: Framework for building the web API.
- **MediatR**: For implementing the CQRS pattern.
- **AutoMapper**: For object-to-object mapping.
- **Serilog**: For structured logging.
- **Swagger/OpenAPI**: For API documentation.
- **JWT Authentication**: For securing API endpoints
---

## API Endpoints
---

1. GET /api/restaurants
    - Parameters: searchPhrase, pageSize, pageNumber, sortBy, sortDirection
    - Authorization Bearer token

2. GET /api/restaurants/{id}
    - Parameters: id
    - Authorization: Bearer token

3. GET /api/restaurants/{id}/dishes
    - Parameters: id
    - Authorization: Bearer token

4. DELETE /api/restaurants/{id}/dishes
    - Parameters: id

5. GET /api/restaurants/{id}/dishes/{dishId}
    - Parameters: id, dishId

6. DELETE /api/restaurants/{id}
    - Parameters: id
    - Authorization: Bearer token

7. POST /api/restaurants
    - Body: JSON object with properties Name, Description, Category, HasDelivery, ContactEmail, ContactNumber, City, Street
    - Authorization: Bearer token
