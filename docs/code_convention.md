# CODE CONVENTION - FOOTBALL FIELD BOOKING API
> Quy ước code được sử dụng trong dự án Football Field Booking Backend

---

## 1. KIẾN TRÚC DỰ ÁN (Architecture)

### 1.1. Clean Architecture Pattern
- **Entities**: Chứa các Domain Models (User, Complex, Field, Booking, etc.)
- **Repositories**: Data Access Layer - Xử lý tương tác với Database
- **Services**: Business Logic Layer - Chứa logic nghiệp vụ
- **Controllers**: Presentation Layer - API Endpoints
- **DTOs**: Data Transfer Objects - Đối tượng truyền tải dữ liệu
- **Utils**: Các class tiện ích (JwtHelper, etc.)

### 1.2. Repository Pattern
- **Generic Repository**: `IGenericRepository<T>` và `GenericRepository<T>`
  - Cung cấp các method CRUD cơ bản cho tất cả entities
- **Specific Repository**: Kế thừa Generic Repository và thêm các method đặc thù
  - VD: `IUserRepository : IGenericRepository<User>`

### 1.3. Dependency Injection
- Sử dụng .NET Core DI Container
- Đăng ký services với lifetime: **Scoped** (cho Repository và Service)
- Injection qua Constructor

---

## 2. NAMING CONVENTIONS

### 2.1. Files & Folders
- **PascalCase** cho tất cả files và folders
- **Interfaces**: Prefix với chữ "I" - `IUserRepository.cs`, `IUserService.cs`
- **Implementations**: Tên class - `UserRepository.cs`, `UserService.cs`
- **Controllers**: Suffix với "Controller" - `UsersController.cs`
- **DTOs**: Suffix với "Dto" hoặc tên mô tả - `ApiResponseDto.cs`

### 2.2. Classes & Interfaces
```csharp
// Interface: IUserService
public interface IUserService { }

// Class: UserService
public class UserService : IUserService { }

// Controller: UsersController
public class UsersController : ControllerBase { }

// Entity: User
public class User { }

// DTO: ApiResponse<T>
public class ApiResponse<T> { }
```

### 2.3. Methods & Properties
- **PascalCase** cho public methods và properties
- **camelCase** cho private/protected fields (với prefix `_`)
- **Async methods**: Suffix với "Async"

```csharp
// Properties
public int UserId { get; set; }
public string FirstName { get; set; }

// Private fields
private readonly IUserRepository _userRepository;
private readonly JwtHelper _jwtHelper;

// Methods
public async Task<User?> GetUserByIdAsync(int id) { }
private void ValidateInput() { }
```

### 2.4. Variables & Parameters
- **camelCase** cho local variables và parameters
```csharp
var userId = 1;
var userName = "John";
public void CreateUser(int userId, string firstName) { }
```

---

## 3. CODE STYLE

### 3.1. Indentation & Spacing
- **4 spaces** cho indentation (không dùng tabs)
- Opening brace `{` trên dòng mới (Allman style)
- Một dòng trống giữa các methods

```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}
```

### 3.2. Comments
- Sử dụng `///` cho XML documentation comments
- `//` cho inline comments
- Tiếng Việt được chấp nhận trong comments

```csharp
/// <summary>
/// Get user by ID
/// </summary>
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    // Tìm user theo id
    var user = await _userService.GetUserByIdAsync(id);
    if (user == null)
        return Ok(ApiResponse<string>.Fail("Không tìm thấy người dùng", 404));

    return Ok(ApiResponse<User>.Ok(user, "Lấy thông tin người dùng thành công"));
}
```

### 3.3. Using Statements
- Đặt ở đầu file
- Sắp xếp theo alphabet
- Group: System namespaces → Third-party → Project namespaces

```csharp
using System.Linq.Expressions;
using FootballField.API.DbContexts;
using FootballField.API.Entities;
using Microsoft.EntityFrameworkCore;
```

---

## 4. ENTITIES & MODELS

