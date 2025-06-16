# Module 16: Authentication & Security

## 📋 Overview

This module explores essential security concepts in web applications, focusing on secure password handling and custom authentication mechanisms. You'll learn to implement proper password hashing, create custom authentication handlers, and build role-based authorization systems for protecting API endpoints.

## 🎯 Learning Objectives

- Implement secure password hashing using industry-standard algorithms
- Create custom authentication handlers for ASP.NET Core
- Design role-based authorization systems
- Understand salt generation and password security best practices
- Build protected API endpoints with custom security schemes
- Master claims-based authentication and authorization
- Apply security principles to real-world web applications

## 📁 Project Structure

```
modul16/
├── modul16.sln                          # Solution file
├── password-hashing/
│   ├── HashedPassword.csproj            # Password hashing project
│   └── Program.cs                       # PBKDF2 password hashing demo
└── unsecure-api/
    ├── secure-api.csproj                # API project configuration
    ├── Program.cs                       # Secure API with custom authentication
    └── DummyAuthenticationHandler.cs    # Custom authentication implementation
```

## 🔐 Password Hashing Implementation

### Secure Password Storage

**password-hashing/Program.cs** demonstrates industry-standard password hashing:

```csharp
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

// User credentials
string password = "password123";

// Generate a 128-bit salt for the hash function
byte[] saltBytes = new byte[128 / 8];
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetNonZeroBytes(saltBytes);
}

// Convert salt to Base64 for storage
string salt = Convert.ToBase64String(saltBytes); 

// Create 256-bit hash of "password + salt" using 100,000 iterations
// HMACSHA256 is the hash function used below
string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
    password: password,
    salt: Convert.FromBase64String(salt),
    prf: KeyDerivationPrf.HMACSHA256,
    iterationCount: 100000,
    numBytesRequested: 256 / 8));

Console.WriteLine($"Password {password} plus salt {salt} er hashed til {hashed}");
```

### Key Security Components

#### Salt Generation
```csharp
byte[] saltBytes = new byte[128 / 8];  // 16 bytes = 128 bits
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetNonZeroBytes(saltBytes);    // Cryptographically secure random
}
string salt = Convert.ToBase64String(saltBytes);
```

**Purpose of Salt:**
- **Prevents Rainbow Table Attacks**: Pre-computed hash tables become useless
- **Unique Per Password**: Same password gets different hash with different salt
- **Randomness**: Cryptographically secure random number generator

#### PBKDF2 Password Hashing
```csharp
string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
    password: password,                    // The password to hash
    salt: Convert.FromBase64String(salt),  // Random salt
    prf: KeyDerivationPrf.HMACSHA256,     // Hash function (HMAC-SHA256)
    iterationCount: 100000,               // Number of iterations
    numBytesRequested: 256 / 8));         // Output length (32 bytes)
```

**PBKDF2 Benefits:**
- **Slow by Design**: 100,000 iterations make brute force attacks expensive
- **Industry Standard**: Recommended by security experts and standards bodies
- **Configurable**: Can adjust iteration count as hardware improves
- **Proven**: Extensively tested and widely adopted

## 🛡️ Custom Authentication Handler

### DummyAuthenticationHandler Implementation

**DummyAuthenticationHandler.cs** provides a complete custom authentication system:

```csharp
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public class DummyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public DummyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
        ) : base(options, logger, encoder, clock)
    {
    }

    // Hash password using same method as password-hashing project
    public string GetHashedPassword(string password)
    {
        var salt = "V7LWFOWhquIRPxZaXAP1nw=="; // Fixed salt (shared secret)

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        
        return hashed;   
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Check if anonymous access is allowed for this endpoint
        var endpoint = Context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        // Extract Authorization header
        var authHeader = Request.Headers["Authorization"].ToString();

        // Check if header exists and starts with "Password"
        if (authHeader != null && authHeader.StartsWith("Password", StringComparison.OrdinalIgnoreCase))
        {
           var password = GetHashedPassword(authHeader.Substring("Password ".Length).Trim());

            // Validate admin password
            if (GetHashedPassword("password123") == password)
            {
                var claims = new[] { new Claim("Role", "Admin") };
                var identity = new ClaimsIdentity(claims);
                var claimsPrincipal = new ClaimsPrincipal(identity);

                return Task.FromResult(AuthenticateResult.Success(
                    new AuthenticationTicket(claimsPrincipal, "DummyAuthentication")));
            }
            
            // Validate cake lover password
            if (GetHashedPassword("cake123") == password)
            {
                var claims = new[] { new Claim("Role", "CakeLover") };
                var identity = new ClaimsIdentity(claims);
                var claimsPrincipal = new ClaimsPrincipal(identity);

                return Task.FromResult(AuthenticateResult.Success(
                    new AuthenticationTicket(claimsPrincipal, "DummyAuthentication")));
            }
        }
 
        Response.StatusCode = 401;
        return Task.FromResult(AuthenticateResult.Fail("Invalid Password"));  
    }
}
```

