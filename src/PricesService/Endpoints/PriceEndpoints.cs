using PricesService.Contracts;
using PricesService.Services;
using Microsoft.AspNetCore.Mvc;

namespace PricesService.Endpoints;

public static class PriceEndpoints
{
    public static void MapPriceEndpoints(this IEndpointRouteBuilder app)
    {
        var prices = app.MapGroup("/prices").WithTags("Price");

        prices.MapGet(string.Empty, async (
            [FromQuery] string? productId,
            [FromQuery] string? storeId,
            IPriceService priceService,
            CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                return Results.BadRequest("The query parameter 'productId' is required.");
            }

            if (!string.IsNullOrWhiteSpace(storeId))
            {
                var price = await priceService.GetByProductAndStoreAsync(productId, storeId, ct);
                return price is null ? Results.NotFound() : Results.Ok(price);
            }

            var allPrices = await priceService.GetByProductIdAsync(productId, ct);
            return Results.Ok(allPrices);
        })
        .WithName("GetPrices")
        .WithSummary("Get prices by productId (and optionally storeId)");

        prices.MapPost(string.Empty, async (
            [FromBody] CreatePriceRequest request,
            IPriceService priceService,
            CancellationToken ct) =>
        {
            try
            {
                var created = await priceService.CreateAsync(request, ct);
                return Results.Created($"/prices/{created.Id}", created);
            }
            catch (ArgumentException ex)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    [ex.ParamName ?? "error"] = [ex.Message]
                });
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { error = ex.Message });
            }
        })
        .WithName("CreatePrice")
        .WithSummary("Create a price for a product in a store");

        prices.MapPut("/{priceId}", async (
            string priceId,
            [FromBody] UpdatePriceRequest request,
            IPriceService priceService,
            CancellationToken ct) =>
        {
            try
            {
                var updated = await priceService.UpdateAsync(priceId, request, ct);
                return updated is null ? Results.NotFound() : Results.Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    [ex.ParamName ?? "error"] = [ex.Message]
                });
            }
        })
        .WithName("UpdatePrice")
        .WithSummary("Update the price value");

        prices.MapDelete("/{priceId}", async (
            string priceId,
            IPriceService priceService,
            CancellationToken ct) =>
        {
            var success = await priceService.DeleteAsync(priceId, ct);
            return success ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeletePrice")
        .WithSummary("Delete a price by id");
    }
}
