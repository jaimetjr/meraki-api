# Enums Reference

This document contains all enums used in the Meraki API for frontend integration.

## CourseStatus

**Enum Values:**
```typescript
enum CourseStatus {
  EmBreve = "EmBreve",
  InscricoesAbertas = "InscricoesAbertas",
  Disponivel = "Disponivel"
}
```

**Display Names (for UI):**
- `EmBreve` → "Em Breve"
- `InscricoesAbertas` → "Inscrições Abertas"
- `Disponivel` → "Disponivel"

**JSON Values:**
```json
{
  "EmBreve": "EmBreve",
  "InscricoesAbertas": "InscricoesAbertas",
  "Disponivel": "Disponivel"
}
```

---

## CourseType

**Enum Values:**
```typescript
enum CourseType {
  Curso = "Curso",
  Mentoria = "Mentoria"
}
```

**JSON Values:**
```json
{
  "Curso": "Curso",
  "Mentoria": "Mentoria"
}
```

---

## Modality

**Enum Values:**
```typescript
enum Modality {
  Online = "Online",
  Presencial = "Presencial",
  Hibrido = "Hibrido"
}
```

**JSON Values:**
```json
{
  "Online": "Online",
  "Presencial": "Presencial",
  "Hibrido": "Hibrido"
}
```

---

## TypeScript/JavaScript Implementation

```typescript
// CourseStatus Enum
export enum CourseStatus {
  EmBreve = 'EmBreve',
  InscricoesAbertas = 'InscricoesAbertas',
  Disponivel = 'Disponivel'
}

// CourseStatus Display Names Map
export const CourseStatusDisplayNames: Record<CourseStatus, string> = {
  [CourseStatus.EmBreve]: 'Em Breve',
  [CourseStatus.InscricoesAbertas]: 'Inscrições Abertas',
  [CourseStatus.Disponivel]: 'Disponivel'
};

// CourseType Enum
export enum CourseType {
  Curso = 'Curso',
  Mentoria = 'Mentoria'
}

// Modality Enum
export enum Modality {
  Online = 'Online',
  Presencial = 'Presencial',
  Hibrido = 'Hibrido'
}

// Helper function to get display name
export function getCourseStatusDisplayName(status: CourseStatus): string {
  return CourseStatusDisplayNames[status] || status;
}
```

---

## API Response Format

When the API returns enum values, they will be in the following format:

**Course DTO Example:**
```json
{
  "id": "guid-here",
  "title": "Fundamentos do Mindfulness",
  "description": "Aprenda os princípios básicos",
  "type": "Curso",
  "status": "Disponivel",
  "modality": "Online",
  "price": 150,
  "currency": "BRL"
}
```

**Note:** The API may also return display names using the `GetDisplayName()` extension method. Check the actual API response to see which format is used.

---

---

## User Roles

**Note:** Roles are stored as strings, not enums. Currently supported roles:

**Role Values:**
```typescript
type UserRole = 'Admin' | string; // Currently only 'Admin' is used
```

**JSON Values:**
```json
{
  "Admin": "Admin"
}
```

**Usage:**
- Default role for new users: `"Admin"`
- All admin endpoints require role: `"Admin"`

---

## Usage in Forms

When creating or updating courses, use these enum values:

**Create Course Request:**
```json
{
  "title": "Fundamentos do Mindfulness",
  "description": "Aprenda os princípios",
  "type": "Curso",
  "status": "Disponivel",
  "modality": "Online",
  "price": 150,
  "currency": "BRL"
}
```

**Valid Values:**
- `type`: `"Curso"` or `"Mentoria"`
- `status`: `"EmBreve"`, `"InscricoesAbertas"`, or `"Disponivel"`
- `modality`: `"Online"`, `"Presencial"`, or `"Hibrido"`

**Create User Request:**
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123!",
  "firstName": "John",
  "lastName": "Doe",
  "role": "Admin"
}
```

**Valid Role Values:**
- `role`: `"Admin"` (currently the only supported role)

