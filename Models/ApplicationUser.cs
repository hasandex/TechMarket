namespace TechMarket.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FName {  get; set; }
        public string LName { get; set; }
        public byte[]? ProfilePicture { get; set; }    
    }
}
