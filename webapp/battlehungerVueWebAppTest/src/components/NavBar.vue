<template>
  <nav class="nav-container">
    <router-link to="/">Home</router-link>
    <router-link to="/profile">Profile protected by Guard</router-link>
    <router-link to="/profilenoguard">Profile protected no Guard</router-link>
    <SignOutButton v-if="isAuthenticated" />
    <SignInButton v-else />
  </nav>
</template>

<script setup lang="ts">
import { useIsAuthenticated } from "../composition-api/useIsAuthenticated";
import SignInButton from "./SignInButton.vue";
import SignOutButton from "./SignOutButton.vue";

const isAuthenticated = useIsAuthenticated();
</script>

<style>
a {
  text-decoration: none;
  color: #fff;
}
#authButton {
  margin-left: auto;
}

.nav-container {
  display: flex;
  justify-content: space-between;
  gap: 10px;
  flex-direction: row;
  flex-wrap: nowrap;
}
</style>
