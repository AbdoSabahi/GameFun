using GameFun.Data;

namespace GameFun.Services
{
    public interface ICategoriesServies
    {
       
        IEnumerable<SelectListItem> GetSelectList();
    }
}
