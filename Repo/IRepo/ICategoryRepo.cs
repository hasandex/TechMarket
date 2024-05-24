using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechMarket.Repo.IRepo
{
    public interface ICategoryRepo
    {
        IEnumerable<SelectListItem> GetCategoriesSelectList();
    }
}
