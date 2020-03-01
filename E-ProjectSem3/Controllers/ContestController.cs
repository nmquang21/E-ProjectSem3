using E_ProjectSem3.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_ProjectSem3.Controllers
{
    public class ContestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Contest
        public ActionResult Index()
        {
            return View("~/Views/Contest/AddRecipesContest.cshtml");
        }
        public ActionResult SubmitRecipeContest(string title,string content, string featuredImage,
        string difficulty, string description, int preparationMinute, int cookingMinute,
            int cookingTemp, List<Ingredient> listIngredient, List<Step> listStep, List<Nutrition> listNutrition)
        {
            var newRecipe = new Recipe();
            newRecipe.Title = title;
            newRecipe.Type = Convert.ToInt16(Recipe.RecipeType.Iscompetition);
            newRecipe.Content = content;
            newRecipe.FeaturedImage = featuredImage;
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
    }
}