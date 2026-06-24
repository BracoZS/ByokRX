namespace BYOK.Core.Exceptions;

/// <summary>Thrown when BYOK is missing or misconfigured (endpoint, API key, etc.).</summary>
public class BYOKConfigurationException : Exception
{
    /// <summary>Creates a new configuration exception with a message.</summary>
    public BYOKConfigurationException(string message) : base(message) { }
    /// <summary>Creates a new configuration exception wrapping an inner exception.</summary>
    public BYOKConfigurationException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>Thrown when routing or fallback execution fails.</summary>
public class BYOKRoutingException : Exception
{
    /// <summary>Creates a new routing exception with a message.</summary>
    public BYOKRoutingException(string message) : base(message) { }
    /// <summary>Creates a new routing exception wrapping an inner exception.</summary>
    public BYOKRoutingException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>Thrown when an argument violates BYOK-specific validation rules.</summary>
public class BYOKArgumentException : ArgumentException
{
    /// <summary>Creates a new argument exception with a message.</summary>
    public BYOKArgumentException(string message) : base(message) { }
    /// <summary>Creates a new argument exception for a specific parameter.</summary>
    public BYOKArgumentException(string message, string paramName) : base(message, paramName) { }
}
