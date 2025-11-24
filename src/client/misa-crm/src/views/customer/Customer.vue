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

            <template v-if="selectedItems.length > 0">
                <div class="select-item">
                    Đã chọn {{ selectedItems.length }}
                </div>

                <div class="select-item d-flex align-items-center justify-content-center"
                    @click="openCustomerTypePopup">
                    <div class="change-customer-type">Gán loại khách hàng</div>
                </div>

                <div class="select-item">
                    <a class="delete-item" @click="handleDeleteSelected">Xóa hàng đã chọn</a>
                </div>
            </template>
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
            <div
                class="tooltip wrap-icon wrap-icon-statistic d-flex justify-content-center align-items-center cursor-pointer bg-gradient">
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

    <MsConfirmPopup :visible="deleteConfirm.visible" :message="deleteConfirm.message" @confirm="handleConfirmDelete"
        @cancel="deleteConfirm.visible = false" />

    <div v-if="importResult.visible" class="import-popup">
        <div class="popup-content">
            <h3>Kết quả Import</h3>
            <p>Tổng số bản ghi: {{ importResult.total }}</p>
            <p>Thành công: {{ importResult.success }}</p>
            <p>Thất bại: {{ importResult.failed }}</p>

            <div v-if="importResult.errors.length">
                <h4>Danh sách lỗi:</h4>
                <ul>
                    <li v-for="err in importResult.errors" :key="err.rowIndex">
                        Dòng {{ err.rowIndex }}: {{ err.error }}
                    </li>
                </ul>
            </div>

            <button @click="importResult.visible = false" class="">Đóng</button>
        </div>
    </div>

    <div v-if="assignPopup.visible" class="assign-popup">
        <div class="assign-popup-content">
            <h3>Gán loại khách hàng</h3>

            <label for="">Chọn loại khách hàng:</label>
            <select v-model="assignPopup.selectedTypeId" class="dropdown">
                <option value="">-- Chọn loại khách hàng --</option>
                <option v-for="ct in customerTypes" :key="ct.customerTypeId" :value="ct.customerTypeId">
                    {{ ct.customerTypeName }}
                </option>
            </select>

            <div class="actions">
                <button @click="confirmAssign" class="btn-primary">Xác nhận</button>
                <button @click="assignPopup.visible = false" class="btn-secondary">Hủy</button>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, computed, watch, inject, reactive } from 'vue';
import { useRouter } from 'vue-router'
import MsButton from '../../components/MsButton.vue';
import TheTopbar from '../../layouts/TheTopbar.vue';
import MsTable from '../../components/MsTable.vue';
import { customerService } from '../../services/customerService.js';
import MsConfirmPopup from '../../components/MsConfirmPopup.vue';
import { customerTypeService} from '../../services/customerTypeService.js';

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

const msTableRef = ref(null)

const deleteConfirm = reactive({
    visible: false,
    message: ''
});

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

function handleReload() {
    pageIndex.value = 1;

    selectedItems.value = [];

    msTableRef.value.clearSelection()

    fetchCustomers();
}

function handleDeleteSelected() {
    if (!selectedItems.value.length) return;

    deleteConfirm.message = `Bạn có chắc muốn xóa ${selectedItems.value.length} khách hàng?`;
    deleteConfirm.visible = true;
}

async function handleConfirmDelete() {
    try {
        const ids = selectedItems.value.map(item => item.customerId);
        await customerService.deleteMulti(ids);

        toast.open("Xóa thành công!", "success", 2000);
        selectedItems.value = [];
        msTableRef.value.clearSelection();
        fetchCustomers();
    } catch (err) {
        console.error(err);
        toast.open("Xóa thất bại!", "error", 2000);
    } finally {
        deleteConfirm.visible = false;
    }
}

// Import 
const fileInput = ref(null);

const importResult = reactive({
    visible: false,
    total: 0,
    success: 0,
    failed: 0,
    errors: []
});

function openFileDialog() {
    fileInput.value.click();
}

