<template>
    <div class="base-input">
        <!-- Content: input mặc định hoặc custom -->
        <slot>
            <input
                class="input-control"
                :value="modelValue"
                @input="handleInput"
                @blur="$emit('blur')"
            >
        </slot>

        <!-- Error -->
        <p v-if="error" class="error-msg">{{ error }}</p>
    </div>
</template>

<script setup>
defineProps({
    modelValue: [String, Number, null],
    error: String
});

const emit = defineEmits(["update:modelValue", "blur", "input"]);

function handleInput(e) {
    emit("update:modelValue", e.target.value);
    emit("input", e.target.value);
}
</script>

<style scoped>
.base-input {
    display: flex;
    flex-direction: column;
    flex: 1;
}

.input-control {
    height: 32px;
    padding: 0 8px;
    border: 1px solid #d3d7de;
    border-radius: 4px;
    font-size: 13px;
    width: 100%;
}

.error-msg {
    color: red;
    font-size: 12px;
    margin-top: 4px;
}
</style>
