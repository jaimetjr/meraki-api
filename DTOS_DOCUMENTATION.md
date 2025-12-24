# DTOs and Their Connections

This document provides a comprehensive overview of all DTOs in the application and their related components.

## DTO Overview

The application has **13 DTOs** organized in the `Application/DTOs` namespace:

### Entity DTOs (with AutoMapper mappings)
1. **BenefitDto** - Maps to `Benefit` entity
2. **CategoryDto** - Maps to `Category` entity
3. **CourseDto** - Maps to `Course` entity
4. **ServiceDto** - Maps to `Service` entity
5. **SpecialtyDto** - Maps to `Specialty` entity
6. **TestimonialDto** - Maps to `Testimonial` entity
7. **TherapistDto** - Maps to `Therapist` entity
8. **UserDto** - Maps to `User` entity

### Authentication DTOs
9. **AuthResponseDto** - Response after login/register
10. **LoginDto** - Login request
11. **RegisterDto** - User registration request

### Contact DTOs
12. **ContactRequestDto** - Contact form request
13. **ContactResponseDto** - Contact form response

---

## Detailed DTO Information

### 1. BenefitDto
**File:** `Application/DTOs/BenefitDto.cs`

**Properties:**
- `Guid Id`
- `string Title`
- `string? Description`

**AutoMapper Mapping:**
- `Benefit ↔ BenefitDto` (reverse mapping enabled)

**Validator:** `BenefitValidator`
- Title: Required, MaxLength(100)
- Description: Optional, MaxLength(300)

**Controller:** `BenefitsController`
- GET `/api/benefits`
- GET `/api/benefits/{id}`
- GET `/api/benefits/search/{keyword}`
- POST `/api/benefits` [Admin]
- PUT `/api/benefits/{id}` [Admin]
- DELETE `/api/benefits/{id}` [Admin]

**Service:** `IBenefitService` / `BenefitService`

**Used in:** `ServiceDto.Benefits` (List<BenefitDto>)

---

### 2. CategoryDto
**File:** `Application/DTOs/CategoryDto.cs`

**Properties:**
- `Guid Id`
- `string Name`

**AutoMapper Mapping:**
- `Category ↔ CategoryDto` (reverse mapping enabled)

**Validator:** `CategoryValidator`
- Name: Required, MaxLength(50)

**Controller:** `CategoriesController`
- GET `/api/categories`
- GET `/api/categories/{id}`
- POST `/api/categories` [Admin]
- PUT `/api/categories/{id}` [Admin]
- DELETE `/api/categories/{id}` [Admin]

**Service:** `ICategoryService` / `CategoryService`

**Used in:** `ServiceDto.Category` (CategoryDto?)

---

### 3. CourseDto
**File:** `Application/DTOs/CourseDto.cs`

**Properties:**
- `Guid Id`
- `string Title`
- `string Description`
- `string Image`
- `string Instructor`
- `DateTime? Date`
- `string? Modality` (Enum as string)
- `decimal? Price`
- `string Currency` (default: "BRL")
- `string Type` (Enum as string)
- `string Status` (Enum as string)
- `string? Link`

**AutoMapper Mapping:**
- `Course ↔ CourseDto` (complex mapping)
  - Maps `Money` value object to `Price` and `Currency` properties
  - Converts enums to display names using `GetDisplayName()`
  - Reverse mapping converts Price/Currency back to `Money` value object

**Validator:** `CourseValidator`
- Title: Required, MaxLength(100)
- Description: Required, MaxLength(500)
- Image: Required
- Instructor: Required
- Type: Must be valid CourseType enum name
- Status: Must be valid CourseStatus enum name
- Modality: Optional, must be valid Modality enum name
- Price: Optional, >= 0
- Currency: Length(3) when Price is present

**Controller:** `CoursesController`
- GET `/api/courses`
- GET `/api/courses/{id}`
- GET `/api/courses/status/{status}`
- POST `/api/courses` [Admin]
- PUT `/api/courses/{id}` [Admin]
- DELETE `/api/courses/{id}` [Admin]

**Service:** `ICourseService` / `CourseService`

---

### 4. ServiceDto
**File:** `Application/DTOs/ServiceDto.cs`

**Properties:**
- `Guid Id`
- `string Name`
- `string Description`
- `string Image`
- `string? LongDescription`
- `string? Duration`
- `decimal Price`
- `string Currency` (default: "BRL")
- `CategoryDto? Category`
- `List<BenefitDto>? Benefits`

