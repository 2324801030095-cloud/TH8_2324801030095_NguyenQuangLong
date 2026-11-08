<template>
    <main class="home-page py-4">
        <!-- Modern Hero Banner -->
        <section class="hero-banner shadow-lg text-white position-relative overflow-hidden mb-5">
            <div class="hero-glow position-absolute"></div>
            <div class="hero-content position-relative z-index-1">
                <span class="badge badge-pill badge-light text-primary font-weight-bold px-3 py-2 mb-3 tracking-wide">
                    <i class="fa fa-shield mr-1"></i> INSURANCE SALES PORTAL
                </span>
                <h1 class="hero-title font-weight-bold display-4 mb-3">
                    {{ msg }}
                </h1>
                <p class="hero-intro lead text-light mb-4">
                    Empowering sales agents with real-time premium calculation, instant quote generation, and automated policy lifecycle management.
                </p>
                <div class="hero-actions d-flex flex-wrap align-items-center">
                    <router-link class="btn btn-light btn-lg font-weight-bold text-primary px-4 py-2 mr-3 mb-2 shadow-sm hero-btn" to="/products">
                        <i class="fa fa-th-large mr-2"></i> Browse Products
                    </router-link>
                    <router-link class="btn btn-outline-light btn-lg font-weight-bold px-4 py-2 mb-2 hero-btn-outline" to="/policies">
                        <i class="fa fa-search mr-2"></i> Find a Policy
                    </router-link>
                </div>
            </div>
        </section>

        <!-- Feature Capabilities Cards -->
        <section class="row capabilities-row" aria-label="Portal capabilities">
            <div class="col-md-4 mb-4">
                <div class="capability-card card h-100 border-0 shadow-sm p-4">
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
                <div class="capability-card card h-100 border-0 shadow-sm p-4">
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
                <div class="capability-card card h-100 border-0 shadow-sm p-4">
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
        border-radius: 20px;
        background: linear-gradient(135deg, #0f172a 0%, #1e3a8a 40%, #0284c7 100%);
        padding: 56px 48px;
        box-shadow: 0 16px 36px rgba(15, 23, 42, 0.25) !important;

        .hero-glow {
            top: -50px;
            right: -50px;
            width: 320px;
            height: 320px;
            background: radial-gradient(circle, rgba(14, 165, 233, 0.35) 0%, rgba(14, 165, 233, 0) 70%);
            border-radius: 50%;
            pointer-events: none;
        }

        .hero-title {
            letter-spacing: -0.5px;
            line-height: 1.15;
        }

        .hero-intro {
            max-width: 640px;
            font-size: 1.15rem;
            opacity: 0.92;
        }

        .hero-btn {
            border-radius: 10px;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
            &:hover {
                transform: translateY(-2px);
                box-shadow: 0 8px 18px rgba(0, 0, 0, 0.15) !important;
            }
        }

        .hero-btn-outline {
            border-radius: 10px;
            border-width: 2px;
            transition: all 0.2s ease;
            &:hover {
                background: rgba(255, 255, 255, 0.15);
                transform: translateY(-2px);
            }
        }
    }

    .capability-card {
        border-radius: 16px;
        background: #ffffff;
        border: 1px solid #e2e8f0 !important;
        transition: transform 0.25s ease, box-shadow 0.25s ease;

        &:hover {
            transform: translateY(-4px);
            box-shadow: 0 12px 24px rgba(0, 0, 0, 0.08) !important;
            border-color: #3b82f6 !important;
        }

        .step-badge {
            letter-spacing: 1px;
        }
    }
</style>
