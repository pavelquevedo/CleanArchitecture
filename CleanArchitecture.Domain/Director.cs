namespace CleanArchitecture.Domain
{
    public class Director
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int VideoId { get; set; }    
        public virtual Video Video { get; set; }
    }
}
