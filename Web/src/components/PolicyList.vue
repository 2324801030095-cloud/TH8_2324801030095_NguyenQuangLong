<template>
    <div class="policy-list-page text-left py-3">
        <div class="d-flex flex-wrap justify-content-between align-items-center mb-4 pb-3 border-bottom">
            <div>
                <h2 class="font-weight-bold text-dark mb-1">
                    <i class="fa fa-file-text-o text-primary mr-2"></i>Policy Management
                </h2>
                <p class="text-muted mb-0 small">Search active policies and click any row to view complete details.</p>
            </div>
            <div>
                <span class="badge badge-light border px-3 py-2 text-secondary font-weight-bold">
                    <i class="fa fa-list-ul mr-1 text-primary"></i> {{ policies ? policies.length : 0 }} Policies
                </span>
            </div>
        </div>

        <!-- Filter & Search Box -->
        <div class="card border-0 shadow-sm rounded-lg p-3 mb-4 bg-white">
            <h6 class="font-weight-bold text-secondary mb-3">
                <i class="fa fa-search text-primary mr-1"></i> Search Policies
            </h6>
            <div class="form-row align-items-center">
                <div class="col-md-5 mb-2 mb-md-0">
                    <b-input v-model="filterFields.number" placeholder="Policy number..." class="form-control" @keyup.enter.native="search()"/>
                </div>
                <div class="col-md-5 mb-2 mb-md-0">
                    <b-input v-model="filterFields.policyHolder" placeholder="Policy Holder name..." class="form-control" @keyup.enter.native="search()"/>
                </div>
                <div class="col-md-2">
                    <b-button v-on:click="search()" variant="primary" class="btn-block font-weight-bold shadow-sm">
                        <i class="fa fa-search mr-1"></i> Search
                    </b-button>
                </div>
            </div>
        </div>

        <!-- Table -->
        <div class="card border-0 shadow-sm rounded-lg overflow-hidden bg-white">
            <b-table bordered striped hover
                     :items="policies"
                     :fields="fields"
                     @row-clicked="showDetails"
                     class="mb-0 custom-table">
            </b-table>
        </div>
    </div>
</template>

<script>
    import {HTTP} from "./http/ApiClient";
    const AND = "%20AND%20";

    export default {
        name: "PolicyList",
        data() {
            return {
                fields: [
                    {key: 'policyNumber', label: 'Policy Number', sortable: true},
                    {key: 'policyStartDate', label: 'Start Date', sortable: true},
                    {key: 'policyEndDate', label: 'End Date', sortable: true},
                    {key: 'policyHolder', label: 'Policy Holder', sortable: true}
                ],
                policies: [],
                filterFields: {
                    policyHolder: '',
                    number: ''
                }
            }
        },
        created: function () {
            this.runSearch();
        },
        methods: {
            showDetails(record) {
                this.$router.push({name: 'policyDetails', params: {policyNumber: record.policyNumber}});
            },
            search() {
                let queryString = '';
                queryString = this.addCriteria(queryString, this.filterFields.policyHolder);
                queryString = this.addCriteria(queryString, this.filterFields.number);

                this.runSearch(this.formatQueryString(queryString));
            },
            runSearch(queryString = '') {
                HTTP.get('PolicySearch' + queryString).then(response => {
                    this.policies = response.data.policies;
                });
            },
            addCriteria(queryString, criteria) {
                if(criteria !== '')
                    queryString += criteria + AND;

                return queryString;
            },
            formatQueryString(queryString) {
                if(queryString.endsWith(AND))
                    queryString = queryString.substring(0, queryString.length - AND.length);

                return queryString !== '' ? '?q=' + queryString : '';
            }
        }
    }
</script>

<style scoped lang="scss">
    .custom-table {
        cursor: pointer;
    }
</style>
