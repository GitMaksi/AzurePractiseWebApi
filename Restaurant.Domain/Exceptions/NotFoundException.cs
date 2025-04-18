namespace Restaurant.Doman.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
}