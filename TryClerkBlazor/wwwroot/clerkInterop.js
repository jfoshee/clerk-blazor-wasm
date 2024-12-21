window.clerkInterop = {
  initializeClerk: async () => {
    await Clerk.load();
  },

  isUserSignedIn: () => !!Clerk.user,

  mountUserButton: (element) => {
    Clerk.mountUserButton(element);
  },

  mountSignIn: (element) => {
    Clerk.mountSignIn(element);
  },
};
