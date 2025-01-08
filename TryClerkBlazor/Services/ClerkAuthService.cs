using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TryClerkBlazor.Services;

public class ClerkAuthService(IJSRuntime js) : IClerkAuthService
{
    private readonly IJSRuntime _js = js;

    public Task InitializeAsync() =>
        _js.InvokeVoidAsync("Clerk.load").AsTask();

    public async Task<bool> IsUserSignedInAsync()
    {
        // In JS we could just check whether Clerk.user is defined,
        // but we cannot execute arbitrary JS code from C#.
        // And I do not want to rely on interop scripts to be present in the page.
        // So we use JS interop to effectively check for the existence of Clerk.user
        // by trying to invoke the `getSessions` method.
        // We expect this method doesn't make any calls to Clerk's API,
        // so it shouldn't result in extra traffic or delay.
        try
        {
            await _js.InvokeAsync<object>("Clerk.user.getSessions").AsTask();
            return true;
        }
        catch (JSException e)
        {
            if (e.Message.Contains("'getSessions'"))
                return false;
            throw;
        }
    }

    public async Task<string?> GetUserToken()
    {
        if (await IsUserSignedInAsync())
        {
            return await _js.InvokeAsync<string>("Clerk.session.getToken");
        }
        else
        {
            return null;
        }
    }

    public Task MountUserButtonAsync(ElementReference element) =>
        _js.InvokeVoidAsync("Clerk.mountUserButton", element).AsTask();

    public Task MountSignInAsync(ElementReference element) =>
        _js.InvokeVoidAsync("Clerk.mountSignIn", element).AsTask();
}