**AutoMapper Mapping:**
- `Service ↔ ServiceDto` (complex mapping)
  - Maps `Money` value object to `Price` and `Currency` properties
  - Reverse mapping converts Price/Currency back to `Money` value object
  - Includes nested CategoryDto and BenefitDto mappings

**Validator:** `ServiceValidator`
- Name: Required, MaxLength(100)
- Description: Required, MaxLength(300)
- Image: Required
- Price: Required, >= 0
- Currency: Required, Length(3)
- Benefits: Validates each BenefitDto using BenefitValidator
- Category: Validates CategoryDto using CategoryValidator when present

**Controller:** `ServicesController`
- GET `/api/services`
- GET `/api/services/{id}`
- GET `/api/services/category/{category}`
- POST `/api/services` [Admin]
- PUT `/api/services/{id}` [Admin]
- DELETE `/api/services/{id}` [Admin]

**Service:** `IServiceManager` / `ServiceManager`

**Service Creation/Update Flow:**
The `ServiceManager.CreateAsync` and `UpdateAsync` methods use explicit entity constructors instead of AutoMapper for entity creation. This ensures proper validation and initialization:

- **Entity Creation:** Services are created using the `Service` constructor (no ID parameter - EF Core generates IDs automatically)
- **Category Handling:**
  - If `CategoryDto.Id` is provided and not empty, loads existing category from repository
  - If category doesn't exist or no ID provided, creates new `Category` entity and adds it to repository for EF Core tracking
  - Uses `Service.UpdateCategory()` method which only sets `CategoryId` foreign key if category already has an ID
- **Benefit Handling:**
  - If `BenefitDto.Id` is provided and not empty, loads existing benefit from repository
  - If benefit doesn't exist or no ID provided, creates new `Benefit` entity and adds it to repository for EF Core tracking
  - Uses `Service.AddBenefit()` method which checks for duplicates by reference equality or ID

**Entity Methods:**
- `Service.UpdateCategory(Category category)`: Sets category navigation property and foreign key (only if category has ID)
- `Service.AddBenefit(Benefit benefit)`: Adds benefit to collection with duplicate checking (by reference or ID)

**Note:** All repositories share the same scoped DbContext, so entities added to one repository are automatically tracked and available to others. New Category/Benefit entities are explicitly added to their repositories before being associated with the Service to ensure proper EF Core change tracking.

---

### 5. SpecialtyDto
**File:** `Application/DTOs/SpecialtyDto.cs`

**Properties:**
- `Guid Id`
- `string Name`
- `string? Description`

**AutoMapper Mapping:**
- `Specialty ↔ SpecialtyDto` (reverse mapping enabled)

**Validator:** `SpecialtyValidator`
- Name: Required, MaxLength(100)
- Description: Optional, MaxLength(300)

**Controller:** `SpecialtiesController`
- GET `/api/specialties`
- GET `/api/specialties/{id}`
- GET `/api/specialties/name/{name}`
- POST `/api/specialties` [Admin]
- PUT `/api/specialties/{id}` [Admin]
- DELETE `/api/specialties/{id}` [Admin]

**Service:** `ISpecialtyService` / `SpecialtyService`

**Used in:** `TherapistDto.Specialties` (List<SpecialtyDto>)

---

### 6. TestimonialDto
**File:** `Application/DTOs/TestimonialDto.cs`

**Properties:**
- `Guid Id`
- `string AuthorName`
- `string AuthorAvatarUrl`
- `string? AuthorBadge`
- `int Rating`
- `string Content`

**AutoMapper Mapping:**
- `Testimonial ↔ TestimonialDto` (reverse mapping enabled)

**Validator:** `TestimonialValidator`
- AuthorName: Required, MaxLength(100)
- AuthorAvatarUrl: Required, MaxLength(300)
- AuthorBadge: Optional, MaxLength(50)
- Rating: Between 1 and 5 (inclusive)
- Content: Required, MaxLength(1000)

**Controller:** `TestimonialsController`
- GET `/api/testimonials`
- GET `/api/testimonials/{id}`
- GET `/api/testimonials/top/{count}`
- POST `/api/testimonials` [Admin]
- PUT `/api/testimonials/{id}` [Admin]
- DELETE `/api/testimonials/{id}` [Admin]

**Service:** `ITestimonialService` / `TestimonialService`

---

### 7. TherapistDto
**File:** `Application/DTOs/TherapistDto.cs`

