<template>
    <div class="product-card card h-100 shadow-sm border-0">
        <div class="card-img-wrapper position-relative">
            <img :src="product.image" :alt="product.name" class="card-img-top product-image">
            <span class="product-code-badge badge badge-primary position-absolute">{{ product.code }}</span>
        </div>
        <div class="card-body d-flex flex-column">
            <h5 class="card-title font-weight-bold text-dark mb-1">{{ product.name }}</h5>
            <p class="card-text text-muted small mb-2 description-text">
                {{ product.description }}
            </p>
            <div class="my-2 flex-grow-1">
                <CoverList :covers="product.covers"></CoverList>
            </div>
            <div class="mt-auto pt-3 border-top">
                <router-link :to="{name: 'product', params: { productCode: product.code }}" class="btn btn-primary btn-block shadow-sm font-weight-bold">
                    <i class="fa fa-calculator mr-1"></i> Calculate &amp; Buy
                </router-link>
            </div>
        </div>
    </div>
</template>

<script>
    import CoverList from "./CoverList";

    export default {
        name: 'ProductCard',
        props: ['product'],
        components: {CoverList}
    }
</script>

<style scoped lang="scss">
    .product-card {
        border-radius: 12px;
        overflow: hidden;
        background: #ffffff;
        border: 1px solid #e2e8f0 !important;
        transition: transform 0.25s ease, box-shadow 0.25s ease;

        &:hover {
            transform: translateY(-5px);
            box-shadow: 0 14px 28px rgba(0, 0, 0, 0.08) !important;
            border-color: #3b82f6 !important;
        }

        .card-img-wrapper {
            height: 165px;
            overflow: hidden;
            background-color: #f1f5f9;

            .product-image {
                width: 100%;
                height: 100%;
                object-fit: cover;
                transition: transform 0.35s ease;
            }

            .product-code-badge {
                top: 10px;
                right: 10px;
                font-size: 0.75rem;
                letter-spacing: 0.5px;
                padding: 4px 8px;
                border-radius: 6px;
                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
            }
        }

        &:hover .product-image {
            transform: scale(1.05);
        }

        .card-title {
            font-size: 1.15rem;
        }

        .description-text {
            line-height: 1.45;
            min-height: 42px;
            display: -webkit-box;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
            overflow: hidden;
        }

        .btn-block {
            border-radius: 8px;
            padding: 8px 12px;
            font-size: 0.9rem;
        }
    }
</style>
