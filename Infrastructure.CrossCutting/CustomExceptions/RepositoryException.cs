namespace Infrastructure.CrossCutting.CustomExceptions
{
    using System;

    public class RepositoryException : Exception
    {
        public RepositoryException(string methodName, string message, Exception innerException)
            : base($"Error in repository method '{methodName}': {message}", innerException)
        {
            MethodName = methodName;
        }

        public string MethodName { get; }
    }
}
