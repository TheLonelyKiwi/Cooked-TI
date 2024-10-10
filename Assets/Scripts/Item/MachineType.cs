using System.Collections.Generic;
using System.Linq;
using JUtils;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/MachineType")]
public class MachineType : ScriptableObject
{
    public IEnumerable<Recipe> GetRecipes()
    {
        return Recipe.all.Where(it => it.machineType == this);
    }

    public bool IsValidRecipeWithItem(IEnumerable<ItemData> currentItems, ItemData itemData)
    {
        IEnumerable<ItemData> datas = currentItems.Append(itemData);
        List<Recipe> recipes = GetRecipes().ToList();

        Dictionary<ItemData, int> formattedItems = new ();
        foreach (ItemData item in datas) {
            if (!formattedItems.TryGetValue(item, out int count)) {
                count = 0;
                formattedItems.Add(item, 0);
            }

            formattedItems[item] = count + 1;
        }

        List<Recipe> recipesToRemove = new(recipes.Count);
        foreach ((ItemData key, int value) in formattedItems) {
            foreach (Recipe recipe in recipes) {
                bool found = true;
                foreach (Recipe.RecipeItem recipeInput in recipe.inputs) {
                    if (recipeInput.item == key) continue;
                    if (recipeInput.amount <= value) continue;
                    found = false;
                    break;
                }

                if (!found) {
                    recipesToRemove.Add(recipe);
                }
            }

            foreach (Recipe recipe in recipesToRemove) {
                recipes.Remove(recipe);
            }
            recipesToRemove.Clear();
        }

        return recipes.Count > 0;
    }
    
    public bool TryGetRecipesForItem(IEnumerable<ItemData> itemDatas, out Recipe outputRecipe)
    {
        List<Recipe> recipes = GetRecipes().ToList();

        Dictionary<ItemData, int> formattedItems = new ();
        foreach (ItemData item in itemDatas) {
            if (!formattedItems.TryGetValue(item, out int count)) {
                count = 0;
                formattedItems.Add(item, 0);
            }

            formattedItems[item] = count + 1;
        }

        List<Recipe> recipesToRemove = new(recipes.Count);
        foreach ((ItemData key, int value) in formattedItems) {
            foreach (Recipe recipe in recipes) {
                bool found = true;
                foreach (Recipe.RecipeItem recipeInput in recipe.inputs) {
                    if (recipeInput.item == key) continue;
                    if (recipeInput.amount == value) continue;
                    found = false;
                    break;
                }

                if (!found) {
                    recipesToRemove.Add(recipe);
                }
            }

            foreach (Recipe recipe in recipesToRemove) {
                recipes.Remove(recipe);
            }
            recipesToRemove.Clear();
        }

        if (recipes.Count != 1) {
            outputRecipe = null;
            return false;
        }

        outputRecipe = recipes[0];
        return true;
    }
}