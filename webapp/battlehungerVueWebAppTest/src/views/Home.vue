<template>
  <div class="home">
    <span v-if="!isAuthenticated"
      >Please sign-in to see your profile information.</span
    >
    <div v-else class="button-group">
      <button @click="goToProfile">Goto Profile</button>
      <button @click="callApi1">Call API1</button>
      <button @click="callApi2">Call API2</button>
    </div>
  </div>
  <pre>
    {{ dataObj }}
  </pre>
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

const state = reactive({
  resolved: false,
  token: "",
});

const dataObj = ref();

const router = useRouter();

const stopWatcher = watch(inProgress, () => {
  if (!state.resolved) {
    getTokenData();
  }
});

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

  if (inProgress.value === InteractionStatus.None) {
    state.token = response.accessToken;
    state.resolved = true;
    stopWatcher();
  }
  console.log(state.token);
};

const callApi1 = async () => {
  console.log("callApi1: ", state.token);
  if (inProgress.value === InteractionStatus.None) {
    dataObj.value = await getDataApi1(state.token);
  }
};

const callApi2 = async () => {
  console.log("callApi2: ", state.token);
  if (inProgress.value === InteractionStatus.None) {
    dataObj.value = await getDataApi2ViaManagedIdentity(state.token);
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
