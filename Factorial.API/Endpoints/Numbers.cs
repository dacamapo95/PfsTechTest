using Carter;
using Contracts;
using Factorial.API.Services.Factorial;
using Microsoft.AspNetCore.Mvc;

namespace Factorial.API.Endpoints;

public class Numbers : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/numbers")
            .WithTags(nameof(Numbers))
            .WithOpenApi();

        group.MapGet("/factorial", GetFactorial);

        group.MapPost("/sort", SortNumbers);
    }

    public IResult GetFactorial(INumberService numberService, [FromQuery] int number)
    {
        var result = numberService.CalculateFactorial(number);

        return result.Success
            ? Results.Ok(new FactorialResponse(result.Data))
            : Results.BadRequest(result.ErrorMessage);
    }

    public IResult SortNumbers([FromServices] INumberService numberService, [FromBody] int[] numbers)
    {
        if (numbers.Length == 0)
        {
            return Results.BadRequest("At least one number is required.");
        }

        var sortedNumbers = numberService.Sort(numbers);
        return Results.Ok(sortedNumbers);
    }
}
