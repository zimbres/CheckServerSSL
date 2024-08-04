namespace CheckServerSSL.Services;

public class SslClient
{
    private readonly ILogger<SslClient> _logger;

    public SslClient(ILogger<SslClient> logger)
    {
        _logger = logger;
    }

    public async Task<Response> SendCommandAsync(string server, int port, string command)
    {
        try
        {
            using TcpClient client = new(server, port);
            using SslStream sslStream = new(client.GetStream(), false,
                new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true), null);
            await sslStream.AuthenticateAsClientAsync(server);

            byte[] commandBytes = Encoding.UTF8.GetBytes(command + "\r\n");
            await sslStream.WriteAsync(commandBytes);
            await sslStream.FlushAsync();

            using StreamReader reader = new(sslStream, Encoding.UTF8);
            string serverResponse = await reader.ReadToEndAsync();

            var response = new Response
            {
                ServerResponse = serverResponse.Trim(),
                Certificate = GetCertificateDetails(sslStream.RemoteCertificate)
            };
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception: {ex.Message}", ex.Message);
            return null!;
        }
    }

    private Certificate GetCertificateDetails(X509Certificate certificate)
    {
        if (certificate is X509Certificate2 cert)
        {
            string commonName = GetCommonName(cert.Subject);

            var response = new Certificate
            {
                CommonName = commonName,
                Issuer = cert.Issuer,
                Subject = cert.Subject,
                NotBefore = cert.NotBefore,
                NotAfter = cert.NotAfter,
                SerialNumber = cert.SerialNumber,
                Thumbprint = cert.Thumbprint,
            };
            return response;
        }
        else
        {
            _logger.LogWarning("The certificate is not an X509Certificate2 instance.");
            return null!;
        }
    }

    private static string GetCommonName(string subject)
    {
        const string cnPrefix = "CN=";
        string[] parts = subject.Split(',');

        foreach (string part in parts)
        {
            if (part.Trim().StartsWith(cnPrefix))
            {
                return part.Trim().Substring(cnPrefix.Length);
            }
        }
        return "Unknown";
    }
}
