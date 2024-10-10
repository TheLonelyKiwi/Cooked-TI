using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/Recipe")]
public class Recipe : ScriptableObject
{
    public static Recipe[] all { get; private set; }
    
    [field: SerializeField] public MachineType machineType { get; private set; }
    [field: SerializeField] public RecipeItem[] inputs { get; private set; }
    [field: SerializeField] public ItemData output { get; private set; }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeItems()
    {
        all = Resources.LoadAll<Recipe>("Recipes");
    }
    
    [Serializable]
    public struct RecipeItem
    {
        public int amount;
        public ItemData item;
    }
}