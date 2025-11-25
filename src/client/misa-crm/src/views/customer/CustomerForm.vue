<template>
    <TheTopbar>
        <div class="topbar-left d-flex align-items-center">
            <div class="topbar-title">
                <div v-if="isAdd">Thêm khách hàng</div>
                <div v-else>Sửa thông tin khách hàng</div>
            </div>
            <div class="template d-flex align-items-center justify-content-center">
                <div>Mẫu tiêu chuẩn</div>
                <span class="icon icon-angle-down"></span>
            </div>
            <div class="option-layout d-flex align-items-center justify-content-center">
                <div>Sửa bố cục</div>
            </div>
        </div>

        <div class="topbar-right d-flex">
            <div class="btn-cancel" @click="handleCancel">Hủy bỏ</div>
            <div class="btn-save-add" v-if="isAdd" @click="handleSaveAdd">Lưu và thêm</div>
            <div class="btn-save" @click="handleSave">Lưu</div>
        </div>
    </TheTopbar>

    <!-- Form -->
    <div class="main-content flex1 d-flex flex-direction-column">
        <div class="avatar-title">
            Ảnh
        </div>
        <div class="select-avatar">
            <template v-if="avatarPreview">
                <img :src="avatarPreview" class="avatar-preview cursor-pointer" alt="avatar" @click="triggerFileInput">
                <button class="btn-clear" @click.stop="clearAvatar">×</button>
            </template>
            <template v-else>
                <span class="icon-avatar cursor-pointer" @click="triggerFileInput"></span>
            </template>
            <input type="file" ref="fileInput" accept="image/*" @change="handleFileChange" style="display: none;">
        </div>

        <div class="tilte-form">Thông tin chung</div>

        <form action="">
            <div class="form-container d-flex flex-direction-column">

                <!-- Row 1 -->
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Mã khách hàng</label>
                        <input type="text" disabled :value="currentCustomerId ?? newCustomerId">
                    </div>

                    <div class="d-flex field">
                        <label for="">
                            Tên khách hàng
                            <span class="required">*</span>
                        </label>
                        <MsInput v-model="formData.customerName" :error="errors.customerName"
                            @input="errors.customerName = ''" @blur="() => validateName(formData.customerName)" />
                    </div>
                </div>

                <!-- Row 2 -->
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Số điện thoại</label>
                        <MsInput v-model="formData.customerPhone" :error="errors.customerPhone"
                            @input="errors.customerPhone = ''" @blur="() => validatePhone(formData.customerPhone)" />
                    </div>

                    <div class="d-flex field">
                        <label for="">Email</label>
                        <MsInput v-model="formData.customerEmail" :error="errors.customerEmail"
                            @input="errors.customerEmail = ''" @blur="() => validateEmail(formData.customerEmail)" />
                    </div>
                </div>

                <!-- Row 3 -->
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Giới tính</label>
                        <div class="custom-select" @click="toggleGenderDropdown" :class="{ active: isGenderOpen}">
                            <div class="custom-select-display">
                                {{ selectedGenderLabel }}
                            </div>

                            <span class="icon icon-angle-down"></span>

                            <div class="custom-dropdown" v-if="isGenderOpen">
                                <div class="dropdown-item" @click.stop="selectGender(null, '')">Không chọn</div>
                                <div class="dropdown-item" @click.stop="selectGender(0, 'Nam')">Nam</div>
                                <div class="dropdown-item" @click.stop="selectGender(1, 'Nữ')">Nữ</div>
                            </div>
                        </div>

                    </div>

                    <div class="d-flex field">
                        <label for="">Địa chỉ</label>
                        <MsInput v-model="formData.customerAddress" />
                    </div>
                </div>

                <!-- Row 4 -->
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Lĩnh vực</label>
                        <MsInput v-model="formData.customerIndustry" />
                    </div>

                    <div class="d-flex field">
                        <label for="">Mã số thuế</label>
                        <MsInput v-model="formData.customerTaxCode" />
                    </div>
                </div>

                <!-- Row 5 -->
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Số điện thoại khác</label>
                        <MsInput v-model="formData.otherPhoneNumber" />
                    </div>

                    <div class="d-flex field">
                        <label for="">Loại khách hàng</label>
                        <div class="custom-select" @click="toggleCustomerTypeDropdown" :class="{ active: isCustomerTypeOpen }">
                            <div class="custom-select-display">
                                {{ selectedCustomerTypeLabel }}
                            </div>

                            <span class="icon icon-angle-down"></span>

                            <div class="custom-dropdown" v-if="isCustomerTypeOpen">
                                <div class="dropdown-item" @click.stop="selectCustomerType(null, '')">Không chọn</div>

                                <div class="dropdown-item" v-for="type in customerTypes" :key="type.customerTypeId"
                                    @click.stop="selectCustomerType(type.customerTypeId, type.customerTypeName)">
                                    {{ type.customerTypeName }}
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <!-- Row 6 -->
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Ngày mua gần nhất</label>
                        <MsInput>
                            <input type="date" v-model="formData.lastPurchaseDate">
                        </MsInput>
                    </div>

                    <div class="d-flex field">
                        <label for="">Hàng hóa đã mua</label>
                        <MsInput v-model="formData.purchaseItems" />
                    </div>
                </div>

                <!-- Row 7 -->
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Tên hàng hóa mua</label>
                        <MsInput v-model="formData.purchaseItemName" />
                    </div>

                    <div class="d-flex field">
                        <label for="">Địa chỉ giao hàng</label>
                        <MsInput v-model="formData.shippingAddress" />
                    </div>
                </div>

            </div>
        </form>
    </div>

