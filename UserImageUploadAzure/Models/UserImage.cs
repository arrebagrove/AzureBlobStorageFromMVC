namespace UserImageUploadAzure.Models
{
    public class UserImage
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
