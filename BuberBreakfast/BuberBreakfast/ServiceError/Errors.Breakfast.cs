using ErrorOr;
namespace BuberBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {
        public static Error InvalidName => Error.Validation(
           code: "Breakfast.InvalidName",
           description: $"Breakfast name must be at least {Models.Breakfast.MinNameLength} characters long and {Models.Breakfast.MaxNameLength} max"
        );
        public static Error InvalidDescription => Error.Validation(
        code: "Breakfast.InvalidDescription",
        description: $"Description name must be at least {Models.Breakfast.MinDescriptionLength} characters long and {Models.Breakfast.MaxDescriptionLength} max"
        );
        public static Error NotFound => Error.NotFound(
        code: "Breakfast.Not Found",
        description: "Breakfast not found"
        );
    }
}