<script setup>
import TheHeader from './layouts/TheHeader.vue';
</script>

<template>
  <div class="d-flex flex-direction-column h-100">
    <!-- Header -->
    <div class="header">
      <TheHeader />
    </div>
    <!-- Container -->
    <div class="container flex1 d-flex flex-direction-column">
      <!-- Router -->
       <router-view />
    </div>
  </div>
</template>

<style scoped>
.header {
  height: 48px;
}

.container {
  overflow: hidden;
}
</style>
