# Meraki API Endpoints

## Authentication Endpoints

### Login
**POST** `/api/auth/login`

Authenticates a user and returns a JWT token.

**Request Body:**
```json
{
  "email": "admin@meraki.com",
  "password": "Admin123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "admin@meraki.com",
  "role": "Admin",
  "userId": "guid-here",
  "expiresAt": "2024-01-01T12:00:00Z"
}
```

**Status Codes:**
- `200 OK` - Login successful
- `401 Unauthorized` - Invalid credentials

---

### Register
**POST** `/api/auth/register`

Creates a new user account. Requires Admin authentication.

**Headers:**
```
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123!",
  "firstName": "John",
  "lastName": "Doe",
  "role": "Admin"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "user@example.com",
  "role": "Admin",
  "userId": "guid-here",
  "expiresAt": "2024-01-01T12:00:00Z"
}
```

**Status Codes:**
- `201 Created` - User created successfully
- `400 Bad Request` - Validation errors
- `401 Unauthorized` - Not authenticated
- `403 Forbidden` - Not authorized (requires Admin role)
- `409 Conflict` - Email already exists

---

## Protected Admin Endpoints

All **POST**, **PUT**, and **DELETE** endpoints on the following controllers now require Admin authentication:

- `/api/benefits`
- `/api/categories`
- `/api/courses`
- `/api/services`
- `/api/specialties`
- `/api/testimonials`
- `/api/therapists`

**Headers Required:**
```
Authorization: Bearer {token}
```

**Status Codes:**
- `401 Unauthorized` - Missing or invalid token
- `403 Forbidden` - User does not have Admin role

---

## Default Admin Credentials

- **Email:** `admin@meraki.com`
- **Password:** `Admin123!`

---

## Notes

- JWT tokens expire after 60 minutes (configurable in `appsettings.json`)
- All entity changes are automatically tracked in the audit log
- `CreatedBy` and `UpdatedBy` fields are automatically set on all entities

