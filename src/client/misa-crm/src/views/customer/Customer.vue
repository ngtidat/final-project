<template>
    <TheTopbar>
        <div class="topbar-left d-flex align-items-center">
            <div class="other-option d-flex align-items-center">
                <span class="icon icon-folder"></span>
                <div class="other-option-title">Tất cả khách hàng</div>
                <span class="icon icon-angle-down"></span>
            </div>
            <div class="update">Sửa</div>
            <div class="reload d-flex align-items-center justify-content-center" @click="handleReload">
                <span class="icon icon-reload"></span>
            </div>
            <div v-if="selectedItems.length > 0" class="select-item">
                Đã chọn {{ selectedItems.length }}
            </div>

            <div v-if="selectedItems.length > 0" class="select-item">
                <a class="delete-item" @click="handleDeleteSelected">Xóa hàng đã chọn</a>
            </div>
        </div>

        <div class="topbar-right d-flex align-items-center justify-content-end">
            <div class="search-box d-flex align-items-center bg-gradient">
                <div class="icon-search-box cursor-pointer">
                    <span class="icon icon-smart-search"></span>
                </div>
                <div class="flex1 cursor-pointer">
                    <input class="input" type="text" placeholder="Tìm kiếm thông minh"
                        @input="handleSearchChange($event.target.value)">
                </div>
                <img src="../../assets/images/icon-ai.svg" alt="" class="icon-search-box">
            </div>
            <div class="tooltip wrap-icon wrap-icon-statistic d-flex justify-content-center align-items-center cursor-pointer bg-gradient">
                <span class="icon icon-statistic"></span>
            </div>
            <MsButton />
            <div class="tooltip import d-flex justify-content-center align-items-center cursor-pointer"
                @click="openFileDialog">
                <div class="d-flex justify-content-center align-items-center">
                    <span class="icon icon-import"></span>
                </div>
                <div>Nhập từ Excel</div>
                <input type="file" ref="fileInput" style="display:none" accept=".csv" @change="handleFileChange">
            </div>
            <div v-if="selectedItems.length > 0"
                class="tooltip import d-flex justify-content-center align-items-center cursor-pointer"
                @click="handleExportSelected">
                <div class="d-flex justify-content-center align-items-center">
                    <span class="icon icon-export"></span>
                </div>
                <div>Xuất ra Excel</div>
                <input type="file" ref="fileInput" style="display:none" accept=".csv">
            </div>
            <div class="tooltip wrap-icon d-flex justify-content-center align-items-center cursor-pointer">
                <span class="icon icon-dot-menu"></span>
            </div>
            <div class="tooltip dropdown-menu d-flex justify-content-center align-items-center cursor-pointer">
                <span class="icon icon-category"></span>
                <span class="icon icon-angle-down"></span>
            </div>
        </div>
    </TheTopbar>

    <!-- Table and pagination -->
    <div class="main-content flex1 d-flex flex-direction-column">
        <MsTable ref="msTableRef" :columns="columns" :rows="customers" :total-count="totalRecords"
            :current-page="pageIndex" :page-size="pageSize" @edit-row="handleEditRow"
            @selection-change="handleSelection" @page-change="handlePageChange"
            @page-size-change="handlePageSizeChange">
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
import { ref, onMounted, computed, watch, inject } from 'vue';
import { useRouter } from 'vue-router'
import MsButton from '../../components/MsButton.vue';
import TheTopbar from '../../layouts/TheTopbar.vue';
import MsTable from '../../components/MsTable.vue';
import { customerService } from '../../services/customerService.js';

const router = useRouter()

const toast = inject('toast');
if (!toast) throw new Error('Toast not provided!');

// Data
const customers = ref([]);
const loading = ref(false);
const error = ref(null);

const pageIndex = ref(1);
const pageSize = ref(100);
const strSearch = ref('');
const sortColumn = ref(null);
const sortDirection = ref(0);

const totalRecords = ref(0);
const selectedItems = ref([])

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

const handleEditRow = (row) => {
    router.push({
        name: 'update-customer',
        params: { id: row.customerId }
    });
};

const handleSelection = (rows) => {
    selectedItems.value = rows
};

const msTableRef = ref(null)
function handleReload() {
    pageIndex.value = 1;

    selectedItems.value = [];

    msTableRef.value.clearSelection()

    fetchCustomers();
}

