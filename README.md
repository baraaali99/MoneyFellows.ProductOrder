# MoneyFellows.ProductOrder

## Overview

**MoneyFellows.ProductOrder** is a .NET Core 8 Web API project built with Clean Architecture principles. This API manages product orders, providing CRUD operations for products and orders. The project aims to maintain high code quality, separation of concerns, and ease of maintenance.

## Table of Contents

1. [Project Structure](#project-structure)
2. [Prerequisites](#prerequisites)
3. [Setup and Installation](#setup-and-installation)
4. [Usage](#usage)
5. [API Endpoints](#api-endpoints)
   - [Products](#products)
   - [Orders](#orders)
6. [Sample JSON](#sample-json)
   - [Product](#product)
   - [Order](#order)
7. [Testing](#testing)
8. [Technology Stack](#technology-stack)
   
## Project Structure

The project follows the Clean Architecture pattern, organized into the following layers:

- **Core**: Contains domain entities, business rules, and interfaces.
- **Application**: Includes MediatR commands, queries, and handlers.
- **Infrastructure**: Implements repositories, database context, and external services.
- **API**: Exposes endpoints, handles HTTP requests, and serializes responses.

## Prerequisites

- .NET Core 8 SDK (latest version)
- Visual Studio or JetBrains Rider
- SQL Server.
- IIS Express (for local development)

## Setup and Installation

1. **Clone the repository**:
    ```sh
    git clone https://github.com/baraaali99/MoneyFellows.ProductOrder.git
    cd MoneyFellows.ProductOrder
    git checkout dev
    ```

2. **Restore dependencies**:
    ```sh
    dotnet restore
    ```

3. **Update database connection settings**:
    Configure the database connection in `appsettings.json` under the `MoneyFellows.ProductOrder.API` project.

4. **Build the project**:
    ```sh
    dotnet build
    ```

5. **Run the project** (This will automatically apply any pending migrations):
    ```sh
    dotnet run --project MoneyFellows.ProductOrder.API
    ```

## Usage

To use the API, send HTTP requests to the appropriate endpoints.

## API Endpoints

### Products

- **Create Product**: `POST /api/v1/products`
- **Get All Products**: `GET /api/v1/products`
- **Get Product by ID**: `GET /api/v1/products/{id}`
- **Update Product**: `PUT /api/v1/products/{id}`
- **Delete Product**: `DELETE /api/v2/products/{id}`

### Orders

- **Create Order**: `POST /api/v1/orders`
- **Get All Orders**: `GET /api/v1/orders`
- **Get Order by ID**: `GET /api/v1/orders/{id}`
- **Update Order**: `PUT /api/v1/orders/{id}`
- **Delete Order**: `DELETE /api/v1/orders/{id}`

## Sample JSON

### Product

```json
{
  "productName": "iPhone 14",
  "productDescription": "The latest iPhone with A15 Bionic chip, 5G capability, and improved camera system.",
  "productImage": "Url.Com",
  "price": 999,
  "merchant": "Apple Store"
}


```

### Order

```json
{
  "deliveryAddress": "123 Apple St, Cupertino, CA",
  "totalCost": 1999,
  "orderDetails": [
    {
      "productId": 1,
      "quantity": 2
    }
  ],
  "customerDetails": {
    "name": "John Doe",
    "email": "john.doe@example.com",
    "contactNumber": "123-456-7890"
  },
  "deliveryTime": "2024-06-01T10:00:00"
}
```
## Testing

You can test the API endpoints using Swagger. Once the project is running, navigate to `/swagger` in your browser (e.g., `http://localhost:5000/swagger`). Swagger provides a user-friendly interface for interacting with the API, making it easy to send requests and view responses without needing a separate tool.

## Technology Stack

- **.NET Core 8**: Framework for building the API.
- **Entity Framework Core**: ORM for database interactions.
- **MediatR**: Library for CQRS implementation.
- **Serilog**: Logging library for structured and centralized logging.
- **IIS Express**: Web server for local development.


