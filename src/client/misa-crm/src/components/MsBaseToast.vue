<template>
    <teleport to="body">
        <transition name="fade">
            <div v-if="visible" class="toast" :class="typeClass">
                {{ message }}
            </div>
        </transition>
    </teleport>

</template>

<script setup>
import { ref, watch, computed } from "vue";

const props = defineProps({
    modelValue: Boolean,
    message: String,
    type: {
        type: String,
        default: "success"
    },
    duration: {
        type: Number,
        default: 2000
    }
});

const emit = defineEmits(["update:modelValue"]);

const visible = ref(false);
let timeoutId = null;

watch(
    () => props.modelValue,
    (val) => {
        // Nếu toast được mở
        if (val) {
            // Clear timeout cũ nếu có
            if (timeoutId) clearTimeout(timeoutId);

            // Reset visible để chạy lại animation
            visible.value = false;
            setTimeout(() => {
                visible.value = true;
            }, 0); // delay 0ms là đủ

            // Tự tắt sau duration
            timeoutId = setTimeout(() => {
                emit("update:modelValue", false);
            }, props.duration);
        }
        else {
            visible.value = false;
        }
    }
);

const typeClass = computed(() =>
    props.type === "success" ? "toast-success" : "toast-error"
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
    z-index: 9999;
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
</style>
