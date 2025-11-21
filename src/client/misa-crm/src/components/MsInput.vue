<template>
    <slot>
        <div class="d-flex flex1 flex-direction-column">
            <template v-if="type === 'select'">
                <select v-model="value" :class="{ error: showError && !isValid }">
                    <option v-for="opt in options" :key="opt.value" :value="opt.value">
                        {{ opt.label }}
                    </option>
                </select>
            </template>

            <template v-else-if="type === 'textarea'">
                <textarea v-model="value" :placeholder="placeholder"
                    :class="{ error: showError && !isValid }"></textarea>
            </template>

            <template v-else>
                <input v-model="value" :type="type" :placeholder="placeholder"
                    :class="{ error: showError && !isValid }" />
            </template>

            <div v-if="showError && required && !isValid" class="error-text">
                Không được để trống
            </div>
        </div>
    </slot>
</template>

<script setup>
import { ref, watch } from 'vue'

const props = defineProps({
    type: { type: String, default: 'text' },
    placeholder: { type: String, default: '' },
    required: { type: Boolean, default: false },
    options: { type: Array, default: () => [] },
    modelValue: { type: [String, Number], default: '' },
    fieldKey: { type: String, required: false }
})

const emit = defineEmits(['update:modelValue', 'validate'])

const value = ref(props.modelValue)
const isValid = ref(true)
const showError = ref(false)

// cập nhật giá trị khi người dùng nhập
watch(value, (val) => {
    emit('update:modelValue', val)
})

// Hàm kiểm tra hợp lệ, được gọi từ component cha khi ấn submit
function validate() {
    if (props.required) {
        isValid.value = value.value?.toString().trim() !== ''
    } else {
        isValid.value = true
    }

    showError.value = true
    emit('validate', { field: props.fieldKey, isValid: isValid.value })
    return isValid.value
}

// Cho phép cha gọi phương thức validate()
defineExpose({ validate })
</script>

<style scoped>
input,
textarea,
select {
    padding: 2px 16px;
    border-radius: 3px;
    border: 1px solid #dddde4;
    font-size: 14px !important;
}

.error {
    border: 1px solid red;
}

.error-text {
    color: red;
}
</style>
