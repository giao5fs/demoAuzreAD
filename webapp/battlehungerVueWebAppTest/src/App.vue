<template>
  <div>
    <NavBar />
    <h1>Welcome to summoner rift</h1>
    <router-view />
  </div>
</template>

<script setup lang="ts">
import NavBar from "./components/NavBar.vue";
</script>

<style scoped>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  text-align: center;
  color: #2c3e50;
}
</style>
