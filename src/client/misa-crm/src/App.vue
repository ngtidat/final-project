<script setup>
import TheHeader from './layouts/TheHeader.vue';
</script>

<template>
  <div>
    <!-- Header -->
    <div class="header">
      <TheHeader />
    </div>
    <!-- Container -->
    <div class="container d-flex flex-direction-column">
      <!-- Router -->
       <router-view />
    </div>
  </div>
</template>

<style scoped>
.container {
  overflow: hidden;
}
</style>