### Authentication Flow Breakdown

#### 1. Anonymous Access Check
```csharp
var endpoint = Context.GetEndpoint();
if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
{
    return Task.FromResult(AuthenticateResult.NoResult());
}
```
**Purpose**: Allow some endpoints to be accessed without authentication

#### 2. Header Extraction
```csharp
var authHeader = Request.Headers["Authorization"].ToString();
if (authHeader != null && authHeader.StartsWith("Password", StringComparison.OrdinalIgnoreCase))
{
    var password = GetHashedPassword(authHeader.Substring("Password ".Length).Trim());
}
```
**Format**: `Authorization: Password <plaintext-password>`
**Process**: Extract password and hash it for comparison

#### 3. Credential Validation
```csharp
// Admin user
if (GetHashedPassword("password123") == password)
{
    var claims = new[] { new Claim("Role", "Admin") };
    // ... create authentication ticket
}

// Cake lover user  
if (GetHashedPassword("cake123") == password)
{
    var claims = new[] { new Claim("Role", "CakeLover") };
    // ... create authentication ticket
}
```
**Users**:
- **Admin**: Password "password123" → Role "Admin"
- **CakeLover**: Password "cake123" → Role "CakeLover"

#### 4. Claims Creation
```csharp
var claims = new[] { new Claim("Role", "Admin") };
var identity = new ClaimsIdentity(claims);
var claimsPrincipal = new ClaimsPrincipal(identity);

return Task.FromResult(AuthenticateResult.Success(
    new AuthenticationTicket(claimsPrincipal, "DummyAuthentication")));
```
**Claims-Based Security**: User identity represented as set of claims

## 🌐 Secure API Implementation

### API Configuration and Setup

**Program.cs** configures the secure API with custom authentication:

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Register custom authentication scheme
builder.Services.AddAuthentication("DummyAuthentication")
                .AddScheme<AuthenticationSchemeOptions, DummyAuthenticationHandler>("DummyAuthentication", null);

