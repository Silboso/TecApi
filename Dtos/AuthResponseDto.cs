namespace TecApi.Dtos;

public class AuthResponseDto
{
    public bool Success { get; set; }
    
    public string? Uid { get; set; }
    
    public string? Token { get; set; }
    
    public string? Message { get; set; }
}