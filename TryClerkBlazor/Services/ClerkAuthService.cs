using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TryClerkBlazor.Services;

public class ClerkAuthService(IJSRuntime js) : IClerkAuthService
{
    private readonly IJSRuntime _js = js;

    public Task InitializeAsync() =>
        _js.InvokeVoidAsync("Clerk.load").AsTask();

    public Task<bool> IsUserSignedInAsync() =>
        _js.InvokeAsync<bool>("clerkInterop.isUserSignedIn").AsTask();

    public Task<string> GetUserToken() =>
        _js.InvokeAsync<string>("Clerk.session.getToken").AsTask();

    public Task MountUserButtonAsync(ElementReference element) =>
        _js.InvokeVoidAsync("Clerk.mountUserButton", element).AsTask();

    public Task MountSignInAsync(ElementReference element) =>
        _js.InvokeVoidAsync("Clerk.mountSignIn", element).AsTask();
}
