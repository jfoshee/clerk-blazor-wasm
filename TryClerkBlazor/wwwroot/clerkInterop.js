window.clerkInterop = {
  initializeClerk: async () => {
    await Clerk.load();
  },
  isUserSignedIn: () => !!Clerk.user,
  mountUserButton: (selector) => {
    const el = document.querySelector(selector);
    if (el) Clerk.mountUserButton(el);
  },
  mountSignIn: (selector) => {
    const el = document.querySelector(selector);
    if (el) Clerk.mountSignIn(el);
  },
};
