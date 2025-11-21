<template>
    <TheTopbar>
        <div class="topbar-left d-flex align-items-center">
            <div class="topbar-title">
                <div>Thêm khách hàng</div>
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
            <span class="icon-avatar"></span>
        </div>

        <div class="tilte-form">Thông tin chung</div>

        <form action="">
            <div class="form-container d-flex flex-direction-column">
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Mã khách hàng</label>
                        <input type="text" disabled :value="newCustomerId">
                    </div>
                    <div class="d-flex field">
                        <label for="">
                            Tên khách hàng
                            <span class="required">*</span>
                        </label>
                        <input type="text" v-model="formData.CustomerName">
                    </div>
                </div>
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Số điện thoại</label>
                        <input type="text" v-model="formData.CustomerPhone"
                            @blur="() => validatePhone(formData.CustomerPhone)">
                    </div>
                    <div class="d-flex field">
                        <label for="">Email</label>
                        <input type="text" v-model="formData.CustomerEmail"
                            @blur="() => validateEmail(formData.CustomerEmail)">
                    </div>
                </div>
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Giới tính</label>
                        <select v-model="formData.Gender">
                            <option :value="null"></option>
                            <option :value="1">Nam</option>
                            <option :value="0">Nữ</option>
                        </select>
                    </div>
                    <div class="d-flex field">
                        <label for="">Địa chỉ</label>
                        <input type="text" v-model="formData.CustomerAddress">
                    </div>
                </div>
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Lĩnh vực</label>
                        <input type="text" placeholder="null" v-model="formData.CustomerIndustry">
                    </div>
                    <div class="d-flex field">
                        <label for="">Mã số thuế</label>
                        <input type="text" v-model="formData.CustomerTaxCode">
                    </div>
                </div>
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Số điện thoại khác</label>
                        <input type="text" v-model="formData.OtherPhoneNumber">
                    </div>
                    <div class="d-flex field">
                        <label for="">Loại khách hàng</label>
                        <select v-model="formData.CustomerTypeId">
                            <option :value="null">null</option>
                            <option v-for="type in customerTypes" :key="type.customerTypeId"
                                :value="type.customerTypeId">
                                {{ type.customerTypeName }}
                            </option>
                        </select>
                    </div>
                </div>
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Ngày mua gần nhất</label>
                        <input type="text" v-model="formData.LastPurchaseDate">
                    </div>
                    <div class="d-flex field">
                        <label for="">Hàng hóa đã mua</label>
                        <input type="text" v-model="formData.PurchaseItems">
                    </div>
                </div>
                <div class="form-group d-flex align-items-center justify-content-space-between">
                    <div class="d-flex field">
                        <label for="">Tên hàng hóa mua</label>
                        <input type="text" v-model="formData.PurchasedItemName">
                    </div>
                    <div class="d-flex field">
                        <label for="">Địa chỉ giao hàng</label>
                        <input type="text" v-model="formData.ShippingAddress">
                    </div>
                </div>
            </div>
        </form>
    </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { customerTypeService } from '../../services/customerTypeService.js'
import { customerService } from '../../services/customerService.js'
import TheTopbar from '../../layouts/TheTopbar.vue'

const route = useRoute();
const router = useRouter();
const isAdd = ref(true);

const customerTypes = ref([]);
const customerId = ref(null);
const newCustomerId = ref(null);
const error = ref(null);

const originalEmail = ref(null);
const originalPhone = ref(null);

