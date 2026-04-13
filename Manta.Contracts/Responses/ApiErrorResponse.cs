namespace Manta.Contracts.Responses;

public class ApiErrorResponse
{
    public string Error { get; set; } = string.Empty; 
    
    public string Message { get; set; } = string.Empty;
    
    public List<ErrorDetail>? Details { get; set; }
    
    public string RequestId { get; set; } = Guid.NewGuid().ToString();
}

public class ErrorDetail
{
    public string Attribute { get; set; } = string.Empty;
    public string Issue { get; set; } = string.Empty;
}