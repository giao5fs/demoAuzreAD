<template>
  <div v-if="state.resolved">
    <pre>{{ state.data }}</pre>
  </div>
</template>

<script setup lang="ts">
import { useMsal } from "../composition-api/useMsal";
import {
  InteractionRequiredAuthError,
  InteractionStatus,
} from "@azure/msal-browser";
import { reactive, onMounted, watch } from "vue";
import { loginRequest } from "@/authConfig";
import { callApi } from "@/utils/ApiCall";

const { instance, inProgress } = useMsal();

const state = reactive({
  resolved: false,
  data: {},
});

async function getGraphData() {
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
    const resposeData = await callApi(response.accessToken);
    state.data = resposeData;
    state.resolved = true;
    stopWatcher();
  }
}

onMounted(() => {
  getGraphData();
});

const stopWatcher = watch(inProgress, () => {
  if (!state.resolved) {
    getGraphData();
  }
});
</script>
