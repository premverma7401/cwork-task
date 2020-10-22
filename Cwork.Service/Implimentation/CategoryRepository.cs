using Cwork.Domain.Models.Input;
using Cwork.Persistance;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cwork.Service.Implimentation
{
    public class CategoryRepository
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
                MaxWeight = model.MaxWeight
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
            return categories;
        }

        public int DeleteCategory(int id)
        {
            var category = _data.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category.MinWeight == 0)
            {
                Console.WriteLine("1st row in DB, You cannot delete it, rather try updating the max weight.");
                return 1;
            }
            // else if (category.MinWeight != 0)
            // {
            //     var lastCategory = _data.Categories.OrderByDescending(x => x.MaxWeight).FirstOrDefault();
            //     _data.Remove(lastCategory);
            // }
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




    }

}