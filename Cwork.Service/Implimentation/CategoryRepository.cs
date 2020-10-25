using Cwork.Domain.Models.Input;
using Cwork.Persistance;
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
            if (categories == null)
            {
                throw new Exception("Not Found");
            }
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
        public string UpdateCategory(int id, CategoryModel category)
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

                var nextCategory = _data.Categories.Where(x => x.MinWeight == (actualMaxWeight + 1)).FirstOrDefault();
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
                    nextCategory.MinWeight += differenceInMaxWeight;
                    _data.Categories.Update(categoryToUpdate);
                    _data.SaveChanges();
                    return ("when row 1 is updated and we have more rows in db, and max weight is updated");
                }
            }
            else
            {
                return ("sample");
                // for 2nd row, which is the last row..
                //var allCategories = _data.Categories.ToList();
                //for (var i = categoryToUpdate.MinWeight ; i >= previousCategory.MinWeight; i++)
                //{
                //    Console.WriteLine(i);

                //    return ("${i}");
                //}

                //// for 2nd row, which has 3rd row as well.
                //previousCategory.MaxWeight += differencePreMax ;
                //var nextCategory = _data.Categories.Where(x => x.MaxWeight == (actualMaxWeight + 1)).FirstOrDefault();
                //if (nextCategory == null)
                //{
                //    _data.Categories.Update(categoryToUpdate);
                //    _data.SaveChanges();
                //    return ("Looks like this is last row of the db, and max weight is updated");
                //}
                //else
                //{
                //    nextCategory.MinWeight += differenceNextMin;
                //    _data.Categories.Update(categoryToUpdate);
                //    _data.SaveChanges();
                //    return ("when row 1 is updated and we have more rows in db, and max weight is updated");
                //}
                // 0 50
                // if nextMin >= nextMax{
                // foreach var in categories
                // 
                // 51 100----change to 70 150
                //currentvalue - category.min
                //
                // nextMin = currentMax(which is stored, + difference => )
                // 101 200---- 150 -- 200

                // if previous entry is not null
                //    categoryToUpdate.MinWeight = category.MinWeight;
                //categoryToUpdate.MaxWeight = category.MaxWeight;
                // prev ka max weight 
                // next ka min weight update

            }



            //categoryToUpdate.MaxWeight = category.MaxWeight;
            //var weightDifference = (category.MaxWeight) - actualMaxWeight;
            //var minWeightOfUpdatedRow = categoryToUpdate.MinWeight;
            //var maxWeightOfUpdatedRow = categoryToUpdate.MaxWeight;

            //categoryToUpdate.MinWeight = category.MinWeight;
            //categoryToUpdate.MaxWeight = category.MaxWeight;

            //var differenceForMinWeight = minWeightOfUpdatedRow - category.MinWeight;
            //var differenceForMaxWeight = maxWeightOfUpdatedRow - category.MaxWeight;



            // Only 1 category in DB
            //if (categoryToUpdate.MinWeight == 0 && nextCategory == null)
            //{
            //    _data.Categories.Update(categoryToUpdate);
            //    _data.SaveChanges();
            //    return 1;
            //}
            //else
            //{

            //}
            //{
            //    Console.WriteLine("this is last row");
            //    _data.Categories.Update(categoryToUpdate);
            //    _data.SaveChanges();
            //    return 1;
            //}
            //else
            //{

            //    nextCategory.MinWeight = (nextCategory.MinWeight + (weightDifference));
            //    _data.Categories.UpdateRange(categoryToUpdate, nextCategory);
            //    _data.SaveChanges();
            //    Console.WriteLine("Max value for row 1 updated");
            //    return 1;
            //}

            //else
            //{
            // min and max both changes
            // in all cases send min and max value both to the api
            //var minWeightOfUpdatedRow = categoryToUpdate.MinWeight;
            //    var maxWeightOfUpdatedRow = categoryToUpdate.MaxWeight;

            //    categoryToUpdate.MinWeight = category.MinWeight;
            //    categoryToUpdate.MaxWeight = category.MaxWeight;

            //    var differenceForMinWeight = minWeightOfUpdatedRow - category.MinWeight;
            //    var differenceForMaxWeight = maxWeightOfUpdatedRow - category.MaxWeight;

            //var previousCategory = _data.Categories.Where(e => e.MaxWeight == (minWeightOfUpdatedRow - 1)).FirstOrDefault();
            //previousCategory.MaxWeight = previousCategory.MaxWeight + differenceForMinWeight;

            //var nextCategory = _data.Categories.Where(e => e.MinWeight == (maxWeightOfUpdatedRow + 1)).FirstOrDefault();
            //nextCategory.MinWeight = nextCategory.MinWeight + differenceForMaxWeight;

            //_data.Categories.UpdateRange(categoryToUpdate, previousCategory, nextCategory);
            //Console.WriteLine("Category Updated");
            //_data.SaveChanges();


            // now get next row
            // var nextCategory = _data.Categories.Where(e => e.MinWeight == (maxWeightOfDeletedRow + 1)).FirstOrDefault();
            // if (nextCategory == null)
            // {
            //     Console.WriteLine("Last Row Deleted", nextCategory);
            //     _data.Remove(category);
            // }
            // else
            // {
            //     Console.WriteLine("THis should not come");
            //     nextCategory.MinWeight = minWeightOfDeletedRow;
            //     Console.WriteLine("Category deleted", nextCategory);
            //     _data.Categories.Update(nextCategory);
            // }
            // }
        }

    }

}