// Configure authorization policies
builder.Services.AddAuthorization(options => {
    options.AddPolicy("LikesCake", policy => policy.RequireClaim("Role", "CakeLover"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure middleware pipeline
app.UseRouting();
app.UseAuthentication();    // Must come before authorization
app.UseAuthorization();
```

### Protected API Endpoints

#### Admin-Only Endpoint
```csharp
app.MapGet("/api/admin", [Authorize(Policy= "Admin")]
    () => {
        return "Hello Admin!";
    });
```
**Access**: Only users with "Admin" role
**Authentication**: Requires `Authorization: Password password123`

#### Cake Lover Endpoint
```csharp
app.MapGet("/api/cake", [Authorize(Policy= "LikesCake")]
    () => {
        return "Hello Cake Lover!";
    });
```
**Access**: Only users with "CakeLover" role
**Authentication**: Requires `Authorization: Password cake123`

#### Anonymous Endpoints
```csharp
app.MapGet("/api/hello", [AllowAnonymous]
    () => "Hello World!");

app.MapGet("/", [AllowAnonymous]
    () => "Welcome to the front page");
```
**Access**: No authentication required
**Purpose**: Public endpoints for general access

## 🔑 Authorization Policies

### Policy Configuration

Role-based authorization policies defined at startup:

```csharp
builder.Services.AddAuthorization(options => {
    options.AddPolicy("LikesCake", policy => policy.RequireClaim("Role", "CakeLover"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
});
```

### Policy Application

Applied to endpoints using attributes:

```csharp
[Authorize(Policy = "Admin")]        // Requires Admin role
[Authorize(Policy = "LikesCake")]    // Requires CakeLover role
[AllowAnonymous]                     // No authentication required
```

## 🧪 Testing the Security System

### Testing with curl

#### Anonymous Access
```bash
# Public endpoints - no authentication needed
curl http://localhost:5000/
curl http://localhost:5000/api/hello
```

#### Admin Access
```bash
# Admin endpoint - requires admin password
curl -H "Authorization: Password password123" http://localhost:5000/api/admin

# Expected response: "Hello Admin!"
```

#### Cake Lover Access
```bash
# Cake endpoint - requires cake lover password
curl -H "Authorization: Password cake123" http://localhost:5000/api/cake

# Expected response: "Hello Cake Lover!"
```

#### Unauthorized Access
```bash
# Wrong password
curl -H "Authorization: Password wrongpassword" http://localhost:5000/api/admin

# Expected response: 401 Unauthorized

# No authorization header
curl http://localhost:5000/api/admin

# Expected response: 401 Unauthorized
```

### Testing with Browser/Postman

**Headers to set:**
- **Key**: `Authorization`
- **Value**: `Password password123` (for admin) or `Password cake123` (for cake lover)

**Test Scenarios:**
1. **Valid Admin**: Access `/api/admin` with admin credentials
2. **Valid CakeLover**: Access `/api/cake` with cake lover credentials
3. **Cross-Role Access**: Try admin credentials on cake endpoint (should work if policy allows)
4. **Invalid Credentials**: Try wrong password on any protected endpoint
5. **Missing Headers**: Access protected endpoints without authorization header

## 🚀 How to Run

### 1. Password Hashing Demo
```bash
cd modul16/password-hashing
dotnet run
```
**Output**: Shows password, salt, and resulting hash

### 2. Secure API
```bash
cd modul16/unsecure-api
dotnet run
```
**Server**: Starts on configured port (check launchSettings.json)

### 3. Test API Endpoints
```bash
# Test anonymous access
curl http://localhost:5000/api/hello

# Test protected access
curl -H "Authorization: Password password123" http://localhost:5000/api/admin
curl -H "Authorization: Password cake123" http://localhost:5000/api/cake
```

## 📦 Project Dependencies

### Password Hashing Project
**HashedPassword.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Cryptography.KeyDerivation" Version="6.0.10" />
  </ItemGroup>
</Project>
```

### Secure API Project
**secure-api.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <RootNamespace>secure_api</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.0.10" />
  </ItemGroup>
</Project>
```

## 🔐 Security Best Practices Demonstrated

### Password Security

#### ✅ Good Practices Implemented
- **Strong Hashing**: PBKDF2 with 100,000 iterations
- **Random Salt**: Cryptographically secure salt generation
- **Sufficient Length**: 128-bit salt, 256-bit hash output
- **Standard Algorithm**: HMAC-SHA256 with PBKDF2

#### ❌ Common Mistakes Avoided
- **Plain Text Storage**: Never store passwords in plain text
- **Weak Hashing**: Avoid MD5, SHA1, or single-pass SHA256
- **No Salt**: Always use unique salts per password
- **Low Iterations**: Minimum 10,000, preferably 100,000+ iterations

### Authentication Security

#### ✅ Secure Implementation Features
- **Claims-Based**: Modern claims-based authentication
- **Role Separation**: Different roles have different access levels
- **Middleware Pipeline**: Proper authentication/authorization order
- **Anonymous Handling**: Explicit anonymous access control

#### ⚠️ Demo Limitations (Don't Use in Production)
- **Fixed Salt**: Production should use unique salts per user
- **Simple Scheme**: Real applications should use JWT or similar
- **Hardcoded Credentials**: Production should use database storage
- **Basic Validation**: Missing rate limiting, account lockout, etc.

## 🎓 Real-World Applications

### Production Considerations

#### Database Integration
```csharp
public class UserService
{
    public async Task<User> ValidateUserAsync(string username, string password)
    {
        var user = await GetUserByUsernameAsync(username);
        if (user == null) return null;
        
        var hashedInput = HashPassword(password, user.Salt);
        return hashedInput == user.HashedPassword ? user : null;
    }
    
    public async Task<User> CreateUserAsync(string username, string password)
    {
        var salt = GenerateRandomSalt();
        var hashedPassword = HashPassword(password, salt);
        
        return await SaveUserAsync(new User 
        { 
            Username = username, 
            HashedPassword = hashedPassword, 
            Salt = salt 
        });
    }
}
```

#### JWT Token Authentication
```csharp
public class JwtAuthenticationHandler : AuthenticationHandler<JwtBearerOptions>
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Request.Headers["Authorization"]
            .FirstOrDefault()?.Split(" ").Last();
            
        if (token == null)
            return AuthenticateResult.Fail("Missing Authorization Header");

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Token");
        }
    }
}
```

### Advanced Security Features

#### Rate Limiting
```csharp
public class RateLimitingMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var clientId = GetClientId(context);
        if (await IsRateLimitExceededAsync(clientId))
        {
            context.Response.StatusCode = 429; // Too Many Requests
            return;
        }
        
        await next(context);
    }
}
```

#### Account Lockout
```csharp
public class AccountLockoutService
{
    public async Task<bool> IsAccountLockedAsync(string username)
    {
        var attempts = await GetFailedAttemptsAsync(username);
        return attempts.Count >= 5 && 
               attempts.Max(a => a.Timestamp) > DateTime.UtcNow.AddMinutes(-15);
    }
    
    public async Task RecordFailedAttemptAsync(string username)
    {
        await SaveFailedAttemptAsync(new FailedAttempt 
        { 
            Username = username, 
            Timestamp = DateTime.UtcNow 
        });
    }
}
```

## 🎓 Practice Exercises

### Beginner Level

1. **Add New Role**: Create a "Moderator" role with access to specific endpoints
2. **Database Users**: Store users in a database instead of hardcoded values
3. **Unique Salts**: Generate unique salt per user instead of fixed salt
4. **Password Validation**: Add password strength requirements

### Intermediate Level

5. **JWT Implementation**: Replace custom handler with JWT token authentication
6. **Refresh Tokens**: Implement token refresh mechanism
7. **Role Hierarchy**: Create role inheritance (Admin includes Moderator permissions)
8. **Session Management**: Add session timeout and renewal

### Advanced Level

9. **Multi-Factor Authentication**: Add SMS or TOTP second factor
10. **OAuth Integration**: Add Google/Microsoft OAuth provider
11. **Rate Limiting**: Implement request rate limiting per user
12. **Audit Logging**: Log all authentication and authorization events

### Expert Level

13. **Zero-Trust Architecture**: Implement comprehensive security model
14. **Certificate Authentication**: Add client certificate validation
15. **Distributed Security**: Security across microservices architecture
16. **Security Headers**: Implement comprehensive security headers

## 🔧 Troubleshooting

### Common Issues

#### Authentication Not Working
```bash
# Check middleware order
app.UseRouting();
app.UseAuthentication();    # Must come before UseAuthorization
app.UseAuthorization();
```

#### Claims Not Found
```csharp
// Verify claim creation
var claims = new[] { new Claim("Role", "Admin") };  // Exact claim name
var identity = new ClaimsIdentity(claims);          // Identity must have claims
```

#### Policy Not Applied
```csharp
// Check policy registration
builder.Services.AddAuthorization(options => {
    options.AddPolicy("PolicyName", policy => policy.RequireClaim("Role", "Value"));
});

// Check endpoint attribution
app.MapGet("/api/endpoint", [Authorize(Policy = "PolicyName")] () => { });
```

#### Hash Comparison Failing
```csharp
// Ensure same salt and parameters
var hash1 = HashPassword("password", salt);
var hash2 = HashPassword("password", salt);
// hash1 should equal hash2
```

## 📚 Further Reading

- [ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security/)
- [OWASP Password Storage Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html)
- [PBKDF2 Specification](https://tools.ietf.org/html/rfc2898)
- [Claims-Based Authentication](https://docs.microsoft.com/en-us/dotnet/framework/security/claims-based-identity-model)
- [JWT Authentication in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn)
- [NIST Password Guidelines](https://pages.nist.gov/800-63-3/sp800-63b.html)