<script setup lang="ts">
import { RouterLink, RouterView, useRoute } from "vue-router";
import DefaultLayout from "./components/DefaultLayout.vue";
import EmptyLayout from "./components/EmptyLayout.vue";
import { computed, onMounted } from "vue";

const route = useRoute();
const layout = computed(() => route.meta.layout ?? EmptyLayout);
</script>

<template>
  <component :is="layout">
    <RouterView v-slot="{ Component }" :key="route.fullPath">
      <component :is="Component"></component>
    </RouterView>
  </component>
</template>

<style scoped></style>
