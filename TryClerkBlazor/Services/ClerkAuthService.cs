using Microsoft.JSInterop;

namespace TryClerkBlazor.Services;

public class ClerkAuthService : IClerkAuthService
{
    private readonly IJSRuntime _js;

    public ClerkAuthService(IJSRuntime js) => _js = js;

    public Task<bool> IsUserSignedInAsync() =>
        _js.InvokeAsync<bool>("clerkInterop.isUserSignedIn").AsTask();
}
