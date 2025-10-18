# Đề tài: Ứng dụng Quản lý Đặt sân bóng 7 người
## 1. Giới thiệu
Trong bối cảnh nhu cầu thể thao, đặc biệt là bóng đá phong trào ngày càng tăng, việc tìm kiếm và đặt sân bóng vẫn còn thủ công, gây mất thời gian và không thuận tiện cho cả khách hàng lẫn chủ sân. Đề tài “Ứng dụng Quản lý Đặt sân bóng 7 người” nhằm xây dựng một hệ thống hỗ trợ khách hàng dễ dàng đặt sân trực tuyến, giúp chủ sân quản lý sân bãi và lịch đặt một cách hiệu quả, đồng thời cung cấp công cụ quản trị cho admin hệ thống.
Ứng dụng sẽ được phát triển theo mô hình đa nền tảng:
- Backend: ASP.NET Core Web API, quản lý dữ liệu và xử lý nghiệp vụ.
- Web App: Giao diện dành cho admin và chủ sân quản lý.
- Mobile App: Giao diện dành cho khách hàng, thân thiện, tiện dụng.
- AI tích hợp: hỗ trợ gợi ý sân phù hợp và chatbot trả lời nhanh các câu hỏi liên quan đến đặt sân.

## 2. Đối tượng sử dụng
- Khách hàng (người đặt sân):
    - Đăng ký/Đăng nhập để sử dụng hệ thống.
    - Tìm kiếm sân theo địa điểm, giá cả, khung giờ.
    - Đặt sân và theo dõi lịch đặt.
    - Thanh toán trực tuyến hoặc thanh toán khi đến sân.
    - Nhận gợi ý sân phù hợp dựa trên lịch sử đặt sân hoặc địa điểm thường chơi (AI).

- Chủ sân:
    - Đăng ký/Đăng nhập với tài khoản chủ sân.
    - Quản lý thông tin sân: thêm, sửa, xóa (tên sân, địa chỉ, giá, mô tả, hình ảnh,...).
    - Xem và quản lý lịch đặt sân theo khung giờ.
    - Xác nhận/cập nhật trạng thái đơn đặt (chờ xác nhận, đã xác nhận, đã hủy).
    - Theo dõi doanh thu và báo cáo thống kê.

- Admin (quản trị hệ thống):
    - Quản lý người dùng (khách hàng, chủ sân).
    - Quản lý hệ thống sân bóng (duyệt sân mới đăng ký từ chủ sân).
    - Theo dõi hoạt động chung của hệ thống.
    - Quản lý và kiểm duyệt nội dung (feedback, đánh giá sân,...).
    - Xem báo cáo tổng hợp về lượng đặt sân, doanh thu toàn hệ thống.

## 3. Chức năng chính
a. Khách hàng
- Đăng ký/Đăng nhập (qua số điện thoại).
- Tìm kiếm & lọc sân: theo địa điểm, giá, khung giờ,...
- Xem chi tiết sân: thông tin, giá, hình ảnh, đánh giá từ khách hàng khác.
- Đặt sân: chọn khung giờ, sân, số giờ → xác nhận đặt.
- Thanh toán: tích hợp thanh toán online (Momo, VNPay,...) hoặc offline.
- Xem lịch sử đặt sân và trạng thái (chờ, xác nhận, hủy).
- Đánh giá & phản hồi sau khi sử dụng sân.
- AI gợi ý sân phù hợp dựa trên thói quen đặt sân và khu vực.

b. Chủ sân
- Quản lý tài khoản chủ sân.
- Thêm/sửa/xóa sân: nhập thông tin chi tiết, giá, hình ảnh.
- Quản lý lịch đặt sân: xem danh sách đơn đặt theo khung giờ.
- Xác nhận/huỷ đơn đặt của khách.
- Theo dõi báo cáo: doanh thu, tần suất đặt sân.
- Tích hợp chatbot AI để trả lời khách nhanh về giá và tình trạng sân.

c. Admin
- Quản lý người dùng (thêm, khóa, chỉnh sửa).
- Duyệt sân mới khi chủ sân đăng ký.
- Quản lý báo cáo: doanh thu hệ thống, số lượng đặt sân, hoạt động người dùng.
- Giám sát & kiểm duyệt nội dung (feedback, đánh giá).
- Quản lý cấu hình hệ thống (chính sách, quy định, giá dịch vụ nền).

## 4. Công nghệ sử dụng
- Backend: ASP.NET Core Web API, Entity Framework, SQL Server.
- Frontend Web: ReactJS + TailwindCSS + Shadcn UI.
- Mobile App: React Native.
- Cơ sở dữ liệu: SQL Server.
- AI tích hợp:
    - Gợi ý sân phù hợp dựa trên thuật toán học máy (Machine Learning).
    - Chatbot hỗ trợ khách hàng (FAQ, tình trạng sân).
- Thanh toán: tích hợp Momo, VNPay.
- Triển khai: Docker, Azure/AWS.

## 5. Mục tiêu đồ án
- Xây dựng hệ thống quản lý đặt sân trực tuyến, hoạt động trên cả Web và Mobile.
- Đảm bảo tính đa vai trò (khách hàng, chủ sân, admin).
- Tích hợp AI thông minh để tăng trải nghiệm người dùng.
- Ứng dụng công nghệ hiện đại (ASP.NET, React, React Native).
- Hướng đến mô hình thực tế có thể triển khai cho các sân bóng tại địa phương.
