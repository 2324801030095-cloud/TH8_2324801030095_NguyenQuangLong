<template>
    <div id="app" class="d-flex flex-column min-vh-100">
        <!-- Top Navigation Bar -->
        <header class="app-header shadow-sm bg-white sticky-top border-bottom">
            <div class="container d-flex flex-wrap justify-content-between align-items-center py-2">
                <router-link to="/" class="navbar-brand d-flex align-items-center text-decoration-none">
                    <img src="./assets/logo_horizontal.png" alt="Logo" class="navbar-brand-logo mr-2"/>
                    <span class="portal-brand-text font-weight-bold text-primary">Insurance<span class="text-dark font-weight-normal">Portal</span></span>
                </router-link>

                <nav class="nav nav-pills align-items-center my-2 my-md-0">
                    <router-link to="/" class="nav-link text-secondary px-3 py-1 font-weight-bold" exact exact-active-class="active text-white bg-primary">
                        <i class="fa fa-home mr-1"></i> Home
                    </router-link>
                    <router-link to="/products" class="nav-link text-secondary px-3 py-1 font-weight-bold" active-class="active text-white bg-primary">
                        <i class="fa fa-th-large mr-1"></i> Products
                    </router-link>
                    <router-link to="/policies" class="nav-link text-secondary px-3 py-1 font-weight-bold" active-class="active text-white bg-primary">
                        <i class="fa fa-file-text-o mr-1"></i> Policies
                    </router-link>
                    <router-link to="/dashboard" class="nav-link text-secondary px-3 py-1 font-weight-bold" active-class="active text-white bg-primary">
                        <i class="fa fa-line-chart mr-1"></i> Dashboard
                    </router-link>
                    <router-link to="/chat" class="nav-link text-secondary px-3 py-1 font-weight-bold" active-class="active text-white bg-primary">
                        <i class="fa fa-comments mr-1"></i> Chat for Agents
                    </router-link>
                </nav>

                <div class="user-action-area">
                    <router-link to="/account" class="btn btn-sm btn-outline-secondary font-weight-bold px-3 py-1" v-if="currentUser">
                        <i class="fa fa-user-circle text-primary mr-1"></i> {{ currentUser.userName }}
                    </router-link>
                    <router-link to="/account" class="btn btn-sm btn-outline-primary font-weight-bold px-3 py-1" v-else>
                        <i class="fa fa-sign-in mr-1"></i> Sign In
                    </router-link>
                </div>
            </div>
        </header>

        <!-- Main Workspace -->
        <main id="main-container" class="container flex-grow-1 my-4">
            <router-view/>

            <!-- Floating Action Button for Live Chat -->
            <router-link to="/chatbot" class="chat-fab shadow-lg" title="Live Chat Support">
                <i class="fa fa-commenting-o"></i>
                <span class="fab-text">Live Chat</span>
            </router-link>
        </main>

        <!-- Footer -->
        <footer class="app-footer mt-auto py-3 bg-light border-top">
            <div class="container text-center">
                <img src="./assets/logo_horizontal.png" alt="Logo Footer" class="footer-logo mb-1"/>
                <p class="text-muted small mb-0">
                    <a href="https://asc.altkom.pl" target="_blank" class="text-secondary text-decoration-none">
                        Altkom Software &amp; Consulting LAB @ 2020
                    </a>
                    &bull; Microservices Architecture Study Case
                </p>
            </div>
        </footer>
    </div>
</template>

<script>
    import Auth from "./components/http/Auth";

    export default {
        name: "App",
        data() {
            return {
                currentUser: null
            };
        },
        created() {
            this.updateUser();
        },
        watch: {
            $route() {
                this.updateUser();
            }
        },
        methods: {
            updateUser() {
                this.currentUser = Auth.getAuthDetails();
            }
        }
    };
</script>

<style lang="scss">
    #app {
        font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        background-color: #f8fafc;
        color: #1e293b;
        min-height: 100vh;
    }

    .app-header {
        background-color: #ffffff;
        z-index: 1030;

        .navbar-brand-logo {
            height: 38px;
            object-fit: contain;
        }

        .portal-brand-text {
            font-size: 1.25rem;
            letter-spacing: -0.3px;
        }

        .nav-link {
            border-radius: 20px;
            margin: 0 4px;
            font-size: 0.9rem;
            transition: all 0.2s ease;

            &:hover {
                color: #0d6efd !important;
                background-color: #f1f5f9;
            }
        }
    }

    #main-container {
        width: 100%;
        max-width: 1200px;
    }

    .chat-fab {
        position: fixed;
        right: 28px;
        bottom: 28px;
        z-index: 1050;
        display: inline-flex;
        align-items: center;
        background: linear-gradient(135deg, #0284c7, #2563eb);
        color: #ffffff !important;
        padding: 11px 18px;
        border-radius: 30px;
        text-decoration: none !important;
        font-weight: 600;
        font-size: 0.9rem;
        box-shadow: 0 6px 18px rgba(37, 99, 235, 0.35);
        transition: transform 0.25s ease, box-shadow 0.25s ease;

        i {
            font-size: 1.15rem;
            margin-right: 8px;
        }

        &:hover {
            transform: translateY(-3px) scale(1.02);
            box-shadow: 0 8px 24px rgba(37, 99, 235, 0.45);
        }
    }

    .app-footer {
        .footer-logo {
            height: 32px;
            opacity: 0.85;
        }
    }
</style>
