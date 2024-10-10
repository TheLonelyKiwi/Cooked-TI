using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform _visualParent;
    
    public ItemData itemData { get; private set;}
    public Inventory inventory;

    public static Item prefab => Resources.Load<Item>("Item");

    public static Item CreateItem(ItemData itemData)
    {
        Item instance = Instantiate(prefab);
        instance.SetItemData(itemData);
        return instance;
    }

    private void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
        Instantiate(itemData.itemModel, _visualParent);
    }
}