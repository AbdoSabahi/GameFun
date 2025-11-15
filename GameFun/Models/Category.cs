
namespace GameFun.Models
{
    public class Category:BaseEntity
    {

        public ICollection<Game> Games { get; set; } = [];
    }
}
