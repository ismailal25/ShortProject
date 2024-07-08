namespace ShortProject.Models
{
    public abstract class BaseEntity
    {
        private static int _id = 0;

        public int Id { get;  set; }


        protected BaseEntity()
        {
            Id = ++_id;
        }
    }
}
