namespace ShortProject.Models
{
    public class User : BaseEntity
    {
        internal string Fullname;

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

    }

}

