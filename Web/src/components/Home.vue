<template>
    <main class="home-page py-4">
        <!-- Clean Solid Hero Banner (No Gradient) -->
        <section class="hero-banner text-white mb-5">
            <div class="hero-content">
                <span class="badge badge-pill badge-primary font-weight-bold px-3 py-2 mb-3 tracking-wide">
                    <i class="fa fa-shield mr-1"></i> INSURANCE SALES PORTAL
                </span>
                <h1 class="hero-title font-weight-bold mb-3">
                    {{ msg }}
                </h1>
                <p class="hero-intro text-light mb-4">
                    Empowering sales agents with real-time premium calculation, instant quote generation, and automated policy lifecycle management.
                </p>
                <div class="hero-actions d-flex flex-wrap align-items-center">
                    <router-link class="btn btn-primary btn-lg font-weight-bold px-4 py-2 mr-3 mb-2 shadow-sm" to="/products">
                        <i class="fa fa-th-large mr-2"></i> Browse Products
                    </router-link>
                    <router-link class="btn btn-outline-light btn-lg font-weight-bold px-4 py-2 mb-2" to="/policies">
                        <i class="fa fa-search mr-2"></i> Find a Policy
                    </router-link>
                </div>
            </div>
        </section>

        <!-- Feature Capabilities Cards -->
        <section class="row capabilities-row" aria-label="Portal capabilities">
            <div class="col-md-4 mb-4">
                <div class="capability-card card h-100 border p-4 bg-white shadow-sm">
                    <div class="card-icon-wrapper mb-3 text-primary">
                        <i class="fa fa-calculator fa-2x"></i>
                    </div>
                    <span class="step-badge text-muted small font-weight-bold">STEP 01</span>
                    <h4 class="font-weight-bold text-dark mt-1 mb-2">Products &amp; Quotes</h4>
                    <p class="text-secondary small mb-0">
                        Review insurance products, select customizable covers, and calculate customer quotes instantly.
                    </p>
                </div>
            </div>

            <div class="col-md-4 mb-4">
                <div class="capability-card card h-100 border p-4 bg-white shadow-sm">
                    <div class="card-icon-wrapper mb-3 text-success">
                        <i class="fa fa-file-text-o fa-2x"></i>
                    </div>
                    <span class="step-badge text-muted small font-weight-bold">STEP 02</span>
                    <h4 class="font-weight-bold text-dark mt-1 mb-2">Policies &amp; Documents</h4>
                    <p class="text-secondary small mb-0">
                        Convert accepted quotes into binding policies, search customer records, and inspect policy details.
                    </p>
                </div>
            </div>

            <div class="col-md-4 mb-4">
                <div class="capability-card card h-100 border p-4 bg-white shadow-sm">
                    <div class="card-icon-wrapper mb-3 text-info">
                        <i class="fa fa-users fa-2x"></i>
                    </div>
                    <span class="step-badge text-muted small font-weight-bold">STEP 03</span>
                    <h4 class="font-weight-bold text-dark mt-1 mb-2">Agent Workspace</h4>
                    <p class="text-secondary small mb-0">
                        Sign in, track total sales performance on the Dashboard, and collaborate with team members via Chat.
                    </p>
                </div>
            </div>
        </section>
    </main>
</template>

<script>
    export default {
        name: 'Home',
        props: {
            msg: {
                type: String,
                default: 'Welcome to LAB Insurance Sales Portal'
            }
        }
    }
</script>

<style scoped lang="scss">
    .home-page {
        max-width: 1140px;
        margin: 0 auto;
        text-align: left;
    }

    .hero-banner {
        border-radius: 12px;
        background-color: #0f172a; // Solid dark navy - absolutely NO gradient
        padding: 44px 40px;
        border: 1px solid #1e293b;

        .hero-title {
            font-size: 2.5rem;
            letter-spacing: -0.5px;
            line-height: 1.2;
        }

        .hero-intro {
            max-width: 680px;
            font-size: 1.1rem;
            color: #cbd5e1 !important;
            line-height: 1.6;
        }

        .btn {
            border-radius: 8px;
        }
    }

    .capability-card {
        border-radius: 12px;
        border-color: #e2e8f0 !important;
        transition: transform 0.2s ease, box-shadow 0.2s ease;

        &:hover {
            transform: translateY(-3px);
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.06) !important;
            border-color: #cbd5e1 !important;
        }

        .step-badge {
            letter-spacing: 1px;
        }
    }
</style>