// Form data reactive
const formData = reactive({
    CustomerName: '',
    CustomerPhone: '',
    CustomerEmail: '',
    Gender: null,
    CustomerAddress: '',
    CustomerTypeId: null,
    CustomerIndustry: '',
    CustomerTaxCode: '',
    OtherPhoneNumber: '',
    LastPurchaseDate: '',
    PurchaseItems: '',
    PurchaseItemName: '',
    ShippingAddress: ''
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
    if (!currentCustomerId) return;
    try {
        const res = await customerService.getById(currentCustomerId);
        const data = res.data.data;

        Object.keys(formData).forEach(key => {
            if (data[key] !== undefined) formData[key] = data[key];
        });

        originalEmail.value = data.CustomerEmail || '';
        originalPhone.value = data.CustomerPhone || '';
        customerId.value = data.CustomerTypeId || null;
    } catch (err) {
        error.value = err;
    }
}

onMounted(() => {
    fetchCustomerTypes();
    fetchCustomer();
    fetchNewCustomerId();
});

function buildPayload() {
    return {
        CustomerName: formData.CustomerName || '',
        CustomerAddress: formData.CustomerAddress || null,
        CustomerPhone: formData.CustomerPhone || null,
        CustomerEmail: formData.CustomerEmail || null,
        CustomerTaxCode: formData.CustomerTaxCode || null,
        CustomerTypeId: formData.CustomerTypeId || null,
        CustomerIndustry: formData.CustomerIndustry || null,
        Gender: formData.Gender !== null ? Number(formData.Gender) : null,
        OtherPhoneNumber: formData.OtherPhoneNumber || null,
        LastPurchaseDate: formData.LastPurchaseDate || null,
        PurchaseItems: formData.PurchaseItems || null,
        PurchaseItemName: formData.PurchaseItemName || null,
        ShippingAddress: formData.ShippingAddress || null
    }
}

function handleCancel() {
    router.push('/customer')
}

function validateName(name) {
    if (!name || !name.trim()) {
        alert("Tên khách hàng không được bỏ trống");
        return false;
    }
    return true;
}

async function validateEmail(email) {
    if (!email) return;

    if (currentCustomerId && email === originalEmail.value) return;

    try {
        const res = await customerService.checkExistEmail(email);
        // console.log(res.data.data)
        if (res.data.data) {
            alert("Email đã tồn tại");
            formData.CustomerEmail = '';
        }
    } catch (err) {
        console.error(err.response?.data);
    }
}

async function validatePhone(phone) {
    if (!phone) return;

    if (currentCustomerId && phone === originalPhone.value) return;

    try {
        const res = await customerService.checkExistPhone(phone);
        if (res.data.data) {
            alert("Số điện thoại đã tồn tại");
            formData.CustomerPhone = '';
        }
    } catch (err) {
        console.error(err.response?.data);
    }
}

async function handleSave() {
    if (!validateName(formData.CustomerName)) return;

    await validateEmail(formData.CustomerEmail);
    await validatePhone(formData.CustomerPhone);

    const payload = buildPayload();

    try {
        if (currentCustomerId) {
            // console.log('Data to send:', payload)
            await customerService.update(currentCustomerId, payload)
            // alert("Cập nhật thành công")
        } else {
            // console.log('Data to send:', payload)
            await customerService.create(payload)
            // alert("Thêm mới thành công")
        }

        // router.push('/customer')
    } catch (error) {
        console.log(error)
        alert("Lỗi khi lưu khách hàng")
    }
}

async function handleSaveAdd() {
    if (!validateName(formData.CustomerName)) return;

    await validateEmail(formData.CustomerEmail);
    await validatePhone(formData.CustomerPhone);

    if (!formData.CustomerEmail || !formData.CustomerPhone) {
        return;
    }

    const payload = buildPayload();

    try {
        await customerService.create(payload)

        alert("Thêm mới thành công")

        // Reset form
        Object.keys(formData).forEach(key => formData[key] = null);

        // Lấy mã mới cho form tiếp theo
        fetchNewCustomerId();
    } catch (error) {
        console.error(error)
        alert("Lỗi khi thêm mới")
    }
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
    font-weight: 500;
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
    margin-right: 200px;
}

.avatar-title,
.tilte-form {
    font-size: 20px !important;
    font-weight: 500;
    margin-bottom: 16px;
}

.select-avatar {
    margin-bottom: 40px;
}

/* Form */
.form-group {
    display: flex;
    justify-content: space-between;
    margin-bottom: 16px;
}

.field {
    display: flex;
    align-items: center;
    width: 48%;
}

.field label {
    width: 180px;
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
</style>