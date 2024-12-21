using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TryClerkBlazor.Services;

public class ClerkAuthService(IJSRuntime js) : IClerkAuthService
{
    private readonly IJSRuntime _js = js;

    public Task InitializeAsync() =>
        _js.InvokeVoidAsync("clerkInterop.initializeClerk").AsTask();

    public Task<bool> IsUserSignedInAsync() =>
        _js.InvokeAsync<bool>("clerkInterop.isUserSignedIn").AsTask();

    public Task MountUserButtonAsync(ElementReference element) =>
        _js.InvokeVoidAsync("clerkInterop.mountUserButton", element).AsTask();

    public Task MountSignInAsync(ElementReference element) =>
        _js.InvokeVoidAsync("clerkInterop.mountSignIn", element).AsTask();
}
