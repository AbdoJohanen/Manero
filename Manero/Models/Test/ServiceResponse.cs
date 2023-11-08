using Manero.Enums;

namespace Manero.Models.Test;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public StatusCode Status { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}
