<template>
    <div class="add-item d-flex align-items-center">
        <div class="icon-wrapper">
            <i class="icon icon-plus"></i>
        </div>
        Thêm
    </div>
</template>

<script setup>

</script>

<style scoped>
.add-item {
    margin-left: 8px;
    height: 32px;
    padding-right: 12px;
    border-radius: 4px !important;
    background-color: #4262f0;
    color: #fff;
    font-weight: 600;
}

.add-item:hover {
    cursor: pointer;
    background-color: #2b4eee;
}

.icon-wrapper {
    padding: 8px;
}

.icon {
    background-color: #ffffff;
}
</style>