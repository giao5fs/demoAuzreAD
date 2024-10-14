<template>
  <nav class="nav-container">
    <router-link to="/"><button>Home</button></router-link>
    <router-link to="/profile"
      ><button>Profile protected by Guard</button></router-link
    >
    <router-link to="/profilenoguard"
      ><button>Profile protected no Guard</button></router-link
    >
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
  justify-content: right;
  gap: 10px;
  flex-direction: row;
  flex-wrap: nowrap;
}
</style>
