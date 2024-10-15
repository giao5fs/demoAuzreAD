<template>
  <div class="welcome-name">
    <span v-if="!!name">Welcome, {{ name }}</span>
    <FontAwesomeIcon :icon="faUserAstronaut" color="blue"></FontAwesomeIcon>
  </div>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useMsal } from "../composition-api/useMsal";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { faCircle } from "@fortawesome/free-regular-svg-icons";
import { faUserAstronaut } from "@fortawesome/free-solid-svg-icons";

const { accounts } = useMsal();

const name = computed(() => {
  if (accounts.value.length > 0) {
    const name = accounts.value[0].name;
    if (name) {
      return name.split(" ")[0];
    }
  }
  return "";
});
</script>

<style>
.welcome-name {
  font-family: sans-serif;
  color: black;
  font-weight: bold;
  display: flex;
  justify-content: space-between;
  gap: 5px;
  cursor: pointer;
  :hover {
    opacity: 70%;
  }
}
</style>
