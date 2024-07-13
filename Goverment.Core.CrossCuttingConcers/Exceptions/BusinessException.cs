namespace Core.CrossCuttingConcerns.Exceptions;

public class BusinessException(string message) : Exception(message);