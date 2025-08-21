namespace CleanArchitecture.API.Exceptions;

public class BadRequestException(string message) : ApplicationException(message);