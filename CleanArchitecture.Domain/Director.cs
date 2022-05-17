using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Director : BaseDomainModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int VideoId { get; set; }    
        public virtual Video Video { get; set; }
    }
}