### 4.1. Entity Structure
```csharp
public class User
{
    // Primary Key
    public int Id { get; set; }
    
    // Properties (required)
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    // Properties (nullable)
    public string? Email { get; set; }
    public string? Phone { get; set; }
    
    // Enum
    public UserRole Role { get; set; } = UserRole.Customer;
    public UserStatus Status { get; set; } = UserStatus.Active;
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    
    // Navigation properties
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
```

### 4.2. Enums
- Định nghĩa trong cùng file với Entity hoặc file riêng
- Sử dụng explicit values

```csharp
public enum UserRole : byte
{
    Customer = 0,
    Owner = 1,
    Admin = 2
}

public enum BookingStatus : byte
{
    Pending = 0,
    Confirmed = 1,
    Completed = 2,
    Cancelled = 3
}
```

---

## 5. REPOSITORIES

### 5.1. Interface
```csharp
public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);
}
```

### 5.2. Implementation
```csharp
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email && !u.IsDeleted);
    }
}
```

---

## 6. SERVICES

### 6.1. Interface
```csharp
public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task SoftDeleteUserAsync(int id);
}
```

### 6.2. Implementation
- Service gọi Repository, không trực tiếp gọi DbContext
- Xử lý business logic tại đây

```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        // Business logic validation
        if (await _userRepository.EmailExistsAsync(user.Email))
            throw new Exception("Email đã tồn tại");

        return await _userRepository.AddAsync(user);
    }
}
```

---

## 7. CONTROLLERS

### 7.1. Structure
```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize] // Nếu cần authentication
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get all users with pagination
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var (users, totalCount) = await _userService.GetPagedUsersAsync(pageIndex, pageSize);
        var response = new ApiPagedResponse<User>(users, pageIndex, pageSize, totalCount, "Success");
        return Ok(response);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return Ok(ApiResponse<string>.Fail("Không tìm thấy người dùng", 404));

        return Ok(ApiResponse<User>.Ok(user, "Lấy thông tin thành công"));
    }
}
```

### 7.2. HTTP Methods
- `[HttpGet]` - GET requests (Read)
- `[HttpPost]` - POST requests (Create)
- `[HttpPut]` - PUT requests (Update entire resource)
- `[HttpPatch]` - PATCH requests (Partial update)
- `[HttpDelete]` - DELETE requests (Delete)

### 7.3. Authorization
```csharp
[Authorize] // Requires authentication
[Authorize(Roles = "Admin")] // Requires Admin role
[Authorize(Roles = "Admin,Owner")] // Requires Admin OR Owner role
[AllowAnonymous] // Public endpoint
```

---

## 8. API RESPONSE FORMAT

### 8.1. Standard Response
```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
```

### 8.2. Usage
```csharp
// Success response
return Ok(ApiResponse<User>.Ok(user, "Success message"));

// Error response
return Ok(ApiResponse<string>.Fail("Error message", 400));

// With status code
return Ok(ApiResponse<User>.Ok(user, "Created successfully", 201));
```

### 8.3. Paged Response
```csharp
public class ApiPagedResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
```

---

## 9. ASYNC/AWAIT PATTERN

### 9.1. Best Practices
- Luôn sử dụng `async/await` cho I/O operations
- Method async phải return `Task` hoặc `Task<T>`
- Suffix method name với "Async"
- Không block với `.Result` hoặc `.Wait()`

```csharp
// ✅ Good
public async Task<User?> GetUserAsync(int id)
{
    return await _repository.GetByIdAsync(id);
}

// ❌ Bad - blocking
public User? GetUser(int id)
{
    return _repository.GetByIdAsync(id).Result; // DON'T DO THIS
}
```

---

## 10. NULL HANDLING

### 10.1. Nullable Reference Types
- Enabled trong project: `<Nullable>enable</Nullable>`
- Sử dụng `?` cho nullable types
- Sử dụng `null!` cho non-null but initialized later

