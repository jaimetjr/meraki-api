using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public sealed class User : AggregateRoot
    {
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public bool IsActive { get; private set; }

        // EF Core parameterless constructor
        private User() { }

        public User(Guid id, string email, string passwordHash, string role)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty", nameof(id));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("PasswordHash is required", nameof(passwordHash));
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentException("Role is required", nameof(role));
            if (!IsValidEmail(email)) throw new ArgumentException("Invalid email format", nameof(email));

            Id = id;
            Email = email.ToLowerInvariant();
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            MarkAsModified();
        }

        public void UpdatePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("PasswordHash is required", nameof(newPasswordHash));

            PasswordHash = newPasswordHash;
            MarkAsModified();
        }

        public void UpdateRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role is required", nameof(role));

            Role = role;
            MarkAsModified();
        }

        public void Activate()
        {
            IsActive = true;
            MarkAsModified();
        }

        public void Deactivate()
        {
            IsActive = false;
            MarkAsModified();
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

