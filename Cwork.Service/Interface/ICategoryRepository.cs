using System.Collections.Generic;
using Cwork.Domain.Models.Input;

namespace Cwork.Service.Interface
{
    public interface ICategoryRepository
    {
        int CreateCategory(CategoryModel model);
        int DeleteCategory(int id);
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategoryByWeight(decimal weight);
        CategoryModel GetRecentCategory();
        string UpdateCategory(int id, CategoryModel category);
        CategoryModel GetCategoryById(int id);
    }
}