```csharp
// Nullable
public string? Email { get; set; }
public DateTime? DeletedAt { get; set; }

// Non-null (required)
public string FirstName { get; set; } = null!;
public string LastName { get; set; } = string.Empty;

// Method return
public async Task<User?> GetUserByIdAsync(int id) // có thể return null
{
    return await _dbSet.FirstOrDefaultAsync(u => u.Id == id);
}
```

---

## 11. EXCEPTION HANDLING

### 11.1. Global Exception Middleware
- Sử dụng middleware để catch tất cả exceptions
- Return consistent error response format

```csharp
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
}
```

---

## 12. JWT AUTHENTICATION

### 12.1. Configuration
```json
"JwtSettings": {
    "SecretKey": "YourSecretKey",
    "Issuer": "FootballFieldAPI",
    "Audience": "FootballFieldClient",
    "ExpiryMinutes": "60"
}
```

### 12.2. Token Generation
```csharp
public string GenerateToken(User user)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email ?? ""),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

    var token = new JwtSecurityToken(
        issuer: _issuer,
        audience: _audience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(_expiryMinutes),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```

---

## 13. DATABASE CONVENTIONS

### 13.1. Table Names
- UPPERCASE với underscore: `USER`, `COMPLEX`, `BOOKING`
- Column names: lowercase với underscore: `user_id`, `first_name`

### 13.2. Relationships
- One-to-Many: `HasMany().WithOne()`
- Many-to-Many: Sử dụng junction table
- Foreign Keys: `OnDelete(DeleteBehavior.NoAction)` để tránh cascade conflicts

---

## 14. GIT CONVENTIONS

### 14.1. Commit Messages
```
feat: Add user authentication
fix: Fix booking validation bug
refactor: Refactor repository pattern
docs: Update API documentation
style: Format code
test: Add unit tests for UserService
```

### 14.2. Branch Names
```
feature/user-authentication
bugfix/booking-validation
hotfix/security-issue
refactor/repository-pattern
```

---

## 15. PACKAGE & DEPENDENCIES

### 15.1. Core Packages
- `Microsoft.EntityFrameworkCore.SqlServer` - Database ORM
- `Microsoft.AspNetCore.Authentication.JwtBearer` - JWT Auth
- `Swashbuckle.AspNetCore` - Swagger/OpenAPI

### 15.2. Version Control
- Sử dụng .NET 8.0
- Keep packages up to date
- Review security vulnerabilities regularly

---

## 16. PERFORMANCE BEST PRACTICES

### 16.1. Database Queries
- Sử dụng `AsNoTracking()` cho read-only queries
- Include related data với `Include()` thay vì lazy loading
- Sử dụng pagination cho large datasets
- Filter trước khi load vào memory

```csharp
// ✅ Good - Query with filter and pagination
var users = await _dbSet
    .Where(u => !u.IsDeleted)
    .AsNoTracking()
    .Skip((pageIndex - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();

// ❌ Bad - Load all then filter
var allUsers = await _dbSet.ToListAsync();
var filtered = allUsers.Where(u => !u.IsDeleted).ToList();
```

---

## 17. TESTING (Future Implementation)

### 17.1. Unit Tests
- Test Business Logic trong Services
- Mock Dependencies với Moq
- Naming: `MethodName_Scenario_ExpectedResult`

### 17.2. Integration Tests
- Test API Endpoints
- Test Database operations
- Use In-Memory Database hoặc Test Database

---

## 18. SECURITY BEST PRACTICES

### 18.1. Password Hashing
- Sử dụng BCrypt hoặc PBKDF2
- Never store plain text passwords
- Implement password strength validation

### 18.2. Input Validation
- Validate tất cả user inputs
- Sanitize data trước khi lưu vào DB
- Use Data Annotations hoặc FluentValidation

### 18.3. Authorization
- Implement Role-Based Access Control (RBAC)
- Check ownership trước khi modify resources
- Log security-related events

---

**Created by**: GitHub Copilot
**Last Updated**: 2024
**Project**: Football Field Booking Backend API
