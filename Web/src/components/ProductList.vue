<template>
    <div class="products-container py-3 text-left">
        <div class="d-flex flex-wrap justify-content-between align-items-center mb-4 pb-3 border-bottom">
            <div>
                <h2 class="font-weight-bold text-dark mb-1">
                    <i class="fa fa-th-large text-primary mr-2"></i>Insurance Products
                </h2>
                <p class="text-muted mb-0 small">Browse available policies, choose covers, and calculate customer quotes.</p>
            </div>
            <div class="mt-2 mt-md-0">
                <span class="badge badge-light border px-3 py-2 text-secondary font-weight-bold">
                    <i class="fa fa-cubes text-info mr-1"></i> {{ products.length }} Products Available
                </span>
            </div>
        </div>

        <div class="row">
            <div class="col-12 col-sm-6 col-lg-3 mb-4 d-flex align-items-stretch" v-for="product in products" :key="product.code">
                <ProductCard :product="product"></ProductCard>
            </div>
        </div>
    </div>
</template>

<script>
    import ProductCard from "./ProductCard";
    import {HTTP} from "./http/ApiClient";

    export default {
        name: 'ProductList',
        components: {ProductCard},
        data() {
            return {
                products: []
            };
        },
        created: function () {
            HTTP.get('products').then(response => {
                this.products = response.data;
            });
        }
    }
</script>

<style scoped lang="scss">
    .products-container {
        min-height: 400px;
    }
</style>
