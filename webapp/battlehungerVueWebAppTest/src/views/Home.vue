<template>
  <div class="home">
    <span v-if="!isAuthenticated"
      >Please sign-in to see your profile information.</span
    >
    <div v-else class="button-group">
      <button @click="goToProfile">Goto Profile</button>
      <button @click="callApi1">Call API1</button>
      <button @click="callApi2">Call API2</button>
      <pre>{{ dataObj }}</pre>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from "vue-router";
import { useIsAuthenticated } from "../composition-api/useIsAuthenticated";
import { getDataApi1, getDataApi2ViaManagedIdentity } from "@/utils/ApiCall";
import { useMsal } from "../composition-api/useMsal";
import {
  InteractionRequiredAuthError,
  InteractionStatus,
} from "@azure/msal-browser";
import { onMounted, reactive, ref, watch } from "vue";
import { loginRequest } from "@/authConfig";

const isAuthenticated = useIsAuthenticated();

const { instance, inProgress } = useMsal();

const dataObj = ref();

const router = useRouter();

const getTokenData = async () => {
  const response = await instance
    .acquireTokenSilent({
      ...loginRequest,
    })
    .catch(async (e) => {
      if (e instanceof InteractionRequiredAuthError) {
        await instance.acquireTokenRedirect(loginRequest);
      }
      throw e;
    });

  return response.accessToken;
};

const callApi1 = async () => {
  let token = await getTokenData();
  console.log("callApi1: ", token);
  if (inProgress.value === InteractionStatus.None) {
    dataObj.value = await getDataApi1(token);
  }
};

const callApi2 = async () => {
  let token = await getTokenData();
  console.log("callApi2: ", token);
  if (inProgress.value === InteractionStatus.None) {
    dataObj.value = await getDataApi2ViaManagedIdentity(token);
  }
};

function goToProfile() {
  router.push("/profile");
}
</script>

<style scoped>
.button-group {
  display: flex;
  justify-content: left;
  flex-direction: row;
  flex-wrap: nowrap;
  gap: 10px;
}
</style>
