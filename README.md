## Blazor 🔒 Clerk

This demonstrates how to use Clerk with Blazor WebAssembly.

Clerk provides vanilla JavaScript SDKs for authentication. We simply use JSInterop to call the Clerk SDKs from Blazor.

We provide the same sort of [components](./TryClerkBlazor/Components/) as the Clerk React SDK, but for Blazor.

- `<SignedIn>`: Show content only if the user is signed in.
- `<SignedOut>`: Show content only if the user is signed out.
- `<UserButton>`: Small user button to facilitate signing out, account management, etc.
- `<SignIn>`: Sign in form.
