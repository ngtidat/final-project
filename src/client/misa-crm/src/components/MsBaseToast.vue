<template>
    <teleport to="body">
        <transition name="fade">
            <div v-if="toast.state.show" class="toast" :class="toastType">
                {{ toast.state.message }}
            </div>
        </transition>
    </teleport>
</template>

<script setup>
import { computed, inject } from 'vue';

const toast = inject('toast');
if (!toast) throw new Error('Toast not provided!');

const toastType = computed(() =>
    toast.state.type === 'success' ? 'toast-success' : 'toast-error'
);
</script>

<style>
.toast {
    min-width: 240px;
    padding: 12px 16px;
    border-radius: 6px;
    color: #fff;
    font-size: 14px;
    position: fixed;
    right: 24px;
    top: 24px;
    z-index: 999999;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
}

.toast-success {
    background-color: #2E7D32;
}

.toast-error {
    background-color: #D32F2F;
}

.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.3s;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}

.fade-enter-to {
    opacity: 1;
}
</style>
