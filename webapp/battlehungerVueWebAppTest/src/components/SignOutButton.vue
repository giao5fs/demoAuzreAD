<template>
  <WelcomeName />
  <button @click="logoutPopup">Logout Popup</button>
  <button @click="logoutRedirect">Logout Redirect</button>
</template>

<script setup lang="ts">
import { useMsal } from "../composition-api/useMsal";
import WelcomeName from "./WelcomeName.vue";

const { instance } = useMsal();

const logoutPopup = () => {
  instance.logoutPopup({
    mainWindowRedirectUri: "/",
  });
};

const logoutRedirect = () => {
  instance.logoutRedirect();
};
</script>
