namespace EventAPI.Exceptions;

public class EventInPastException(string message) : Exception(message);