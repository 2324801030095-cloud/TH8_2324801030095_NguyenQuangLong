<template>
    <div class="product-details-page text-left py-3">
        <div class="row">
            <!-- Left Column: Product Info & Image -->
            <div class="col-md-5 mb-4">
                <div class="card border-0 shadow-sm rounded-lg overflow-hidden h-100">
                    <div class="product-image-container">
                        <img class="product-hero-image" :src="productDetails.image" :alt="productDetails.name">
                    </div>
                    <div class="card-body">
                        <span class="badge badge-primary px-2 py-1 mb-2">{{ productDetails.code }}</span>
                        <h2 class="font-weight-bold text-dark">{{ productDetails.name }}</h2>
                        <p class="text-muted">{{ productDetails.description }}</p>

                        <div v-if="productDetails.covers && productDetails.covers.length" class="mt-3 pt-3 border-top">
                            <h6 class="font-weight-bold text-secondary mb-2">
                                <i class="fa fa-shield text-primary mr-1"></i> Included Covers
                            </h6>
                            <ul class="list-group list-group-flush small">
                                <li v-for="c in productDetails.covers" :key="c.code" class="list-group-item d-flex justify-content-between align-items-center px-0 py-2">
                                    <span><i class="fa fa-check-circle text-success mr-2"></i>{{ c.name }}</span>
                                    <span v-if="c.sumInsured" class="font-weight-bold text-dark">{{ c.sumInsured }} EUR</span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Right Column: Calculation Form & Price Result -->
            <div class="col-md-7 mb-4">
                <div class="card border-0 shadow-sm rounded-lg p-4 bg-white h-100">
                    <div class="d-flex justify-content-between align-items-center mb-3 pb-2 border-bottom">
                        <h4 class="font-weight-bold text-dark mb-0">
                            <i class="fa fa-calculator text-primary mr-2"></i>
                            {{ 'VIEW' === mode ? 'Offer Summary' : 'Calculate Premium' }}
                        </h4>
                        <span class="badge" :class="'VIEW' === mode ? 'badge-success px-2 py-1' : 'badge-light border px-2 py-1'">
                            {{ 'VIEW' === mode ? 'Ready to Buy' : 'Parameters' }}
                        </span>
                    </div>

                    <form>
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <label class="font-weight-bold text-secondary small" for="policyFrom">
                                    <i class="fa fa-calendar text-primary mr-1"></i> Policy From
                                </label>
                                <b-form-input v-model="policyFrom" type="date" id="policyFrom" name="policyFrom" :disabled="'VIEW' === mode"></b-form-input>
                            </div>
                            <div class="form-group col-md-6">
                                <label class="font-weight-bold text-secondary small" for="policyTo">
                                    <i class="fa fa-calendar-check-o text-primary mr-1"></i> Policy To
                                </label>
                                <b-form-input v-model="policyTo" type="date" id="policyTo" name="policyTo" :disabled="'VIEW' === mode"></b-form-input>
                            </div>
                        </div>

                        <div v-for="a in answers" class="form-group" :key="a.question.questionCode">
                            <label class="font-weight-bold text-secondary small">{{ a.question.text }}</label>
                            <div v-if="a.question.questionType==='Numeric'">
                                <input required type="number" class="form-control" v-model="a.answer" :disabled="'VIEW' === mode" placeholder="Enter number"/>
                            </div>
                            <div v-if="a.question.questionType==='Choice'">
                                <select required class="form-control" v-model="a.answer" :disabled="'VIEW' === mode">
                                    <option v-for="option in a.question.choices" :value="option.code" :key="option.code">
                                        {{ option.label }}
                                    </option>
                                </select>
                            </div>
                        </div>

                        <!-- Price display when in VIEW mode -->
                        <div class="price-banner p-3 my-3 rounded text-center border" v-if="'VIEW' === mode">
                            <div class="text-uppercase small text-muted font-weight-bold mb-1">Total Calculated Premium</div>
                            <div class="display-4 font-weight-bold text-success mb-1">
                                {{ price.amountToPay }} <span class="h4 text-secondary font-weight-normal">EUR</span>
                            </div>
                            <div class="small text-muted">Offer Reference: <span class="font-weight-bold text-dark">{{ offerNumber }}</span></div>
                        </div>

                        <!-- Action Buttons -->
                        <div class="pt-3 border-top mt-3">
                            <div class="d-flex justify-content-between align-items-center" v-if="'EDIT' === mode">
                                <router-link :to="{name: 'products'}" class="btn btn-outline-secondary px-3">
                                    <i class="fa fa-arrow-left mr-1"></i> Back
                                </router-link>
                                <button type="submit" class="btn btn-primary px-4 font-weight-bold shadow-sm" v-on:click.stop.prevent="calculatePrice">
                                    <i class="fa fa-calculator mr-1"></i> Calculate Price
                                </button>
                            </div>

                            <div class="d-flex justify-content-between align-items-center" v-if="'VIEW' === mode">
                                <div>
                                    <router-link :to="{name: 'products'}" class="btn btn-outline-secondary mr-2">
                                        <i class="fa fa-arrow-left mr-1"></i> Back
                                    </router-link>
                                    <button class="btn btn-light border" v-on:click.stop.prevent="backToEdit">
                                        <i class="fa fa-pencil mr-1"></i> Edit Parameters
                                    </button>
                                </div>
                                <button type="submit" class="btn btn-success px-4 font-weight-bold shadow" v-on:click.stop.prevent="createOffer">
                                    <i class="fa fa-shopping-cart mr-1"></i> Buy Policy
                                </button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import {HTTP} from "./http/ApiClient";
    import Auth from "./http/Auth";

    export default {
        name: 'ProductDetails',
        props: {
            productCode: String
        },
        data() {
            return {
                productDetails: {},
                answers: [],
                mode: 'EDIT',
                price: {
                    amountToPay: null
                },
                policyFrom: '',
                policyTo: '',
                offerNumber: ''
            }
        },
        created: function () {
            HTTP.get('products/' + this.productCode).then(response => {
                this.productDetails = response.data;
                if (!this.productDetails.questions)
                    return;

                for (let i = 0; i < this.productDetails.questions.length; i++) {
                    this.answers.push({
                        answer: null,
                        question: this.productDetails.questions[i]
                    });
                }
            });
        },
        methods: {
            backToEdit: function () {
                this.mode = 'EDIT';
            },
            createRequest: function () {
                const request = {
                    'productCode': this.productDetails.code,
                    'policyFrom': this.policyFrom,
                    'policyTo': this.policyTo,
                    'selectedCovers': [],
                    'answers': []
                };

                for (let i = 0; i < this.productDetails.covers.length; i++) {
                    request.selectedCovers.push(this.productDetails.covers[i].code);
                }

                for (let j = 0; j < this.answers.length; j++) {
                    request.answers.push({
                        'questionCode': this.answers[j].question.questionCode,
                        'questionType': this.answers[j].question.questionType,
                        'answer': this.answers[j].answer
                    });
                }

                return request;
            },
            calculatePrice: function () {
                const endpoint = Auth.isAuthenticated() ? 'offers/agent' : 'offers';
                HTTP.post(endpoint, this.createRequest()).then(response => {
                    this.mode = 'VIEW';
                    this.price.amountToPay = response.data.totalPrice;
                    this.offerNumber = response.data.offerNumber;
                }, () => {
                    alert('Bad Things Happened!');
                });
            },
            createOffer: function () {
                this.$router.push({name: 'createPolicy', params: {offerNumber: this.offerNumber}});
            }
        }
    }
</script>

<style scoped lang="scss">
    .product-details-page {
        .product-image-container {
            height: 220px;
            overflow: hidden;
            background-color: #f1f5f9;

            .product-hero-image {
                width: 100%;
                height: 100%;
                object-fit: cover;
            }
        }

        .price-banner {
            background-color: #f0fdf4;
            border-color: #bbf7d0 !important;
        }
    }
</style>
