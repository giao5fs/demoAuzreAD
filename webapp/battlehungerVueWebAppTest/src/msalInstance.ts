import * as msal from "@azure/msal-browser";
import { msalConfig } from "./authConfig";

export const msalInstance = new msal.PublicClientApplication(msalConfig);
