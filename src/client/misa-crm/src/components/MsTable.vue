<template>
    <div class="table-container flex1">
        <table>
            <thead>
                <tr>
                    <!-- Checkbox chọn tất cả -->
                    <th class="checkbox">
                        <div class="d-flex">
                            <input type="checkbox" v-model="selectAll" @change="toggleSelectAll" />
                        </div>
                    </th>

                    <th v-for="col in columns" :key="col.key">
                        {{ col.label }}
                    </th>
                </tr>
            </thead>

            <tbody>
                <tr v-for="(row, rowIndex) in rows" :key="rowIndex" @click="$emit('row-click', row)">
                    <!-- Checkbox từng dòng -->
                    <td class="checkbox">
                        <input type="checkbox" :value="row" v-model="selectedRows" @click.stop />
                    </td>

                    <td v-for="col in columns" :key="col.key">
                        <!-- Custom type with slot -->
                        <template v-if="col.type === 'custom'">
                            <slot :name="col.key" :row="row" :col="col" :value="row[col.key]">
                                {{ handleFormat(row[col.key], 'text') }}
                            </slot>
                        </template>

                        <!-- Other types -->
                        <template v-else>
                            {{ handleFormat(row[col.key], col.type || 'text') }}
                        </template>
                    </td>
                </tr>
            </tbody>
        </table>

        <div v-if="rows.length <= 0" class="no-data">
            Không có bản ghi nào
        </div>
    </div>

    <!-- Pagination -->
    <div v-if="rows.length > 0" class="pagination d-flex align-items-center justify-content-space-between">
        <div class="pagination-left d-flex align-items-center justify-content-center">
            <div class="icon-wrapper">
                <span class="icon icon-setting-pagination"></span>
            </div>
            <div class="number-record">
                Tổng số:
                <br>
                <strong>{{ totalCount }}</strong>
            </div>
            <div class="pagination-left-item">
                Công nợ:
                <br>
                0
            </div>
        </div>

        <div class="pagination-right d-flex align-items-center">
            <select name="page-size" id="page-size" class="page-size-option" v-model.number="localPageSize"
                @change="emitPageSize">
                <option value="100" selected>100 bản ghi trên trang</option>
                <option value="50">50 bản ghi trên trang</option>
                <option value="20">20 bản ghi trên trang</option>
                <option value="10">10 bản ghi trên trang</option>
            </select>

            <div class="right-icon-wrapper ">
                <i class="icon icon-previous-start cursor-pointer pagination-right-item"
                    :class="{ disabled: localPage === 1 }" @click="emitPrevStart"></i>
            </div>

            <div class="right-icon-wrapper ">
                <i class="icon icon-previous cursor-pointer pagination-right-item"
                    :class="{ disabled: localPage === 1 }" @click="emitPrev"></i>
            </div>

            <div class="pagination-right-item range-records">
                {{ startRecord }} - {{ endRecord }} bản ghi
            </div>

            <div class="right-icon-wrapper ">
                <i class="icon icon-next cursor-pointer pagination-right-item"
                    :class="{ disabled: localPage === totalPages }" @click="emitNext"></i>
            </div>

            <div class="right-icon-wrapper ">
                <i class="icon icon-next-last cursor-pointer pagination-right-item"
                    :class="{ disabled: localPage === totalPages }" @click="emitNextLast"></i>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, computed, watch } from 'vue'

import { formatDate, formatText } from '../utils/formatter'

const props = defineProps({
    columns: {
        type: Array,
        required: true,
        validator: (value) => {
            return value.every(field => {
                const validTypes = ['text', 'date', 'custom'];
                return field.key &&
                    field.label &&
                    validTypes.includes(field.type || 'text');
            });
        }
    },
    rows: { type: Array, required: true },
    totalCount: { type: Number, default: 0 },
    currentPage: { type: Number, default: 1 },
    pageSize: { type: Number, default: 25 }
})

const handleFormat = (value, type) => {
    switch (type) {
        case 'date':
            return formatDate(value);
        case 'text':
            return formatText(value);
        default:
            return formatText(value);
    }
};

