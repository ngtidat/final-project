<template>
    <div class="topbar d-flex justify-content-space-between align-items-center">
        <slot></slot>
    </div>
</template>

<script setup></script>

<style scoped>
.topbar{
    min-height: 56px;
    font-size: 13px;
    max-height: 56px;
    padding: 0 16px !important;
    border-bottom: 1px solid #d3d7de;
    background-color: #e2e4e9;
}
</style>