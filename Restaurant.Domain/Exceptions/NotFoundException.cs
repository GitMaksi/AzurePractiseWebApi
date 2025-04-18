namespace Restaurant.Doman.Exceptions;

public class NotFoundException(string resourceType, string resourceId)
    : Exception($"Resource of type: {resourceType} and Identifier: {resourceId} does not exist.")
{
}