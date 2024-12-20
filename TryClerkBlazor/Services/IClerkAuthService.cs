namespace TryClerkBlazor.Services;

public interface IClerkAuthService
{
    Task<bool> IsUserSignedInAsync();
}
