# Bài thực hành 8 (Đề số 9) – Triển khai hệ thống Microservices

Repository kế thừa solution từ các buổi thực hành trước. Bài thực hành này bổ sung code mẫu hoàn chỉnh cho 2 microservices: **PolicyService** và **PolicySearchService**, đồng thời cấu hình và triển khai hệ thống toàn diện.

## Thành phần được bổ sung ở TH8

- **PolicyService**: Quản lý chào giá (Offer) và hợp đồng bảo hiểm (Policy), sử dụng NHibernate, triển khai Outbox Pattern với RabbitMQ và CQRS (MediatR).
- **PolicySearchService**: Tìm kiếm hợp đồng bảo hiểm, đồng bộ dữ liệu và đánh chỉ mục trên Elasticsearch.
- Các services và thành phần được kế thừa và tích hợp:
  - AgentPortalApiGateway: API Gateway sử dụng Ocelot kết hợp Eureka Service Discovery.
  - AuthService: Xác thực người dùng và phân quyền dựa trên JWT.
  - ProductService: Quản lý danh mục sản phẩm bảo hiểm.
  - PaymentService: Quản lý tài khoản thanh toán hợp đồng (Marten / PostgreSQL).
  - PricingService: Tính toán phí bảo hiểm.
  - DashboardService: Thống kê doanh số bán hàng (Elasticsearch).
  - ChatService: Trao đổi trực tuyến qua SignalR.
  - Frontend: Ứng dụng web Single Page Application (Vue.js) và Blazor WASM.

## Hướng dẫn chạy và triển khai

### 1. Yêu cầu môi trường
- .NET 8 SDK
- Docker Desktop

### 2. Khởi động hạ tầng (Infrastructure)
Khởi động các dịch vụ phụ trợ (Eureka Server, PostgreSQL, RabbitMQ, Elasticsearch) từ thư mục gốc:

`powershell
docker compose up --build -d eureka-server postgres
`

### 3. Chạy API Gateway
`powershell
dotnet run --project .\AgentPortalApiGateway\AgentPortalApiGateway
`
- API Gateway: http://localhost:8099
- Eureka Dashboard: http://localhost:8761

### 4. Chạy Giao diện người dùng (Web UI)
Build và chạy Web UI bằng Docker:

`powershell
docker build -t th8-web-ui:local .\Web
docker run --rm -p 8081:80 th8-web-ui:local
`
Truy cập giao diện tại: http://localhost:8081

## Build dự án

`powershell
dotnet build .\TH8InsuranceMicroservices.sln --nologo
`

## Cấu hình và dữ liệu cục bộ

Không commit .env, secrets, database dumps, archive RAR, hoặc output build (in/, obj/). Cấu hình cục bộ nhạy cảm nên được truyền qua biến môi trường; tuân thủ các quy tắc trong .gitignore.
