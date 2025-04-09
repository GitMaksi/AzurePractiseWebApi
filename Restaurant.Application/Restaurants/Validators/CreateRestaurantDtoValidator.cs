using FluentValidation;
using Restaurant.Application.Restaurants.Dtos;

namespace Restaurant.Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
            RuleFor(restaurantDto => restaurantDto.Name)
                .Length(3,300)
                .WithMessage("Name lenght must be between 3 and 300 characters");
            
            RuleFor(restaurantDto => restaurantDto.Description)
                .NotEmpty()
                .WithMessage("Description is required");
            
            RuleFor(restaurantDto => restaurantDto.Category)
                .NotEmpty()
                .WithMessage("Category is required");
            
            RuleFor(restaurantDto => restaurantDto.ContactEmail)
                .EmailAddress()
                .WithMessage("ContactEmail should be a valid email address");
            
            RuleFor(restaurantDto => restaurantDto.ContactNumber)
                .Matches("^(\\+48\\s?)?(\\d{3}[\\s\\-]?\\d{3}[\\s\\-]?\\d{3})$")
                .WithMessage("ContactNumber should be a valid phone number");
            
            RuleFor(restaurantDto => restaurantDto.PostalCode)
                .Matches("^\\d{2}-\\d{3}$")
                .WithMessage("Postal code should be in format XX-XXX");
    }
} 