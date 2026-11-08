<template>
    <div class="covers-box mt-2">
        <div class="covers-label text-muted small font-weight-bold mb-1">
            <i class="fa fa-shield text-primary mr-1"></i> COVERS INCLUDED:
        </div>
        <div class="d-flex flex-wrap">
            <span v-for="cover in covers" :key="cover.code" class="cover-chip mr-1 mb-1" :title="cover.name">
                <span class="cover-name">{{ cover.name }}</span>
                <span v-if="cover.sumInsured" class="cover-amount text-success font-weight-bold ml-1">({{ cover.sumInsured }} €)</span>
            </span>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'CoverList',
        props: ['covers']
    }
</script>

<style scoped lang="scss">
    .covers-box {
        background-color: #f8fafc;
        border-radius: 8px;
        padding: 8px 10px;
        border: 1px solid #edf2f7;
    }

    .covers-label {
        font-size: 0.72rem;
        letter-spacing: 0.5px;
    }

    .cover-chip {
        display: inline-flex;
        align-items: center;
        background: #ffffff;
        border: 1px solid #e2e8f0;
        border-radius: 6px;
        padding: 2px 7px;
        font-size: 0.75rem;
        color: #2d3748;
        line-height: 1.3;
    }

    .cover-name {
        max-width: 140px;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
    }

    .cover-amount {
        font-size: 0.72rem;
    }
</style>
