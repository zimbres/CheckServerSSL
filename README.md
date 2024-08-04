# Check Server SSL

REST API endpoint that establishes a TCP connection to an SSL/TLS server, sends data, and returns the server's response. Additionally, the API should retrieve and include the server's certificate details in the response.

---

### Response example:

```json

{
  "serverResponse": "HTTP/1.0 400 Bad Request",
  "certificate": {
    "commonName": "*.google.com",
    "issuer": "CN=WR2, O=Google Trust Services, C=US",
    "subject": "CN=*.google.com",
    "notBefore": "2024-07-30T09:32:53-03:00",
    "notAfter": "2024-10-22T09:32:52-03:00",
    "thumbprint": "A95208E0FC37B46B5FCFC5ABC410C7D6004DDC69",
    "serialNumber": "718DF8A4D1488A7809CCED27107D8184",
    "isExpired": false
  }
}

```