**Properties:**
- `Guid Id`
- `string Name`
- `string Bio`
- `string Image`
- `string Experience`
- `string Education`
- `List<SpecialtyDto> Specialties`

**AutoMapper Mapping:**
- `Therapist ↔ TherapistDto` (reverse mapping enabled)
- Includes nested SpecialtyDto mappings

**Validator:** `TherapistValidator`
- Name: Required, MaxLength(100)
- Bio: Required, MaxLength(500)
- Image: Required
- Experience: Required
- Education: Required
- Specialties: Validates each SpecialtyDto using SpecialtyValidator

**Controller:** `TherapistsController`
- GET `/api/therapists`
- GET `/api/therapists/{id}`
- GET `/api/therapists/specialty/{name}`
- POST `/api/therapists` [Admin]
- PUT `/api/therapists/{id}` [Admin]
- DELETE `/api/therapists/{id}` [Admin]

**Service:** `ITherapistService` / `TherapistService`

---

### 8. UserDto
**File:** `Application/DTOs/UserDto.cs`

**Properties:**
- `Guid Id`
- `string Email`
- `string Role`
- `string? FirstName`
- `string? LastName`
- `bool IsActive`

**AutoMapper Mapping:**
- `User ↔ UserDto` (reverse mapping enabled)
- **PasswordHash is excluded** from mapping for security

**Validator:** None (no validator currently exists)

**Controller:** None (no controller currently uses UserDto)

**Service:** None (no service currently uses UserDto)

**Note:** This DTO was recently created and is not yet fully integrated into controllers/services.

---

### 9. AuthResponseDto
**File:** `Application/DTOs/AuthResponseDto.cs`

**Properties:**
- `string Token`
- `string Email`
- `string Role`
- `Guid UserId`
- `DateTime ExpiresAt`

**AutoMapper Mapping:** None (not mapped from entity)

**Validator:** None

**Controller:** `AuthController`
- Used as return type for:
  - POST `/api/auth/login`
  - POST `/api/auth/register`

**Service:** `IAuthService` / `AuthService`
- `LoginAsync(LoginDto)` → `AuthResponseDto`
- `RegisterAsync(RegisterDto)` → `AuthResponseDto`

---

### 10. LoginDto
**File:** `Application/DTOs/LoginDto.cs`

**Properties:**
- `string Email`
- `string Password`

**AutoMapper Mapping:** None (not mapped from entity)

**Validator:** None

**Controller:** `AuthController`
- POST `/api/auth/login` [AllowAnonymous]

**Service:** `IAuthService` / `AuthService`
- `LoginAsync(LoginDto)` → `AuthResponseDto`

---

### 11. RegisterDto
**File:** `Application/DTOs/RegisterDto.cs`

**Properties:**
- `string Email`
- `string Password`
- `string? FirstName`
- `string? LastName`
- `string Role` (default: "Admin")

**AutoMapper Mapping:** None (not mapped from entity, manually creates User entity)

**Validator:** None

**Controller:** `AuthController`
- POST `/api/auth/register` [Admin]

**Service:** `IAuthService` / `AuthService`
- `RegisterAsync(RegisterDto)` → `AuthResponseDto`
- Manually creates `User` entity and maps properties

---

### 12. ContactRequestDto
**File:** `Application/DTOs/ContactRequestDto.cs`

**Properties:**
- `string Name`
- `string Email`
- `string Phone`
- `string Service`
- `string? Message`

**AutoMapper Mapping:** None

**Validator:** None

**Controller:** None (not currently used in any controller)

**Service:** None (not currently used in any service)

**Note:** This DTO exists but is not currently integrated into the application.

---

### 13. ContactResponseDto
**File:** `Application/DTOs/ContactResponseDto.cs`

**Properties:**
- `bool Success`
- `string Message`

**AutoMapper Mapping:** None

**Validator:** None

**Controller:** None (not currently used in any controller)

**Service:** None (not currently used in any service)

**Note:** This DTO exists but is not currently integrated into the application.

---

## DTO Relationships

### Nested DTOs
- **ServiceDto** contains:
  - `CategoryDto?` (optional)
  - `List<BenefitDto>?` (optional)

- **TherapistDto** contains:
  - `List<SpecialtyDto>` (required)

### DTOs with Value Object Mappings
- **CourseDto** and **ServiceDto** map `Money` value objects to separate `Price` (decimal) and `Currency` (string) properties

