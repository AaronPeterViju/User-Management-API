# User Management API

## How to Run

```bash
dotnet build
dotnet run
```

The API will start on a local port (e.g., `http://localhost:5267`).

## Testing

Use the included `UserManagementAPI.http` file to test all endpoints, or use Postman/curl with:
- **Authorization Header:** `Bearer my-secret-token-12345`

## API Endpoints

- **GET** `/api/users` - Get all users
- **GET** `/api/users/{id}` - Get user by ID
- **POST** `/api/users` - Create user
- **PUT** `/api/users/{id}` - Update user
- **DELETE** `/api/users/{id}` - Delete user

## Microsoft Copilot Contributions

### 1. Code Generation
- Generated boilerplate code for Program.cs and project structure
- Created all CRUD endpoint implementations in UsersController
- Scaffolded User model with Data Annotations ([Required], [EmailAddress], [StringLength])

### 2. Debugging and Bug Fixes
Copilot helped identify and resolve the following bugs:

**Bug #1: Missing Null Check in GetUserById()**
- **Problem:** Method returned null for non-existent users instead of proper 404 response
- **Fix:** Added null check and return NotFound() with error message
- **Impact:** API now properly handles requests for non-existent users

**Bug #2: Missing Validation in CreateUser()**
- **Problem:** Endpoint accepted invalid data (empty names, invalid emails)
- **Fix:** Added ModelState.IsValid check before processing
- **Impact:** Only valid user data is now processed, preventing data integrity issues

**Bug #3: Missing Validation & Null Check in UpdateUser()**
- **Problem:** No validation of input data and no handling of non-existent users
- **Fix:** Added both ModelState validation and null check with 404 response
- **Impact:** Ensures data integrity and proper error responses

**Bug #4: Missing Error Handling in GetAllUsers()**
- **Problem:** No try-catch block, unhandled exceptions could crash the API
- **Fix:** Wrapped code in try-catch block with proper logging
- **Impact:** API remains stable even when errors occur

### 3. Middleware Implementation
- Generated three middleware components (ErrorHandling, Authentication, Logging)
- Suggested optimal middleware pipeline order (Error → Auth → Logging)
- Added try-catch blocks for exception handling throughout

### 4. Validation & Error Handling
- Implemented ModelState validation with detailed error messages
- Added proper HTTP status codes (200, 201, 400, 401, 404, 500)
- Created consistent error response format
   - Added XML documentation comments
   - Implemented proper HTTP status codes
   - Enhanced error messages for better debugging

## Additional Features

- In-memory data storage with UserRepository
- Pre-populated sample data for testing
- Health check endpoint at /health (bypasses authentication)
- Comprehensive logging for auditing
- Consistent error response format
- Proper HTTP status codes
- RESTful API design

## Notes

- This is an in-memory implementation (data resets on restart)
- For production, replace UserRepository with a database (Entity Framework Core)
- Token validation is simplified for demonstration purposes
- All timestamps use UTC
- Generated boilerplate code for Program.cs and project structure
- Created all CRUD endpoint implementations in UsersController
- Scaffolded User model with Data Annotations for validation ([Required], [EmailAddress], [StringLength])
- Generated three middleware components (ErrorHandling, Authentication, Logging)
- Suggested optimal middleware pipeline order (Error → Auth → Logging)
- Added error handling for non-existent user lookups (404 responses)
- Implemented ModelState validation with detailed error messages
- Generated try-catch blocks for exception handling
- Suggested proper HTTP status codes (200, 201, 400, 401, 404, 500)