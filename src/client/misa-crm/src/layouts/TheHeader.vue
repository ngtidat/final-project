<template>
    <div class="header-container d-flex justify-content-space-between align-items-center">
        <!-- Header left -->
        <div class="header-left d-flex align-items-center">
            <img src="../assets/images/logo-crm.svg" alt="logo" class="logo">

            <div class="header-title">
                CRM
            </div>

            <div class="nav-item d-flex justify-content-space_between align-items-center">
                <div class="nav-icon icon icon-house"></div>

                <div class="nav-content">Bàn làm việc</div>
            </div>
            <div class="nav-item d-flex justify-content-space_between align-items-center active relative">
                <div class="nav-icon icon icon-customer"></div>

                <RouterLink to="/customer" custom v-slot="{ navigate }">
                    <div class="nav-content" @click="navigate">
                        Khách hàng
                        <div class="active-row"></div>
                    </div>
                </RouterLink>
            </div>
            <div class="nav-item d-flex justify-content-space_between align-items-center">
                <div class="nav-icon icon icon-opportunity"></div>

                <div class="nav-content">Cơ hội</div>
            </div>
            <div class="nav-item d-flex justify-content-space_between align-items-center">
                <div class="nav-icon icon icon-price"></div>

                <div class="nav-content">Báo giá</div>
            </div>
            <div class="nav-item d-flex justify-content-space_between align-items-center">
                <div class="nav-icon icon icon-order"></div>

                <div class="nav-content">Đơn hàng</div>
            </div>
            <div class="nav-item d-flex justify-content-space_between align-items-center">
                <div class="nav-icon icon icon-merchandise"></div>

                <div class="nav-content">Hàng hóa</div>
            </div>
            <div class="nav-item d-flex justify-content-space_between align-items-center">
                <div class="nav-icon icon icon-circle-chart"></div>

                <div class="nav-content">Báo cáo</div>
            </div>
            <div class="nav-item d-flex justify-content-space_between align-items-center">
                <div class="nav-icon icon icon-menu"></div>

                <div class="nav-content">Tất cả</div>
            </div>
        </div>

        <!-- Header right -->
        <!-- <div class="header-right d-flex align-items-center">
            <img src="../assets/images/avt.svg" alt="" class="avatar">
        </div> -->
    </div>
</template>

<script setup>
</script>

<style scoped>
.header-container {
    max-height: 48px;
    font-size: 13px;
    margin: 0px 12px 0px 36px;
}

.header-left {
    overflow: hidden;
    min-width: 100px;
}

.logo {
    width: 32px !important;
    height: 32px !important;
}

.header-title {
    font-size: 18px !important;
    font-weight: 700 !important;
    margin: 12px 8px;
}

.nav-item {
    padding: 8px;
}

.logo:hover,
.header-title:hover,
.nav-item:hover,
.avatar:hover {
    cursor: pointer;
}

.nav-item:hover {
    background-color: #f0f2f4;
    border-radius: 4px;
}

.nav-content {
    margin-left: 8px;
    white-space: nowrap;
}

.active {
    color: #4262f0;
}

.active:hover {
    background-color: inherit !important;
    border-radius: 0 !important;
}

.icon-customer {
    background-color: #4262f0;
}

.active-row {
    position: absolute;
    bottom: -8px;
    height: 2px;
    background-color: #4262f0;
    left: 8px;
    right: 8px;
}

.avatar {
    width: 32px;
    height: 32px;
}
</style>