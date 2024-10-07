import { LogLevel, PublicClientApplication } from "@azure/msal-browser";

// Config object to be passed to Msal on creation
export const msalConfig = {
  auth: {
    clientId: "70c87435-8ae8-4808-bc8a-b031649b4d68",
    authority:
      "https://login.microsoftonline.com/3b94e066-71eb-447b-b43a-9a739d4f2544",
    redirectUri: "https://jolly-desert-02bab8710.5.azurestaticapps.net", // Must be registered as a SPA redirectURI on your app registration
  },
  cache: {
    cacheLocation: "localStorage",
  },
  system: {
    loggerOptions: {
      loggerCallback: (
        level: LogLevel,
        message: string,
        containsPii: boolean
      ) => {
        if (containsPii) {
          return;
        }
        switch (level) {
          case LogLevel.Error:
            console.error(message);
            return;
          case LogLevel.Info:
            console.info(message);
            return;
          case LogLevel.Verbose:
            console.debug(message);
            return;
          case LogLevel.Warning:
            console.warn(message);
            return;
          default:
            return;
        }
      },
      logLevel: LogLevel.Verbose,
    },
  },
};

export const msalInstance = new PublicClientApplication(msalConfig);

// Add here scopes for id token to be used at MS Identity Platform endpoints.
export const loginRequest = {
  scopes: ["User.Read"],
};

// Add here the endpoints for MS Graph API services you would like to use.
export const graphConfig = {
  graphMeEndpoint: "https://graph.microsoft.com/v1.0/me",
};
