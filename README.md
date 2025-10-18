# Football Field Booking Backend API

## Mô tả dự án

API Backend cho hệ thống đặt sân bóng đá, được xây dựng bằng ASP.NET Core 8.0. Hệ thống hỗ trợ quản lý sân bóng, đặt lịch, thanh toán và đánh giá.

## Công nghệ sử dụng

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server
- **ORM**: Entity Framework Core 8.0
- **Authentication**: JWT Bearer Token
- **Mapping**: AutoMapper
- **Password Hashing**: BCrypt.Net
- **Storage**: MinIO (Object Storage)
- **API Documentation**: Swagger/OpenAPI

## Yêu cầu hệ thống

- .NET 8.0 SDK
- SQL Server 2019 trở lên (hoặc SQL Server Express)
- Visual Studio 2022 / Visual Studio Code / JetBrains Rider
- Git

## Cài đặt

### 1. Clone dự án

```bash
git clone <repository-url>
cd football-field-booking-backend
```

### 2. Cài đặt .NET 8.0 SDK

Tải và cài đặt từ: https://dotnet.microsoft.com/download/dotnet/8.0

Kiểm tra cài đặt:
```bash
dotnet --version
```

### 3. Cài đặt SQL Server

- Tải SQL Server Express: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- Hoặc sử dụng SQL Server đã có sẵn

### 4. Cấu hình Database

Mở file `appsettings.json` và cập nhật connection string phù hợp với môi trường của bạn:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FootballFieldManagement;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
  }
}
```

**Lưu ý**: 
- Nếu dùng SQL Server thông thường: `Server=localhost;...`
- Nếu dùng SQL Server Express: `Server=localhost\\SQLEXPRESS;...`
- Nếu dùng SQL Authentication: thêm `User Id=sa;Password=yourpassword;`

### 5. Restore packages

```bash
dotnet restore
```

### 6. Tạo và áp dụng Migration

```bash
# Tạo database và apply migrations
dotnet ef database update

# Nếu cần tạo migration mới
dotnet ef migrations add MigrationName
```

### 7. Chạy ứng dụng

```bash
dotnet run
```

Hoặc sử dụng watch mode để tự động reload khi có thay đổi:
```bash
dotnet watch run
```

### 8. Truy cập ứng dụng

- **API**: http://localhost:5000 hoặc https://localhost:5001
- **Swagger UI**: https://localhost:5001/swagger

## Dữ liệu mẫu (Seeding)

Dự án có sẵn dữ liệu mẫu được tự động seed khi chạy lần đầu, bao gồm:

### Tài khoản mẫu:

**Admin:**
- Email: `admin@footballfield.com`
- Password: `Admin@123`

**Chủ sân (Owner):**
- Email: `owner1@footballfield.com` | Password: `Owner@123`
- Email: `owner2@footballfield.com` | Password: `Owner@123`
- Email: `owner3@footballfield.com` | Password: `Owner@123`

**Khách hàng (Customer):**
- Email: `customer1@footballfield.com` | Password: `Customer@123`
- Email: `customer2@footballfield.com` | Password: `Customer@123`
- Email: `customer3@footballfield.com` | Password: `Customer@123`

### Dữ liệu khác:
- 3 cụm sân bóng
- 12 sân bóng (4-5 sân mỗi cụm)
- 60 time slots (5 slots/sân)
- 4 bookings mẫu
- 3 reviews
- 3 notifications

## Cấu trúc API

### Authentication
- `POST /api/auth/register` - Đăng ký tài khoản
- `POST /api/auth/login` - Đăng nhập

### Users
- `GET /api/users` - Lấy danh sách users
- `GET /api/users/{id}` - Lấy thông tin user
- `PUT /api/users/{id}` - Cập nhật user
- `DELETE /api/users/{id}` - Xóa user

### Complexes (Cụm sân)
- `GET /api/complexes` - Lấy danh sách cụm sân
- `GET /api/complexes/{id}` - Lấy chi tiết cụm sân
- `POST /api/complexes` - Tạo cụm sân mới
- `PUT /api/complexes/{id}` - Cập nhật cụm sân
- `DELETE /api/complexes/{id}` - Xóa cụm sân

### Fields (Sân)
- `GET /api/fields` - Lấy danh sách sân
- `GET /api/fields/{id}` - Lấy chi tiết sân
- `POST /api/fields` - Tạo sân mới
- `PUT /api/fields/{id}` - Cập nhật sân
- `DELETE /api/fields/{id}` - Xóa sân

### TimeSlots
- `GET /api/timeslots` - Lấy danh sách time slots
- `GET /api/timeslots/{id}` - Lấy chi tiết time slot
- `POST /api/timeslots` - Tạo time slot mới
- `PUT /api/timeslots/{id}` - Cập nhật time slot
- `DELETE /api/timeslots/{id}` - Xóa time slot

### Bookings
- `GET /api/bookings` - Lấy danh sách bookings
- `GET /api/bookings/{id}` - Lấy chi tiết booking
- `POST /api/bookings` - Tạo booking mới
- `PUT /api/bookings/{id}` - Cập nhật booking
- `DELETE /api/bookings/{id}` - Xóa booking

### Reviews
- `GET /api/reviews` - Lấy danh sách reviews
- `GET /api/reviews/{id}` - Lấy chi tiết review
- `POST /api/reviews` - Tạo review mới
- `PUT /api/reviews/{id}` - Cập nhật review
- `DELETE /api/reviews/{id}` - Xóa review

### Notifications
- `GET /api/notifications` - Lấy danh sách notifications
- `GET /api/notifications/{id}` - Lấy chi tiết notification
- `POST /api/notifications` - Tạo notification mới
- `PUT /api/notifications/{id}` - Cập nhật notification
- `DELETE /api/notifications/{id}` - Xóa notification

## Xử lý lỗi thường gặp

### 1. Lỗi connection string
```
Error: Cannot connect to SQL Server
```
**Giải pháp**: Kiểm tra lại connection string trong `appsettings.json`

### 2. Lỗi migration
```
Error: Unable to create migration
```
**Giải pháp**: 
```bash
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Lỗi port đã được sử dụng
```
Error: Address already in use
```
**Giải pháp**: Thay đổi port trong `Properties/launchSettings.json`

### 4. Lỗi JWT Secret Key
```
Error: SecurityKey length must be at least 256 bits
```
**Giải pháp**: Đảm bảo `SecretKey` trong `appsettings.json` đủ dài (ít nhất 32 ký tự)

## Tài liệu tham khảo

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [JWT Authentication](https://jwt.io/)

## Liên hệ

- Email: support@footballfield.com
- GitHub: [repository-url]

## License

[Thêm license của bạn ở đây]