async function handleFileChange(event) {
    const file = event.target.files[0];
    if (!file) return;

    const formData = new FormData();
    formData.append('file', file);

    try {
        const res = await customerService.import(formData);

        importResult.total = res.data.data.total;
        importResult.success = res.data.data.success;
        importResult.failed = res.data.data.failed;
        importResult.errors = res.data.data.errors || [];

        importResult.visible = true;

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

// Popup gán loại khách hàng
const assignPopup = reactive({
    visible: false,
    selectedTypeId: ''
});

const customerTypes = ref([]);

async function openCustomerTypePopup() {
    if (!selectedItems.value.length) {
        toast.open("Bạn chưa chọn khách hàng nào!", "warning", 1500);
        return;
    }

    // Load loại khách hàng
    const res = await customerTypeService.getAll();
    customerTypes.value = res.data.data;

    assignPopup.visible = true;
}

async function confirmAssign() {
    if (!assignPopup.selectedTypeId) {
        toast.open("Vui lòng chọn loại khách hàng!", "error", 1500);
        return;
    }

    const ids = selectedItems.value.map(x => x.customerId);

    try {
        await customerService.changeCustomerTypeMany(ids, assignPopup.selectedTypeId);

        toast.open("Gán loại khách hàng thành công!", "success", 2000);

        assignPopup.visible = false;
        selectedItems.value = [];
        msTableRef.value.clearSelection();
        fetchCustomers();

    } catch (err) {
        toast.open("Gán loại thất bại!", "error", 2000);
        console.error(err);
    }
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

.change-customer-type {
    height: 32px;
    font-weight: 500;
    font-size: 13px;
    background-color: #ffffff;
    border-radius: 4px;
    border: 1px solid #d3d7de !important;
    cursor: pointer;
    padding: 4px 8px;
    color: #1f2229;
}

.change-customer-type:hover {
    background-color: #f0f2f4;
}

.icon {
    background-color: rgb(77 80 83);
}

.search-box {
    width: 282px;
    height: 32px;
    border-radius: 4px;
}

.search-box,
.wrap-icon-statistic {
    position: relative;
}

.search-box::before,
.wrap-icon-statistic::before {
    content: "";
    position: absolute;
    inset: 0;
    padding: 1px 0.5px 1.3px 1.8px;
    border-radius: inherit;
    background: linear-gradient(45deg, #9F73F1 24.05%, #4262F0 71.93%);

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

::placeholder {
    color: rgb(66, 98, 240);
    opacity: 1;
}

.input {
    border: none;
    background-color: inherit;
    width: 100%;
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

/* Import result */
.import-popup {
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

.popup-content {
    background: #fff;
    padding: 20px;
    border-radius: 8px;
    width: 500px;
    max-height: 70vh;
    overflow-y: auto;
}

.import-popup button {
    background-color: #4262F0;
    color: white;
    border: none;
    border-radius: 6px;
    padding: 8px 16px;
    font-size: 14px;
    font-weight: 500;
    cursor: pointer;
    transition: background-color 0.2s, transform 0.1s;
    margin-top: 16px;
    display: block;
    margin-left: auto;
}

.import-popup button:hover {
    background-color: #3149c6;
}

.import-popup button:active {
    background-color: #233aa0;
}

/* Change customer type popup */
.assign-popup {
    position: fixed;
    inset: 0;
    background: rgba(0, 0, 0, 0.25);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 9999;
}

.assign-popup .assign-popup-content {
    width: 400px;
    background: white;
    padding: 24px;
    border-radius: 8px;
}

.assign-popup-content h3 {
    padding-bottom: 16px;
}

.dropdown {
    width: 100%;
    padding: 8px;
    margin-top: 8px;
    border: 1px solid #ccc;
    border-radius: 4px;
}

.actions {
    margin-top: 20px;
    display: flex;
    justify-content: flex-end;
    gap: 12px;
}

.btn-primary {
    background: #4262f0;
    color: #fff;
    padding: 8px 16px;
    border-radius: 4px;
    border: none;
    cursor: pointer;
}

.btn-secondary {
    background: #ddd;
    padding: 8px 16px;
    border-radius: 4px;
    border: none;
    cursor: pointer;
}
</style>