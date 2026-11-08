<template>
    <b-card :title="product.name"
            :img-src="product.image"
            :img-alt="product.name"
            img-top
            tag="article"
            class="product-card h-100">
        <div class="product-card-body d-flex flex-column flex-grow-1">
            <p class="card-text text-muted mb-2">
                {{product.description}}
            </p>
            <div class="cover-list-wrap flex-grow-1">
                <CoverList :covers="product.covers"></CoverList>
            </div>
            <div class="btn-wrap mt-auto pt-3 text-center">
                <router-link :to="{name: 'product', params: { productCode: product.code }}">
                    <b-button type="submit" variant="primary" class="px-4">Buy</b-button>
                </router-link>
            </div>
        </div>
    </b-card>
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
        border-radius: 4px;
        transition: box-shadow 0.2s ease;
        border: 1px solid #dee2e6;

        &:hover {
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
        }

        ::v-deep .card-body {
            display: flex;
            flex-direction: column;
            flex: 1 1 auto;
            padding: 1.25rem 1rem;
        }

        ::v-deep .card-title {
            font-size: 1.25rem;
            font-weight: bold;
            text-align: center;
            margin-bottom: 0.25rem;
        }
    }

    .card-img-top {
        height: 190px;
        object-fit: cover;
        width: 100%;
    }

    .card-text {
        text-align: center;
        font-size: 0.95rem;
    }

    .cover-list-wrap {
        margin-top: 0.5rem;
    }
</style>
