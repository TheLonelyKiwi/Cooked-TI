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
        
        foreach (Recipe recipe in recipes) {
            bool valid = true;
            foreach ((ItemData item, int amount) in formattedItems) {
                Recipe.RecipeItem recipeItem = recipe.inputs.FirstOrDefault(it => it.item == item);
                if (recipeItem.item == null) {
                    valid = false;
                    break;
                }

                if (amount > recipeItem.amount) {
                    valid = false;
                    break;
                }
            }

            if (!valid) continue;
            
            return true;
        }
        
        return false;
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

        if (formattedItems.Count == 0) {
            outputRecipe = null;
            return false;
        }
        
        foreach (Recipe recipe in recipes) {
            bool valid = true;
            foreach (Recipe.RecipeItem recipeInput in recipe.inputs) {
                if (!formattedItems.TryGetValue(recipeInput.item, out int providedCount)) {
                    valid = false;
                    break;
                }
                if (providedCount != recipeInput.amount) {
                    valid = false;
                    break;
                }
            }

            if (!valid) continue;
            
            outputRecipe = recipe;
            return true;
        }
        
        outputRecipe = null;
        return false;
    }
}