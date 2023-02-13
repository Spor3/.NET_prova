using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
    public void CreateBreakfast(Breakfast breakfast)
    {
        //Store data db, repo, memory.
        _breakfasts.Add(breakfast.Id, breakfast);
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
    public void DeleteBreakfast(Guid id) {
        _breakfasts.Remove(id);
    }

    public void UpsertBreakfast(Breakfast breakfast) {
        _breakfasts[breakfast.Id] = breakfast;
    }
}