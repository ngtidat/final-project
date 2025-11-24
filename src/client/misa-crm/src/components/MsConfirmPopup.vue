<template>
    <div v-if="visible" class="confirm-popup">
        <div class="popup-content">
            <h3>{{ title }}</h3>
            <p>{{ message }}</p>
            <div class="popup-actions">
                <button class="btn-cancel" @click="cancel">Hủy</button>
                <button class="btn-confirm" @click="confirm">Xác nhận</button>
            </div>
        </div>
    </div>
</template>

<script setup>
import { defineProps, defineEmits } from 'vue';

const props = defineProps({
    visible: { type: Boolean, default: false },
    title: { type: String, default: 'Xác nhận' },
    message: { type: String, required: true }
});

const emit = defineEmits(['confirm', 'cancel']);

function confirm() {
    emit('confirm');
}

function cancel() {
    emit('cancel');
}
</script>

<style scoped>
.confirm-popup {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.3);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 9999;
}

.confirm-popup .popup-content {
    background: #fff;
    padding: 20px;
    border-radius: 8px;
    width: 400px;
    text-align: center;
}

.popup-content p {
    text-align: left;
    margin: 12px 0;
}

.confirm-popup .popup-actions {
    display: flex;
    justify-content: flex-end;
    margin-top: 20px;
    gap: 12px;
}

.confirm-popup .btn-cancel {
    background: #ccc;
    border: none;
    padding: 8px 16px;
    border-radius: 6px;
    cursor: pointer;
}

.confirm-popup .btn-cancel:hover {
    background: #b3b3b3;
}

.confirm-popup .btn-confirm {
    background: #e74c3c;
    color: #fff;
    border: none;
    padding: 8px 16px;
    border-radius: 6px;
    cursor: pointer;
}

.confirm-popup .btn-confirm:hover {
    background: #c0392b;
}
</style>
