import { LogLevel, PublicClientApplication } from "@azure/msal-browser";

// Config object to be passed to Msal on creation
export const msalConfig = {
  auth: {
    clientId: "99a7629b-929c-45fc-853d-e177736ea35f",
    authority:
      "https://login.microsoftonline.com/d9f9f390-21e0-4122-8bc7-741f92e7211d",
    redirectUri: "https://icy-rock-048660400.5.azurestaticapps.net", // Must be registered as a SPA redirectURI on your app registration
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

export const apiConfig = {
  resourceUri: "https://movelorencia-api1.azurewebsites.net/WeatherForecast",
  resourceScopes: ["02013b40-85bb-4145-9abe-6d9942ee97a0/.default"],
};

export const msalInstance = new PublicClientApplication(msalConfig);

// Add here scopes for id token to be used at MS Identity Platform endpoints.
export const loginRequest = {
  scopes: ["openid", "profile", "offline_access", ...apiConfig.resourceScopes],
};

// Add here scopes for access token to be used at the API endpoints.
export const tokenRequest = {
  scopes: [...apiConfig.resourceScopes],
};

// Add here scopes for silent token request
export const silentRequest = {
  scopes: ["openid", "profile", ...apiConfig.resourceScopes],
};
