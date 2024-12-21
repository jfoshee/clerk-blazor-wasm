using Microsoft.AspNetCore.Components;

namespace TryClerkBlazor.Services;

public interface IClerkAuthService
{
    Task InitializeAsync();
    Task<bool> IsUserSignedInAsync();
    Task MountUserButtonAsync(ElementReference element);
    Task MountSignInAsync(ElementReference element);
}
