<template>
    <TheTopbar>
        <div class="topbar-left d-flex align-items-center">
            <div class="other-option d-flex align-items-center">
                <span class="icon icon-folder"></span>
                <div class="other-option-title">Tất cả khách hàng</div>
                <span class="icon icon-angle-down"></span>
            </div>
            <div class="update">Sửa</div>
            <div class="reload d-flex align-items-center justify-content-center">
                <span class="icon icon-reload"></span>
            </div>
            <div></div>
        </div>

        <div class="topbar-right d-flex align-items-center justify-content-end">
            <div class="search-box d-flex align-items-center">
                <div class="icon-search-box">
                    <span class="icon icon-smart-search"></span>
                </div>
                <div class="flex1">
                    <input class="input" type="text" placeholder="Tìm kiếm thông minh" @input="handleSearchChange($event.target.value)">
                </div>
                <img src="../../assets/images/icon-ai.svg" alt="" class="icon-search-box">
            </div>
            <div class="tooltip wrap-icon d-flex justify-content-center align-items-center">
                <span class="icon icon-statistic"></span>
            </div>
            <MsButton />
            <div class="tooltip import d-flex justify-content-center align-items-center">
                <div class="d-flex justify-content-center align-items-center">
                    <span class="icon icon-import"></span>
                </div>
                <div>Nhập từ Excel</div>
            </div>
            <div class="tooltip wrap-icon d-flex justify-content-center align-items-center">
                <span class="icon icon-dot-menu"></span>
            </div>
            <div class="tooltip dropdown-menu d-flex justify-content-center align-items-center">
                <span class="icon icon-category"></span>
                <span class="icon icon-angle-down"></span>
            </div>
        </div>
    </TheTopbar>

    <!-- Table and pagination -->
    <div class="main-content flex1 d-flex flex-direction-column">
        <MsTable :columns="columns" :rows="customers" :total-count="totalRecords" :current-page="pageIndex"
            :page-size="pageSize" @row-click="handleRowClick" @selection-change="handleSelection"
            @page-change="handlePageChange" @page-size-change="handlePageSizeChange">
            <template #customerName="{ row, value }">
                <div class="d-flex align-items-center gap-2">
                    <span style="color: #4262f0;">{{ value }}</span>
                </div>
            </template>
            <template #customerId="{ row, value }">
                <div class="d-flex align-items-center gap-2">
                    <span style="color: #4262f0;">{{ value }}</span>
                </div>
            </template>
            <template #customerPhone="{ row, value }">
                <div class="d-flex align-items-center gap-2">
                    <template v-if="value">
                        <img src="../../assets/images/icon-phone.png" alt="" class="icon-phone">
                        <span style="color: #4262f0;">{{ value }}</span>
                    </template>
                    <template v-else>--</template>
                </div>
            </template>
            <template #customerType="{ row }">
                {{ row.customerType?.customerTypeName || '--' }}
            </template>
        </MsTable>
    </div>
</template>

<script setup>
import { ref, onMounted, computed, watch } from 'vue';
import MsButton from '../../components/MsButton.vue';
import TheTopbar from '../../layouts/TheTopbar.vue';
import MsTable from '../../components/MsTable.vue';
import { customerService } from '../../services/customerService.js';

// Data
const customers = ref([]);
const loading = ref(false);
const error = ref(null);

const pageIndex = ref(1);
const pageSize = ref(100);
const strSearch = ref('');
const sortColumn = ref(null);
const sortDirection = ref(1);

const totalRecords = ref(0);

// Gom mọi tham số vào 1 computed
const queryParams = computed(() => ({
    pageIndex: pageIndex.value,
    pageSize: pageSize.value,
    search: strSearch.value,
    sortColumn: sortColumn.value,
    sortDirection: sortDirection.value
}));

const fetchCustomers = async () => {
    loading.value = true;
    try {
        const res = await customerService.searchAndPaginate(
            queryParams.value.pageIndex,
            queryParams.value.pageSize,
            queryParams.value.search,
            queryParams.value.sortColumn,
            queryParams.value.sortDirection
        );

        customers.value = res.data.data.items;
        totalRecords.value = res.data.data.totalRecords;
        
        console.log(pageIndex.value);
    } catch (err) {
        error.value = err;
    } finally {
        loading.value = false;
    }
};

watch(queryParams, () => {
    fetchCustomers();
}, { deep: true });

onMounted(fetchCustomers);

// Event
function handlePageChange(newPage) {
    pageIndex.value = newPage;
}

function handlePageSizeChange(newSize) {
    pageSize.value = newSize;
    pageIndex.value = 1;
}

function handleSearchChange(newSearch) {
    strSearch.value = newSearch;
    pageIndex.value = 1;
}

// Table columns
const columns = [
    { key: 'customerType', label: 'Loại khách hàng', type: 'custom' },
    { key: 'customerId', label: 'Mã khách hàng', type: 'custom' },
    { key: 'customerName', label: 'Tên khách hàng', type: 'custom' },
    { key: 'customerTaxCode', label: 'Mã số thuế', type: 'text' },
    { key: 'shippingAddress', label: 'Địa chỉ (Giao hàng)', type: 'text' },
    { key: 'customerPhone', label: 'Điện thoại', type: 'custom' },
    { key: 'lastPurchaseDate', label: 'Ngày mua hàng gần nhất', type: 'date' },
    { key: 'purchaseItems', label: 'Hàng hóa đã mua', type: 'text' },
    { key: 'purchaseItemName', label: 'Tên hàng hóa đã mua', type: 'text' }
];

const handleRowClick = (row) => {
    console.log('Clicked row:', row);
};

const handleSelection = (rows) => {
    console.log('Selected rows:', rows);
};
</script>

<style scoped>
.main-content {
    overflow: auto;
}

.other-option {
    background-color: rgb(255, 255, 255);
    height: 32px;
    border-radius: 4px;
    font-size: 14px;
    font-weight: 600;
    color: #1f2229;
    padding-left: 12px;
}

.other-option:hover {
    cursor: pointer;
}

.update {
    color: #4262f0;
    font-size: 14px;
}

.update:hover {
    cursor: pointer;
    text-decoration: underline;
}

.update,
.reload {
    margin-left: 16px;
}

.reload {
    width: 24px;
    height: 24px;
    border-radius: 50%;
}

.reload:hover {
    cursor: pointer;
    background-color: #c5c9d3;
}

.other-option-title {
    margin: 0 8px;
}

.icon {
    background-color: rgb(77 80 83);
}

.search-box {
    width: 248px;
    height: 32px;
}

.input {
    border: none;
    background-color: inherit;
}

.icon-search-box {
    padding: 6px;
}

.tooltip {
    margin-left: 8px;
}

.wrap-icon,
.search-box,
.import,
.dropdown-menu {
    background-color: rgb(255, 255, 255);
    border-radius: 4px;
    height: 32px;
}

.wrap-icon {
    width: 32px;
    height: 32px;
    border: 1px solid #d3d7de;
}

.import {
    color: #4262f0;
    font-size: 13px;
    font-weight: 600;
    border: #4262f0 1px solid;
    padding-right: 8px;
}

.icon-import {
    background-color: #4262f0;
    margin: 0 8px;
}

.dropdown-menu {
    border: 1px solid #d3d7de;
}

.icon-category {
    margin: 0 8px;
}

.icon-angle-down {
    margin-right: 8px;
}

.icon-phone {
    width: 16px;
    height: 16px;
    margin-right: 4px;
}
</style>