const emit = defineEmits(['row-click', 'selection-change', 'page-change', 'page-size-change'])

const selectAll = ref(false)
const selectedRows = ref([])

const localPage = ref(props.currentPage)

const localPageSize = ref(props.pageSize)

const totalPages = computed(() => Math.ceil(props.totalCount / localPageSize.value))

const startRecord = computed(() => (localPage.value - 1) * localPageSize.value + 1)
const endRecord = computed(() =>
    Math.min(localPage.value * localPageSize.value, props.totalCount)
)

function emitNext() {
    if (localPage.value < totalPages.value) {
        localPage.value++
        emit('page-change', localPage.value)
    }
}

function emitNextLast() {
    if (localPage.value < totalPages.value) {
        localPage.value = totalPages.value
        emit('page-change', localPage.value)
    }
}

function emitPrev() {
    if (localPage.value > 1) {
        localPage.value--
        emit('page-change', localPage.value)
    }
}

function emitPrevStart() {
    if (localPage.value > 1) {
        localPage.value = 1;
        emit('page-change', localPage.value)
    }
}

function emitPageSize() {
    emit('page-size-change', localPageSize.value)
    localPage.value = 1
}

watch(selectedRows, (newVal) => {
    emit('selection-change', newVal)
})

function toggleSelectAll() {
    if (selectAll.value) {
        selectedRows.value = [...props.rows]
    } else {
        selectedRows.value = []
    }
}

watch(() => props.rows, () => {
    selectedRows.value = []
    selectAll.value = false
    emit('selection-change', selectedRows.value)
})

function clearSelection() {
    selectedRows.value = []
    selectAll.value = false
    emit('selection-change', selectedRows.value)
}

// Expose method cho cha
defineExpose({ clearSelection })
</script>

<style scoped>
.table-container {
    overflow: auto;
    background-color: #ffffff;
}

table {
    width: 100%;
    border-collapse: collapse;
    white-space: nowrap;
}

thead {
    color: #1f1f1f;
    text-align: left;
}

thead th {
    background-color: #f0f2f4;
    position: sticky;
    top: 0;
    z-index: 2;
    font-size: 14px;
    font-weight: 600;
}

thead th,
tbody td {
    padding: 12px 44px 12px 12px;
}

.checkbox {
    padding-left: 36px;
    padding-right: 0;
}

tbody td {
    border-bottom: 1px solid #e0e0e0;
    font-size: 14px;
    color: #333;
    vertical-align: middle;
}

tbody tr:hover {
    background-color: #e1eeff !important;
    cursor: pointer;
}

input[type="checkbox"] {
    width: 16px;
    height: 16px;
    cursor: pointer;
}

/* Pagination */
.pagination {
    background-color: #fafafa;
    border-bottom-right-radius: 4px;
    border-bottom-left-radius: 4px;
    font-size: 14px !important;
    flex: 0 0 1;
    height: 56px;
}

.pagination-left {
    padding-left: 12px;
}

.pagination-right {
    justify-content: flex-end;
}

.page-size-option {
    border: 1px solid #e0e0e0;
    background-color: #ffffff;
    border-radius: 4px;
    padding: 7px 16px 7px 12px !important;
    margin-right: 20px;
}

.page-size-option:hover {
    box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.1) !important;
    background-color: #ffffff !important;
    border-color: #2a7efc !important;
    cursor: pointer;
}

.pagination-icon-down {
    padding-left: 10px;
}

.pagination-right-item.disabled {
    opacity: 0.5 !important;
    pointer-events: none !important;
}

.icon-wrapper:hover {
    background-color: #e2e4e9;
    border-radius: 50%;
    cursor: pointer;
}

.number-record {
    margin-right: 44px;
}

.icon-wrapper {
    padding: 4px;
    margin-right: 20px;
}

.right-icon-wrapper {
    margin-right: 8px;
    padding: 4px;
}

.no-data {
    text-align: center;
    padding: 32px 0;
    color: #888;
    font-size: 14px;
}
</style>