</template>

<script setup>
import { ref, reactive, onMounted, inject, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { customerTypeService } from '../../services/customerTypeService.js'
import { customerService } from '../../services/customerService.js'
import TheTopbar from '../../layouts/TheTopbar.vue'
import MsInput from '../../components/MsInput.vue'
import { checkEmailFormat, checkPhoneFormat } from '../../utils/validate.js'

const toast = inject('toast');
if (!toast) throw new Error('Toast not provided!');

const route = useRoute();
const router = useRouter();
const isAdd = ref(true);

const customerTypes = ref([]);
const newCustomerId = ref(null);
const error = ref(null);

const fileInput = ref(null);
const avatarPreview = ref(null);
const avatarFile = ref(null);

const originalEmail = ref(null);
const originalPhone = ref(null);

// Form data reactive
const formData = reactive({
    customerName: '',
    customerPhone: '',
    customerEmail: '',
    gender: null,
    customerAddress: '',
    customerTypeId: null,
    customerIndustry: '',
    customerTaxCode: '',
    otherPhoneNumber: '',
    lastPurchaseDate: '',
    purchaseItems: '',
    purchaseItemName: '',
    shippingAddress: '',
    customerAvatar: null,
});

const errors = reactive({
    customerName: "",
    customerEmail: null,
    customerPhone: null
});

const currentCustomerId = route.params.id || null; // lấy từ route nếu có

// Lấy danh sách loại khách hàng
const fetchCustomerTypes = async () => {
    try {
        const res = await customerTypeService.getAll();
        customerTypes.value = res.data.data;
    } catch (err) {
        error.value = err;
    }
}

// Lấy danh sách loại khách hàng
const fetchNewCustomerId = async () => {
    try {
        const res = await customerService.getNewCustomerId();
        newCustomerId.value = res.data.data;
    } catch (err) {
        error.value = err;
    }
}

// Lấy dữ liệu customer nếu đang update
const fetchCustomer = async () => {
    try {
        const res = await customerService.getById(currentCustomerId);
        const data = res.data.data;

        formData.customerName = data.customerName || "";
        formData.customerPhone = data.customerPhone || "";
        formData.customerEmail = data.customerEmail || "";
        formData.gender = data.gender === undefined ? null : data.gender;
        formData.customerAddress = data.customerAddress || "";
        formData.customerIndustry = data.customerIndustry || "";
        formData.customerTaxCode = data.customerTaxCode || "";
        formData.otherPhoneNumber = data.otherPhoneNumber || "";
        formData.lastPurchaseDate = formatDateForInput(data.lastPurchaseDate);
        formData.purchaseItems = data.purchaseItems || "";
        formData.purchaseItemName = data.purchaseItemName || "";
        formData.shippingAddress = data.shippingAddress || "";
        formData.customerTypeId = data.customerType?.customerTypeId || null;
        formData.customerAvatar = data.customerAvatar || null

        avatarPreview.value = data.customerAvatar ?? null;

        originalEmail.value = data.customerEmail;
        originalPhone.value = data.customerPhone;
    } catch (err) {
        console.log(err);
    }
};

function formatDateForInput(dateStr) {
    if (!dateStr) return '';
    const d = new Date(dateStr);
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0'); // Month từ 0-11
    const day = String(d.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
}

onMounted(() => {
    fetchCustomerTypes();

    if (currentCustomerId) {
        isAdd.value = false;
        fetchCustomer();
    } else {
        isAdd.value = true;
        fetchNewCustomerId();
    }
});

function buildPayload() {
    const payload = {
        CustomerName: formData.customerName || '',
        CustomerAddress: formData.customerAddress || null,
        CustomerPhone: formData.customerPhone || null,
        CustomerEmail: formData.customerEmail || null,
        CustomerTaxCode: formData.customerTaxCode || null,
        CustomerTypeId: formData.customerTypeId || null,
        CustomerIndustry: formData.customerIndustry || null,
        Gender: formData.gender == null ? null : Number(formData.gender),
        OtherPhoneNumber: formData.otherPhoneNumber || null,
        LastPurchaseDate: formData.lastPurchaseDate ? new Date(formData.lastPurchaseDate) : null,
        PurchaseItems: formData.purchaseItems || null,
        PurchaseItemName: formData.purchaseItemName || null,
        ShippingAddress: formData.shippingAddress || null,
        Avatar: avatarFile.value || null,
        CustomerAvatar: avatarPreview.value || null
    }

    // Chỉ thêm file nếu có chọn mới
    if (avatarFile.value || avatarPreview.value) {
        payload.Avatar = avatarFile.value;
        payload.CustomerAvatar = avatarPreview.value;
    }

    return payload;
}

function triggerFileInput() {
    fileInput.value.click();
}

function handleFileChange(e) {
    const file = e.target.files[0];
    if (!file) return;

    avatarFile.value = file;
    avatarPreview.value = URL.createObjectURL(file);
}

function clearAvatar() {
    avatarPreview.value = null;
    avatarFile.value = null;
    formData.customerAvatar = null;
    // Reset input file để có thể chọn lại cùng 1 file cũng được
    fileInput.value.value = null;
}

function handleCancel() {
    router.push('/customer')
}

function validateName(name) {
    if (!name || !name.trim()) {
        errors.customerName = name?.trim() ? "" : "Tên không được để trống";
        return false;
    }
    return true;
}

async function validateEmail(email) {
    if (!email) return;

    if (!checkEmailFormat(email)) {
        errors.customerEmail = "Email không đúng format";
        return;
    }

    if (currentCustomerId && email === originalEmail.value) return;

    try {
        const res = await customerService.checkExistEmail(email);
        if (res.data.data) {
            errors.customerEmail = "Email đã tồn tại";
        }
    } catch (err) {
        console.error(err.response?.data);
    }
}

async function validatePhone(phone) {
    if (!phone) return;

    if (!checkPhoneFormat(phone)) {
        errors.customerPhone = "Số điện thoại từ 10-11 số";
        return;
    }

    if (currentCustomerId && phone === originalPhone.value) return;

    try {
        const res = await customerService.checkExistPhone(phone);
        if (res.data.data) {
            errors.customerPhone = "Số điện thoại đã tồn tại";
        }
    } catch (err) {
        console.error(err.response?.data);
    }
}

async function handleSave() {
    if (!validateName(formData.customerName)) return;

    await validateEmail(formData.customerEmail);
    await validatePhone(formData.customerPhone);

    const payload = buildPayload();

    try {
        if (currentCustomerId) {
            await customerService.update(currentCustomerId, payload)
            toast.open("Lưu thành công!", "success", 2000)
        } else {
            await customerService.create(payload)
            toast.open("Lưu thành công!", "success", 2000)
        }

        router.push('/customer');
    } catch (error) {
        console.log(error)
        toast.open("Đã xảy ra lỗi!", "error", 2000)

    }
}

async function handleSaveAdd() {
    if (!validateName(formData.customerName)) return;

    await validateEmail(formData.customerEmail);
    await validatePhone(formData.customerPhone);


    const payload = buildPayload();

    try {
        await customerService.create(payload)

        toast.open("Lưu thành công!", "success", 2000)
        // Reset form
        for (const key of Object.keys(formData)) {
            formData[key] = null;
        }

        fetchNewCustomerId();
    } catch (error) {
        console.error(error)
        toast.open("Đã xảy ra lỗi", "error", 2000)
    }
}

const isGenderOpen = ref(false);

const selectedGenderLabel = computed(() => {
    if (formData.gender === null) return "";
    return formData.gender === 0 ? "Nam" : "Nữ";
});

function toggleGenderDropdown() {
    isGenderOpen.value = !isGenderOpen.value;
}

function selectGender(value, label) {
    formData.gender = value;
    isGenderOpen.value = false;
}

const isCustomerTypeOpen = ref(false);

const selectedCustomerTypeLabel = computed(() => {
    if (!formData.customerTypeId) return "";

    const found = customerTypes.value.find(t => t.customerTypeId === formData.customerTypeId);
    return found?.customerTypeName ?? "";
});

function toggleCustomerTypeDropdown() {
    isCustomerTypeOpen.value = !isCustomerTypeOpen.value;
}

function selectCustomerType(id, name) {
    formData.customerTypeId = id;
    isCustomerTypeOpen.value = false;
}
</script>

<style scoped>
.topbar-title {
    font-size: 20px;
    font-weight: 500;
}

.template {
    font-size: 16px;
    margin-left: 8px;
}

.topbar-title,
.template {
    color: #1f2229;
    font-weight: 600;
    margin-right: 8px;
}

.option-layout {
    color: #4262f0;
    font-size: 14px;
}

.template,
.option-layout,
.topbar-title {
    height: 32px;
}

.icon-angle-down {
    margin-left: 4px;
}

.btn-cancel,
.btn-save-add,
.btn-save {
    border-radius: 4px;
    padding: 5px 16px;
    font-size: 13px;
    line-height: 20px !important;
    font-weight: 500;
    margin-left: 8px;
    cursor: pointer;
}

.btn-cancel,
.btn-save-add {
    background-color: #fff;
}

.btn-cancel {
    color: #1f2229;
    border: 1px solid #d3d7de !important
}

.btn-save-add {
    color: #4262f0;
    border: 1px solid #4262f0
}

.btn-save {
    color: #fff;
    background-color: #4262f0;
}

.main-content {
    padding-top: 32px;
    margin-left: 56px;
    margin-right: 280px;
}

.avatar-title,
.tilte-form {
    font-size: 20px !important;
    font-weight: 500;
    margin-bottom: 12px;
}

.select-avatar {
    margin-bottom: 40px;
    position: relative;
}

.avatar-preview {
    width: 48px;
    height: 48px;
    border-radius: 50%;
    object-fit: cover;
}

.btn-clear {
    position: absolute;
    top: -6px;
    left: 40px;
    color: red;
    border: none;
    border-radius: 50%;
    width: 20px;
    height: 20px;
    cursor: pointer;
    font-weight: bold;
}


/* Form */
.form-group {
    display: flex;
    justify-content: space-between;
    margin-bottom: 12px;
}

.field {
    display: flex;
    align-items: center;
    width: 48%;
}

.field label {
    width: 160px;
    font-size: 13px;
    color: #1f2229;
    font-weight: 500;
}

.field input,
.field select {
    flex: 1;
    height: 32px;
    padding: 0 8px;
    border: 1px solid #d3d7de;
    border-radius: 4px;
    font-size: 13px;
}

.required {
    color: red;
}

/* Custom select */
.custom-select {
    position: relative;
    flex: 1;
    height: 32px;
    border: 1px solid #d3d7de;
    border-radius: 4px;
    padding: 0 8px;
    display: flex;
    align-items: center;
    cursor: pointer;
    background: #fff;
}

.custom-select-display {
    flex: 1;
    font-size: 13px;
    color: #1f2229;
}

.custom-select .icon-angle-down {
    opacity: 0.4;
}

.custom-dropdown {
    position: absolute;
    top: 36px;
    left: 0;
    width: 100%;
    border: 1px solid #d3d7de;
    background: white;
    border-radius: 4px;
    z-index: 20;
    box-shadow: 0px 2px 6px rgba(0, 0, 0, 0.1);
}

.dropdown-item {
    padding: 6px 8px;
    cursor: pointer;
    font-size: 13px;
}

.dropdown-item:hover {
    background-color: #f0f2f5;
}

.custom-select.active {
    border: 1px solid #4262f0
}
</style>