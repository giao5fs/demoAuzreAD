<template>
  <div class="home">
    <span v-if="!isAuthenticated"
      >Please sign-in to see your profile information.</span
    >
    <div v-else>
      <div class="button-group">
        <button @click="goToProfile">Goto Profile</button>
        <LoadingButton @click="callApi1" :loading="processing1"
          >Call API1</LoadingButton
        >
        <LoadingButton @click="callApi2" :loading="processing2"
          >Call API2</LoadingButton
        >
      </div>
      <div>
        <pre>{{ dataObj }}</pre>
      </div>
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
import LoadingButton from "../components/LoadingButton.vue";

const isAuthenticated = useIsAuthenticated();

const processing1 = ref(false);
const processing2 = ref(false);

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
  processing1.value = true;
  let token = await getTokenData();
  console.log("callApi1: ", token);
  if (inProgress.value === InteractionStatus.None) {
    dataObj.value = await getDataApi1(token);
    processing1.value = false;
  }
};

const callApi2 = async () => {
  processing2.value = true;
  let token = await getTokenData();
  console.log("callApi2: ", token);
  if (inProgress.value === InteractionStatus.None) {
    dataObj.value = await getDataApi2ViaManagedIdentity(token);
    processing2.value = false;
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
