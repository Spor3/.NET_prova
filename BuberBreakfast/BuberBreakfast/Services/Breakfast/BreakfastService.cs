using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        //Store data db, repo, memory.
        _breakfasts.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if(_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        //Query data
       return Errors.Breakfast.NotFound;
        
    }
    public ErrorOr<Deleted> DeleteBreakfast(Guid id) {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Updated> UpsertBreakfast(Breakfast breakfast) {
        _breakfasts[breakfast.Id] = breakfast;

        return Result.Updated;
    }
}