### Enum Mappings
- **CourseDto** maps enum types to strings:
  - `Modality` enum → string (with display name support)
  - `Type` (CourseType) enum → string (with display name support)
  - `Status` (CourseStatus) enum → string (with display name support)

---

## AutoMapper Configuration

All entity-to-DTO mappings are configured in `Application/Mapping/AutoMapperProfile.cs`:

```csharp
// Simple mappings with ReverseMap()
CreateMap<Therapist, TherapistDto>().ReverseMap();
CreateMap<Category, CategoryDto>().ReverseMap();
CreateMap<Benefit, BenefitDto>().ReverseMap();
CreateMap<Specialty, SpecialtyDto>().ReverseMap();
CreateMap<Testimonial, TestimonialDto>().ReverseMap();
CreateMap<User, UserDto>().ReverseMap()
    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

// Complex mappings with value object conversions
CreateMap<Service, ServiceDto>()
    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
    .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency))
    .ReverseMap()
    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price, src.Currency)));

CreateMap<Course, CourseDto>()
    // ... enum and value object mappings ...
    .ReverseMap()
    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price ?? 0, src.Currency)));
```

---

## Validators

All validators use FluentValidation and are located in `Application/Validators/`:

1. **BenefitValidator** - Validates BenefitDto
2. **CategoryValidator** - Validates CategoryDto
3. **CourseValidator** - Validates CourseDto (includes enum validation)
4. **ServiceValidator** - Validates ServiceDto (includes nested validators)
5. **SpecialtyValidator** - Validates SpecialtyDto
6. **TestimonialValidator** - Validates TestimonialDto
7. **TherapistValidator** - Validates TherapistDto (includes nested validators)

**Missing Validators:**
- UserDto (no validator)
- LoginDto (no validator)
- RegisterDto (no validator)
- ContactRequestDto (no validator)
- ContactResponseDto (no validator)
- AuthResponseDto (no validator - typically not validated)

---

## Summary Statistics

- **Total DTOs:** 13
- **Entity DTOs (with AutoMapper):** 8
- **Authentication DTOs:** 3
- **Contact DTOs:** 2
- **DTOs with Validators:** 7
- **DTOs used in Controllers:** 9
- **DTOs with nested DTOs:** 2 (ServiceDto, TherapistDto)
- **DTOs not yet integrated:** 4 (UserDto, ContactRequestDto, ContactResponseDto)

---

## Entity Creation Patterns

### Constructor-Based Entity Creation
All entity constructors have been updated to **not require ID parameters**. Entity Framework Core automatically generates IDs when entities are saved to the database. This applies to:
- `Service` - Constructor takes: `(string name, string description, string image, Money money)`
- `Therapist` - Constructor takes: `(string name, string bio, string image, string experience, string education)`
- `Testimonial` - Constructor takes: `(string authorName, string avatarUrl, int rating, string content, string? badge)`
- `Course` - Constructor takes: `(string title, string description, string image, string instructor, CourseType type, CourseStatus status)`
- `Category` - Constructor takes: `(string name)`
- `Benefit` - Constructor takes: `(string title, string? description)`
- `Specialty` - Constructor takes: `(string name, string? description)`

### Service Layer Patterns
Service `CreateAsync` methods:
1. Create entity instances using constructors (not AutoMapper)
2. For nested entities (Category, Benefit, Specialty):
   - Try to load existing entities by ID if provided
   - If not found or no ID provided, create new entities and add them to their repositories
   - Associate entities with parent entity
3. Add parent entity to repository
4. Return mapped DTO

This ensures proper EF Core change tracking and relationship management.

## Notes

1. **UserDto** was recently created and has AutoMapper mapping but is not yet used in any controllers or services.

2. **ContactRequestDto** and **ContactResponseDto** exist but are not currently used anywhere in the application.

3. Authentication DTOs (LoginDto, RegisterDto, AuthResponseDto) do not use AutoMapper as they are not direct mappings from entities.

4. All CRUD operations on entity DTOs require Admin authorization except for GET operations.

5. All entity DTOs follow a consistent pattern with:
   - AutoMapper configuration (for reading/mapping entities to DTOs)
   - FluentValidation validators
   - Controller endpoints
   - Service layer implementation
   - **Entity creation uses constructors, not AutoMapper reverse mapping**

6. **Entity Framework ID Generation:** All entity IDs are automatically generated by EF Core. Entity constructors do not accept ID parameters, and services do not manually generate IDs.

