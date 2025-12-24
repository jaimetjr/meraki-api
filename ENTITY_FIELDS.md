# Entity Fields Reference

This document lists all required and optional fields for each entity in the Meraki API.

## Service

### Required Fields (*)
- `name` (string) - Max 100 characters
- `description` (string) - Max 500 characters
- `image` (string)
- `price` (decimal)
- `currency` (string) - Default: "BRL"

### Optional Fields
- `id` (Guid) - Auto-generated if not provided
- `longDescription` (string) - Max 2000 characters
- `duration` (string) - Max 50 characters
- `category` (CategoryDto) - Can be set via UpdateCategory method
- `benefits` (List<BenefitDto>) - Can be added via AddBenefit method

### Example Request (POST/PUT)
```json
{
  "name": "Respiração Consciente",
  "description": "Técnica de relaxamento com foco na respiração",
  "image": "/images/breathing.jpg",
  "price": 120,
  "currency": "BRL",
  "longDescription": "Exercício respiratório para aliviar ansiedade",
  "duration": "30 minutos",
  "category": {
    "id": "guid-here",
    "name": "Terapia"
  },
  "benefits": [
    {
      "id": "guid-here",
      "title": "Alívio do Estresse",
      "description": "Ajuda a reduzir o estresse"
    }
  ]
}
```

---

## Course

### Required Fields (*)
- `title` (string) - Max 200 characters
- `description` (string) - Max 1000 characters
- `image` (string)
- `instructor` (string)
- `type` (string) - Enum: "Curso" or "Mentoria"
- `status` (string) - Enum: "EmBreve", "InscricoesAbertas", or "Disponivel"

### Optional Fields
- `id` (Guid) - Auto-generated if not provided
- `date` (DateTime) - Must be in the future if provided
- `modality` (string) - Enum: "Online", "Presencial", or "Hibrido"
- `price` (decimal)
- `currency` (string) - Default: "BRL"
- `link` (string) - URL

### Example Request (POST/PUT)
```json
{
  "title": "Fundamentos do Mindfulness",
  "description": "Aprenda os princípios básicos da atenção plena",
  "image": "/images/mindfulness.jpg",
  "instructor": "Carlos Lima",
  "type": "Curso",
  "status": "Disponivel",
  "date": "2025-01-10T00:00:00Z",
  "modality": "Online",
  "price": 150,
  "currency": "BRL",
  "link": "https://exemplo.com/mindfulness"
}
```

---

## Therapist

### Required Fields (*)
- `name` (string) - Max 100 characters
- `bio` (string) - Max 500 characters
- `image` (string)
- `experience` (string)
- `education` (string)

### Optional Fields
- `id` (Guid) - Auto-generated if not provided
- `specialties` (List<SpecialtyDto>) - Can be added via AddSpecialty method

### Example Request (POST/PUT)
```json
{
  "name": "Dra. Ana Souza",
  "bio": "Psicóloga clínica com mais de 10 anos de experiência",
  "image": "/images/ana.jpg",
  "experience": "10 anos atuando com TCC e trauma",
  "education": "Doutorado em Psicologia",
  "specialties": [
    {
      "id": "guid-here",
      "name": "Ansiedade",
      "description": "Apoio para lidar com crises de ansiedade"
    }
  ]
}
```

---

## Category

### Required Fields (*)
- `name` (string) - Max 100 characters

### Optional Fields
- `id` (Guid) - Auto-generated if not provided

### Example Request (POST/PUT)
```json
{
  "name": "Terapia"
}
```

---

## Benefit

### Required Fields (*)
- `title` (string) - Max 200 characters

### Optional Fields
- `id` (Guid) - Auto-generated if not provided
- `description` (string) - Max 1000 characters

### Example Request (POST/PUT)
```json
{
  "title": "Alívio do Estresse",
  "description": "Ajuda a reduzir o estresse e a ansiedade"
}
```

---

## Specialty

### Required Fields (*)
- `name` (string) - Max 100 characters

### Optional Fields
- `id` (Guid) - Auto-generated if not provided
- `description` (string) - Max 500 characters

### Example Request (POST/PUT)
```json
{
  "name": "Ansiedade",
  "description": "Apoio para lidar com crises de ansiedade"
}
```

---

## Testimonial

### Required Fields (*)
- `authorName` (string) - Max 100 characters
- `authorAvatarUrl` (string)
- `rating` (int) - Must be between 1 and 5 (inclusive)
- `content` (string) - Max 1000 characters

### Optional Fields
- `id` (Guid) - Auto-generated if not provided
- `authorBadge` (string)

### Example Request (POST/PUT)
```json
{
  "authorName": "Juliana Costa",
  "authorAvatarUrl": "/images/juliana.jpg",
  "rating": 5,
  "content": "Esse serviço transformou minha vida!",
  "authorBadge": "Verificado"
}
```

---

## User

### Required Fields (*)
- `email` (string) - Must be valid email format
- `password` (string) - For registration only (will be hashed)
- `role` (string) - Currently only "Admin" is supported

### Optional Fields
- `id` (Guid) - Auto-generated if not provided
- `firstName` (string)
- `lastName` (string)

### Example Request (POST /api/auth/register)
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123!",
  "firstName": "John",
  "lastName": "Doe",
  "role": "Admin"
}
```

---

## Field Validation Rules Summary

### String Length Limits
- **Service**: name (100), description (500), longDescription (2000), duration (50)
- **Course**: title (200), description (1000)
- **Therapist**: name (100), bio (500)
- **Category**: name (100)
- **Benefit**: title (200), description (1000)
- **Specialty**: name (100), description (500)
- **Testimonial**: authorName (100), content (1000)

### Numeric Ranges
- **Testimonial.rating**: 1-5 (inclusive)
- **Course.date**: Must be in the future if provided

### Enum Values
- **Course.type**: "Curso", "Mentoria"
- **Course.status**: "EmBreve", "InscricoesAbertas", "Disponivel"
- **Course.modality**: "Online", "Presencial", "Hibrido"
- **User.role**: "Admin"

### Auto-Generated Fields
The following fields are automatically set by the system and should NOT be included in POST requests:
- `id` (Guid) - Generated on creation
- `createdAt` (DateTime) - Set automatically
- `updatedAt` (DateTime) - Set automatically
- `createdBy` (Guid?) - Set automatically from JWT token
- `updatedBy` (Guid?) - Set automatically from JWT token

---

## Notes

1. **Relationships**: When creating entities with relationships (e.g., Service with Category, Therapist with Specialties), ensure the related entities exist first or include their full object structure.

2. **Collections**: Collections like `benefits`, `specialties` are optional and can be empty arrays or omitted entirely.

3. **Nullable Fields**: Fields marked with `?` in TypeScript/C# can be `null` or omitted from the request.

4. **Currency**: Default currency is "BRL" if not specified.

5. **Date Format**: Use ISO 8601 format for dates: `"2025-01-10T00:00:00Z"`

