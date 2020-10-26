using Cwork.Domain.Models.Input;
using Cwork.Persistance;
using Cwork.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Cwork.Domain.Models.Output;

namespace Cwork.Service.Implimentation
{


    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _data;

        public CategoryRepository(DataContext data)
        {
            _data = data;
        }
        public int CreateCategory(CategoryModel model)
        {

            var category = new CategoryModel
            {
                CategoryName = model.CategoryName,
                MinWeight = model.MinWeight,
                MaxWeight = model.MaxWeight,
                Icon = model.Icon
            };
            _data.Categories.Add(category);
            return _data.SaveChanges();

        }

        public List<CategoryModel> GetAllCategories()
        {
            var categories = _data.Categories.ToList();
            return categories;
        }

        public CategoryModel GetRecentCategory()
        {
            var categories = _data.Categories.OrderByDescending(x => x.MaxWeight).First();
            if (categories == null)
            {
                throw new Exception("Not Found");
            }
            return categories;
        }

        public CategoryModel GetCategoryById(int id)
        {
            var category = _data.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            return category;
        }

        public int DeleteCategory(int id)
        {
            var category = _data.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category.MinWeight == 0)
            {
                Console.WriteLine("1st row in DB, You cannot delete it, rather try updating the max weight.");
                return 0;
            }

            else
            {
                var minWeightOfDeletedRow = category.MinWeight;
                var maxWeightOfDeletedRow = category.MaxWeight;
                _data.Remove(category);
                Console.WriteLine("Category deleted");
                // now get next row
                var nextCategory = _data.Categories.Where(e => e.MinWeight == (maxWeightOfDeletedRow + 1)).FirstOrDefault();
                if (nextCategory == null)
                {
                    Console.WriteLine("Last Row Deleted", nextCategory);
                    _data.Remove(category);
                }
                else
                {
                    Console.WriteLine("THis should not come");
                    nextCategory.MinWeight = minWeightOfDeletedRow;
                    Console.WriteLine("Category deleted", nextCategory);
                    _data.Categories.Update(nextCategory);
                }
            }
            _data.SaveChanges();
            return 1;

        }

        public CategoryModel GetCategoryByWeight(decimal weight)
        {
            var category = _data.Categories.Where(x => x.MaxWeight >= weight).FirstOrDefault();
            if (category == null)
            {
                throw new Exception("Not Found");
            }
            return category;
        }
        public string UpdateCategory(int id, CategoryUpdateDTO category)
        {
            var categoryToUpdate = _data.Categories.Where(x => x.CategoryId == id).FirstOrDefault();

            var actualMinWeight = categoryToUpdate.MinWeight;
            var actualMaxWeight = categoryToUpdate.MaxWeight;
            var differencePreMax = (actualMinWeight - category.MinWeight);
            var differenceNextMin = (category.MaxWeight - actualMaxWeight);
            // previous value
            var previousCategory = _data.Categories.Where(e => e.MaxWeight == (actualMinWeight - 1)).FirstOrDefault();
            if (previousCategory == null)
            // First category in the list
            {

                var nextCategory = _data.Categories.Where(x => x.MinWeight == (actualMaxWeight )).FirstOrDefault();
                if (nextCategory == null)
                // last category in the list
                {
                    categoryToUpdate.MaxWeight = category.MaxWeight;
                    Console.WriteLine(categoryToUpdate);
                    _data.Categories.Update(categoryToUpdate);
                    _data.SaveChanges();
                    return ("when only 1 entry in db, and max weight is updated");
                }
                else
                {
                    categoryToUpdate.MaxWeight = category.MaxWeight;
                    //nextCategory.MinWeight = category.MaxWeight;
                    var differenceInMaxWeight = (category.MaxWeight - actualMaxWeight);
                    nextCategory.MinWeight += (differenceInMaxWeight +1);
                    _data.Categories.Update(categoryToUpdate);
                    _data.SaveChanges();
                    return ("when row 1 is updated and we have more rows in db, and max weight is updated");
                }
            }
            else
            {
                if (categoryToUpdate.MinWeight > category.MinWeight || categoryToUpdate.MaxWeight < category.MinWeight)
                {
                    var difference = (categoryToUpdate.MinWeight - category.MinWeight);
                    var getAllPrev = _data.Categories.Where(x => x.MinWeight > category.MinWeight).ToList();
                    foreach (var item in getAllPrev)
                    {
                        if (item.MinWeight > category.MinWeight)
                        {
                            
                            item.MinWeight = category.MinWeight;
                            var prevCat = _data.Categories.Where(x => x.MaxWeight == (item.MinWeight - difference)).FirstOrDefault();
                            prevCat.MaxWeight = (item.MinWeight - 1);
                        }
                    }
                }
                return "Not Completed";
            }
        }

    }
}