namespace EventAPI.Exceptions;

public class NotFoundException(string message) : Exception(message);