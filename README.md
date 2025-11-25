# 🧱 Project README

## 1. Giới thiệu
Dự án này bao gồm **Web API ASP.NET Core** theo mô hình **Service + Repository Pattern** phục vụ cho hệ thống quản lý khách hàng.

Source gồm 3 phần chính:
- **Backend (ASP.NET Core Web API)**
- **Frontend (Vue 3)**

---

## 2. Kiến trúc hệ thống
```
📦 Project Root
 ┣ 📂 src
 ┃ ┣ 📂 Server (ASP.NET Core)
 ┃ ┃ ┣ Api (Controllers)
 ┃ ┃ ┣ Business (Entities + Interfaces + Dtos + Mapping)
 ┃ ┃ ┗ Data (Repositories + DbContext)
 ┃ ┗ 📂 Client (Vue 3)
 ┣ 📦 sql
 ┗ README.md
```

### 🧩 Công nghệ chính
- **ASP.NET Core 8**
- **Dapper**
- **Service + Repository Pattern**
- **MySQL**
- **Vue 3**

---

## 3. Hướng dẫn Dev/Build Backend
### 🛠 Yêu cầu
- .NET 8 SDK
- MySQL
- Node 18+ (cho FE)

### ▶ Chạy Backend
```
cd src/server/Misa.CRM.Api
cp appsettings.json

# sửa connection string

dotnet restore
dotnet build
dotnet watch run
```
API chạy tại:
```
https://localhost:5078
http://localhost:5078
```

---

## 4. Seed database

Mở folder sql và tạo db theo cấu trúc trong MySQL

## 5. Hướng dẫn chạy Frontend (Vue 3)
```
cd src/client
npm install
npm run dev
```
Frontend chạy tại:  
`http://localhost:5173`

---

## 6. Bộ sưu tập Postman
### Các nhóm API chính
- **Customer** – CRUD khách hàng, search, phân trang, export CSV, import CSV, lấy mã Kh mới, check email/phone tồn tại?
- **CustomerType** – GetAll loại khách hàng

---

