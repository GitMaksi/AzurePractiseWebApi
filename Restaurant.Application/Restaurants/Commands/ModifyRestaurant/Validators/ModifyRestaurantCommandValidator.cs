using FluentValidation;

namespace Restaurant.Application.Restaurants.Commands.ModifyRestaurant.Validators;

public class ModifyRestaurantCommandValidator : AbstractValidator<ModifyRestaurantCommand>
{
    public ModifyRestaurantCommandValidator()
    {
        RuleFor(command => command.Name)
            .Length(3,300)
            .WithMessage("Name lenght must be between 3 and 300 characters");
        
        RuleFor(command => command.Description)
            .NotEmpty()
            .WithMessage("Description is required");
        
    }
}