using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace E_ProjectSem3.Controllers
{
    public class ChefController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Chef
        public ActionResult SubmitRecipe(string title, string recipeType, string content, string featuredImage,
            string categoryId, string difficulty, string description, int preparationMinute, int cookingMinute,
            int cookingTemp, List<Ingredient> listIngredient, List<Step> listStep, List<Nutrition> listNutrition)
        {
            var newRecipe = new Recipe();
            newRecipe.Title = title;
            newRecipe.Type = Convert.ToInt16(recipeType);
            newRecipe.Content = content;
            newRecipe.FeaturedImage = featuredImage;
            newRecipe.Category = db.Categories.Find(Convert.ToInt16(categoryId));
            newRecipe.Difficulty = difficulty;
            newRecipe.Description = description;
            newRecipe.PreparationMinute = preparationMinute;
            newRecipe.CookingMinute = cookingMinute;
            newRecipe.CookingTemp = cookingTemp;


            var id = User.Identity.GetUserId();
            ApplicationUser appUser = new ApplicationUser();
            appUser = db.Users.Find(id);
            newRecipe.ApplicationUser = appUser;
            newRecipe.Status = (int)Recipe.RecipeStatus.NonActive;
            newRecipe.CreatedAt = DateTime.Now;

            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (var item in listIngredient)
            {
                if (item.Name != null && item.Amount != null)
                {
                    var ct = new Ingredient();
                    ct.Name = item.Name;
                    ct.Amount = item.Amount;
                    ct.CreatedAt = DateTime.Now;
                    ct.RecipeId = newRecipe.Id;
                    ingredients.Add(ct);
                }
            }

            List<Step> steps = new List<Step>();
            foreach (var item in listStep)
            {
                if (item.Title != null && item.Index != null && item.ImagePath != null & item.Description != null)
                {
                    var step = new Step();
                    step.Index = item.Index;
                    step.Title = item.Title;
                    step.ImagePath = item.ImagePath;
                    step.Description = item.Description;
                    step.CreatedAt = DateTime.Now;
                    step.RecipeId = newRecipe.Id;
                    steps.Add(step);
                }
            }

            List<Nutrition> nutritions = new List<Nutrition>();
            foreach (var item in listNutrition)
            {
                if (item.Name != null && item.Value != null)
                {
                    var nutri = new Nutrition();
                    nutri.Name = item.Name;
                    nutri.Value = item.Value;
                    nutri.CreatedAt = DateTime.Now;
                    nutri.RecipeId = newRecipe.Id;
                    nutritions.Add(nutri);
                }
            }

            if (ingredients.Count > 0)
            {
                newRecipe.Ingredients = ingredients;
            }
            if (steps.Count > 0)
            {
                newRecipe.Steps = steps;
            }
            if (nutritions.Count > 0)
            {
                newRecipe.Nutrition = nutritions;
            }
            db.Recipes.Add(newRecipe);
            try
            {
                db.SaveChanges();
                TempData["AddSuccess"] = "Success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Recipes","Home");
        }
    }
}