using Microsoft.AspNetCore.Mvc;
using BuberBreakfast.Contract.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Controller;


public class BreaKfastController : ApiController
{
    //Dependecy injection
    private readonly IBreakfastService _breakfastService;

    public BreaKfastController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
        // save brakfast in db
       ErrorOr<Created> createBreakfastResult =  _breakfastService.CreateBreakfast(breakfast);    

        if (createBreakfastResult.IsError) {
            return Problem(createBreakfastResult.Errors);
        }

        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new {id = breakfast.Id},
            value: MapBreakfastResponse(breakfast) 
        );
    }
    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfst => Ok(MapBreakfastResponse(breakfst)),
            errors => Problem(errors)
        );
    }


    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
        ErrorOr<Updated> updatedBreakfast = _breakfastService.UpsertBreakfast(breakfast);
        // TODO: return 201 if a new breakfast was created
        return updatedBreakfast.Match(
            updated => NoContent(),
            errors => Problem(errors)
        );
    }
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
       ErrorOr<Deleted> deletedBreakfast = _breakfastService.DeleteBreakfast(id);

        return deletedBreakfast.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

        private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
    }
}