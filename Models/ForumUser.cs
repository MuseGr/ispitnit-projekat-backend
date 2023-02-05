using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class ForumUser : IdentityUser
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(256)]
        public override string Id { get => base.Id; set => base.Id = value; }
        [StringLength(256)]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [StringLength(128)]
        public override string Email { get => base.Email; set => base.Email = value; }
        [StringLength(128)]
        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        [StringLength(256)]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        [StringLength(256)]
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        [StringLength(128)]
        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        [StringLength(128)]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        [StringLength(256)]
        public string? EmailVerificationToken { get; set; }
        [StringLength(256)]
        public string? ReactivationToken { get; set; }

        public bool IsActive { get; set; }

        public bool IsProfileCompleted { get; set; }
        [StringLength(128)]
        public string? FirstName { get; set; }
        [StringLength(128)]
        public string? LastName { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime EmailVerificationTokenCreated { get; set; }
        public DateTime ReactivationTokenCreated { get; set; }
        public string DisplayName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public List<Post> PostsList { get; set; }
    }
}
