using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/ItemData")]
public class ItemData : ScriptableObject
{
    public static ItemData[] all { get; private set; }
    
    [field: SerializeField] public GameObject itemModel { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeItems()
    {
        all = Resources.LoadAll<ItemData>("Items");
    }
}