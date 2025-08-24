namespace Domain.Entities
{
    public sealed class Testimonial : Entity
    {
        public string AuthorName { get; private set; }
        public string AuthorAvatarUrl { get; private set; }
        public string? AuthorBadge { get; private set; }
        public int Rating { get; private set; }
        public string Content { get; private set; }

        public Testimonial() { }

        public Testimonial(Guid id, string authorName, string avatarUrl, int rating, string content, string? badge = null)
        {
            Id = id;
            AuthorName = authorName;
            AuthorAvatarUrl = avatarUrl;
            Rating = rating;
            Content = content;
            AuthorBadge = badge;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string authorName, string avatarUrl, int rating, string content, string? badge = null)
        {
            AuthorName = authorName;
            AuthorAvatarUrl = avatarUrl;
            Rating = rating;
            Content = content;
            AuthorBadge = badge;
            UpdatedAt = DateTime.UtcNow;
        }
    }

}
