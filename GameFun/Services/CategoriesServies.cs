
using GameFun.Data;

namespace GameFun.Services
{
    public class CategoriesServies : ICategoriesServies
    {
        private readonly ApplicationDbContext _context;

        public CategoriesServies(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {

            return _context.Categories
                           .Select(c => new SelectListItem
                           {
                               Value = c.Id.ToString(),
                               Text = c.Name
                           }).OrderBy(c => c.Text)
                             .AsNoTracking()
                           .ToList();
        }
    }
}
