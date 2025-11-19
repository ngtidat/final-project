<script setup>
import TheHeader from './layouts/TheHeader.vue';
import TheTopbar from './layouts/TheTopbar.vue';
</script>

<template>
  <div>
    <!-- Header -->
    <div class="header">
      <TheHeader />
    </div>
    <!-- Container -->
    <div class="container d-flex flex-direction-column">
      <!-- Top bar -->
      <TheTopbar/>
    </div>
  </div>
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}

.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}

.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
