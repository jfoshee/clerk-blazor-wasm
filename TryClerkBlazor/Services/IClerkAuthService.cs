namespace TryClerkBlazor.Services;

public interface IClerkAuthService
{
    Task InitializeAsync();
    Task<bool> IsUserSignedInAsync();
}
