using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace E_ProjectSem3.Controllers
{
    [Authorize(Roles = "admin, chef")]
    public class ChefController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Chef
        public ActionResult AddRecipes()
        {
            ViewBag.Categories = db.Categories.Where(c => c.DeletedAt == null).OrderBy(c => c.Name).ToList();
            return View();
        }
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
            newRecipe.ViewCount = 0;

            var id = User.Identity.GetUserId();
            ApplicationUser appUser = new ApplicationUser();
            appUser = db.Users.Find(id);
            newRecipe.ApplicationUser = appUser;
            newRecipe.Status = (int)Recipe.RecipeStatus.NonActive;
            newRecipe.CreatedAt = DateTime.Now;

            List<Ingredient> ingredients = new List<Ingredient>();
            if (listIngredient != null)
            {
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
            }

            List<Step> steps = new List<Step>();
            if (listStep != null)
            {
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
            }

            List<Nutrition> nutritions = new List<Nutrition>();
            if (listNutrition != null)
            {
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
            return RedirectToAction("AddRecipes");
        }

        public ActionResult UpdateRecipe(int? id)
        {
            var recipe = db.Recipes.Find(id);
            var currentUserId = User.Identity.GetUserId();
            if (recipe == null || recipe.DeletedAt != null || currentUserId == null || recipe.ApplicationUser.Id != currentUserId || recipe.Status != (int)Recipe.RecipeStatus.Active)
            {
                return RedirectToAction("NotFound", "Home");
            }
            ViewBag.Categories = db.Categories.Where(c => c.DeletedAt == null).OrderBy(c => c.Name).ToList();
            return View(recipe);
        }
        [ValidateInput(false)]
        public ActionResult OnSubmitUpdateRecipe(int recipeId, string title, string recipeType, string content, string featuredImage,
            string categoryId, string difficulty, string description, int preparationMinute, int cookingMinute,
            int cookingTemp, List<Ingredient> listIngredient, List<Step> listStep, List<Nutrition> listNutrition)
        {
            var recipe = db.Recipes.Find(recipeId);
            var currentUserId = User.Identity.GetUserId();
            if (recipe == null || recipe.DeletedAt != null || currentUserId == null || recipe.ApplicationUser.Id != currentUserId || recipe.Status != (int)Recipe.RecipeStatus.Active)
            {
                return RedirectToAction("NotFound", "Home");
            }

            recipe.Title = title;
            recipe.Type = Convert.ToInt16(recipeType);
            recipe.Content = content;
            recipe.FeaturedImage = featuredImage;
            recipe.Category = db.Categories.Find(Convert.ToInt16(categoryId));
            recipe.Difficulty = difficulty;
            recipe.Description = description;
            recipe.PreparationMinute = preparationMinute;
            recipe.CookingMinute = cookingMinute;
            recipe.CookingTemp = cookingTemp;
            recipe.UpdatedAt = DateTime.Now;
            recipe.Status = (int)Recipe.RecipeStatus.NonActive;

            db.Ingredients.RemoveRange(recipe.Ingredients);
            List<Ingredient> ingredients = new List<Ingredient>();
            if (listIngredient != null)
            {
                foreach (var item in listIngredient)
                {
                    if (item.Name != null && item.Amount != null)
                    {
                        var ct = new Ingredient();
                        ct.Name = item.Name;
                        ct.Amount = item.Amount;
                        ct.CreatedAt = DateTime.Now;
                        ct.RecipeId = recipe.Id;
                        ingredients.Add(ct);
                    }
                }
            }

            db.Steps.RemoveRange(recipe.Steps);
            List<Step> steps = new List<Step>();
            if (listStep != null)
            {
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
                        step.RecipeId = recipe.Id;
                        steps.Add(step);
                    }
                }
            }

            db.Nutritions.RemoveRange(recipe.Nutrition);
            List<Nutrition> nutritions = new List<Nutrition>();
            if (listNutrition != null)
            {
                foreach (var item in listNutrition)
                {
                    if (item.Name != null && item.Value != null)
                    {
                        var nutri = new Nutrition();
                        nutri.Name = item.Name;
                        nutri.Value = item.Value;
                        nutri.CreatedAt = DateTime.Now;
                        nutri.RecipeId = recipe.Id;
                        nutritions.Add(nutri);
                    }
                }
            }
            

            if (ingredients.Count > 0)
            {
                recipe.Ingredients = ingredients;
            }
            if (steps.Count > 0)
            {
                recipe.Steps = steps;
            }
            if (nutritions.Count > 0)
            {
                recipe.Nutrition = nutritions;
            }
            db.Recipes.AddOrUpdate(recipe);
            try
            {
                db.SaveChanges();
                TempData["SaveSuccess"] = "Success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("AddRecipes");
        }
    }
}