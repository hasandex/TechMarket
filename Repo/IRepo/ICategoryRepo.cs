using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechMarket.Repo.IRepo
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<SelectListItem> GetCategoriesSelectList();
    }
}
