<template>
    <div class="row">
        <div class="col-12 col-sm-6 col-lg-3 mb-4 d-flex align-items-stretch" v-for="product in products" :key="product.code">
            <ProductCard :product="product" class="w-100"></ProductCard>
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
    .row {
        margin-top: 15px;
    }
</style>
