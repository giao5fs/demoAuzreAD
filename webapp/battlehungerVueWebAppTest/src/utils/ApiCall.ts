import { apiConfig } from "@/authConfig";

export async function getDataApi1(accessToken: string) {
  const headers = new Headers();
  const bearer = `Bearer ${accessToken}`;

  headers.append("Authorization", bearer);

  const options = {
    method: "GET",
    headers: headers,
  };

  return fetch(apiConfig.resourceUri + "/weatherforecast", options)
    .then((response) => response.json())
    .catch((error) => {
      console.log(error);
      throw error;
    });
}

export async function getDataApi2ViaManagedIdentity(accessToken: string) {
  const headers = new Headers();
  const bearer = `Bearer ${accessToken}`;

  headers.append("Authorization", bearer);

  const options = {
    method: "GET",
    headers: headers,
  };

  return fetch(apiConfig.resourceUri + "/home", options)
    .then((response) => response.json())
    .catch((error) => {
      console.log(error);
      throw error;
    });
}
