<script setup lang="ts">
import MsButton from '../../components/MsButton.vue';
import TheTopbar from '../../layouts/TheTopbar.vue';

</script>

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
                    <input class="input" type="text" placeholder="Tìm kiếm thông minh">
                </div>
                <img src="../../assets/images/icon-ai.svg" alt="" class="icon-search-box">
            </div>
            <div class="tooltip wrap-icon d-flex justify-content-center align-items-center">
                <span class="icon icon-statistic"></span>
            </div>
            <MsButton/>
            <div class="tooltip"></div>
            <div class="tooltip"></div>
            <div class="tooltip"></div>
        </div>
    </TheTopbar>
    <div class="main-content">

    </div>
</template>

<style scoped>
.other-option {
    background-color: rgb(255, 255, 255);
    height: 32px;
    border-radius: 4px;
    font-size: 14px;
    font-weight: 600;
    color: #1f2229;
    padding: 0 8px 0 12px;
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

.update, .reload {
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

.wrap-icon, .search-box {
    background-color: rgb(255, 255, 255);
    border-radius: 4px;
}

.wrap-icon {
    width: 32px;
    height: 32px;
}
</style>