namespace CleanArchitecture.Domain
{
    public class Actor
    {
        public Actor()
        {
            Videos = new HashSet<Video>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}
