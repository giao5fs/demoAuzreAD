<template>
  <div v-if="state.resolved">
    <pre>
      {{ state.data }}
    </pre>
  </div>
</template>

<script setup lang="ts">
import { useMsalAuthentication } from "@/composition-api/useMsalAuthentication";
import { InteractionType } from "@azure/msal-browser";
import { reactive, watch } from "vue";
import { loginRequest } from "@/authConfig";
import { callApi } from "@/utils/ApiCall";

const { result, acquireToken } = useMsalAuthentication(
  InteractionType.Redirect,
  loginRequest
);

const state = reactive({
  resolved: false,
  data: {},
});

async function getAPIData() {
  if (result.value) {
    const graphData = await callApi(result.value.accessToken).catch(() =>
      acquireToken()
    );
    state.data = graphData;
    state.resolved = true;
  }
}

getAPIData();

watch(result, () => {
  getAPIData();
});
</script>