async function handleDeleteSelected() {
    if (!selectedItems.value.length) return;

    const confirmDelete = confirm(`Bạn có chắc muốn xóa ${selectedItems.value.length} khách hàng?`);
    if (!confirmDelete) return;

    try {
        // Lấy mảng id từ các hàng đã chọn
        const ids = selectedItems.value.map(item => item.customerId);

        console.log(ids);

        // Gọi API xóa nhiều
        await customerService.deleteMulti(ids);

        toast.open("Xóa thành công!", "success", 2000)

        // Load lại dữ liệu và reset selection
        selectedItems.value = [];
        msTableRef.value.clearSelection();
        fetchCustomers();
    } catch (error) {
        console.error(error);
        toast.open("Đã có lỗi xảy ra!. Xóa thất bại!", "error", 2000)
    }
}

// Import 
const fileInput = ref(null);

function openFileDialog() {
    fileInput.value.click();
}

async function handleFileChange(event) {
    const file = event.target.files[0];
    if (!file) return;

    const formData = new FormData();
    formData.append('file', file);

    try {
        await customerService.import(formData);
        toast.open('Import thành công', 'success', 2000)

        // Load lại dữ liệu
        fetchCustomers();
    } catch (err) {
        console.error(err);
        toast.open('Import thất bại', 'error', 2000);
    }

    // reset input để có thể chọn lại file cùng tên
    event.target.value = '';
}

// Export
function handleExportSelected() {
    const escapeCsv = (value) => {
        if (value == null) return '';
        const str = value.toString();
        return /[",\n]/.test(str) ? `"${str.replace(/"/g, '""')}"` : str;
    };

    // Header CSV
    const headers = [
        'Loại khách hàng',
        'Mã khách hàng',
        'Tên khách hàng',
        'Email',
        'Số điện thoại',
        'Địa chỉ',
        'Mã số thuế',
        'Lĩnh vực',
        'Giới tính',
        'Số điện thoại khác',
        'Ngày mua hàng gần nhất',
        'Hàng hóa đã mua',
        'Tên hàng hóa đã mua',
        'Địa chỉ giao hàng'
    ];

    // Rows CSV
    const csvRows = selectedItems.value.map(c => [
        escapeCsv(c.customerType?.customerTypeName || ''),
        escapeCsv(c.customerId),
        escapeCsv(c.customerName),
        escapeCsv(c.customerEmail),
        escapeCsv(c.customerPhone),
        escapeCsv(c.customerAddress),
        escapeCsv(c.customerTaxCode),
        escapeCsv(c.customerIndustry),
        escapeCsv(c.gender ?? ''),
        escapeCsv(c.otherPhoneNumber),
        escapeCsv(c.lastPurchaseDate ? new Date(c.lastPurchaseDate).toLocaleDateString() : ''),
        escapeCsv(c.purchaseItems),
        escapeCsv(c.purchaseItemName),
        escapeCsv(c.shippingAddress)
    ].join(','));

    const csvContent = "\uFEFF" + [headers.join(','), ...csvRows].join('\n');

    // Tạo blob và download
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.setAttribute('download', `SelectedCustomers_${Date.now()}.csv`);
    link.click();
}
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
.reload,
.select-item {
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
    border-radius: 4px;
}

.search-box::before,
.wrap-icon-statistic::before {
    content: "";
    position: absolute;
    inset: 0;
    padding: 1px;
    border-radius: inherit;

    background: linear-gradient(251deg, #9F73F1 24.05%, #4262F0 71.93%);

    -webkit-mask:
        linear-gradient(#fff 0 0) content-box,
        linear-gradient(#fff 0 0);
    -webkit-mask-composite: xor;
            mask-composite: exclude;

    pointer-events: none;
}


::-ms-input-placeholder {
    color: rgb(66, 98, 240);
    opacity: 1;
}

.bg-gradient {
    background: linear-gradient(90deg, rgba(66, 98, 240, .1) 0%, rgba(159, 115, 241, .1) 100%), #fff;
}

.search-box:focus-within {
    background: #fff;
}

.search-box,
.wrap-icon-statistic {
    position: relative;
}

::placeholder {
    color: rgb(66, 98, 240);
    opacity: 1;
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

.icon-import,
.icon-export {
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

.delete-item {
    color: red;
    cursor: pointer;
    text-decoration: underline;
}
</style>