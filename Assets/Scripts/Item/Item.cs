using System.Collections;
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

    public Coroutine MoveTowards(Transform transform)
    {
        StopAllCoroutines();
        return StartCoroutine(MoveTowardsRoutine(transform));
    }

    private void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
        Instantiate(itemData.itemModel, _visualParent);
    }

    private IEnumerator MoveTowardsRoutine(Transform targetTransform)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        
        for (float i = 0; i < 1f; i += Time.deltaTime * 3f) {
            float t = Mathf.Sqrt(i);
            
            transform.position = Vector3.Lerp(startPos, targetTransform.position, t);
            transform.rotation = Quaternion.Slerp(startRot, targetTransform.rotation, t);

            yield return null;
        }
    }
}