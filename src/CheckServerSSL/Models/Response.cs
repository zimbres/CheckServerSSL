namespace CheckServerSSL.Models;

public class Response
{
    public string ServerResponse { get; set; } = string.Empty;
    public Certificate Certificate { get; set; } = new();
}

public class Certificate
{
    public string CommonName { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public DateTime NotBefore { get; set; }
    public DateTime NotAfter { get; set; }
    public string Thumbprint { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public bool IsExpired { get => DateTime.Now > NotAfter; }
}
