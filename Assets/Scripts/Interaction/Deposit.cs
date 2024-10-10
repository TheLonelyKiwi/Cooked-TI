using System.Collections;
using JUtils;
using UnityEngine;

namespace Interaction
{
    public class Deposit : Interactable, IItemDeposit
    {
        [Space]
        [SerializeField, Required] private Inventory _inventory;
        [SerializeField] private Transform _endTransform;
        
        private bool _isProcessingItem;
        
        public override bool CanInteract(Player player)
        {
            return base.CanInteract(player) && CanDepositItem(player.inventory.LastItem()) && !_isProcessingItem;
        }

        public bool CanDepositItem(Item item)
        {
            return _inventory.IsNotFull() && item != null && item.itemData.deliverScore > 0;
        }

        public Coroutine DepositItem(Item item)
        {
            _inventory.TryAddItem(item, out Coroutine coroutine);
            return coroutine;
        }

        public IEnumerator DisposeItem(Item item)
        {
            _isProcessingItem = true;

            Vector3 startPos = item.transform.position;
            Vector3 endPos = _endTransform.position;

            for (float i = 0; i < 1f; i += Time.deltaTime) {
                item.transform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
            
            _inventory.TryRemoveItem(item);
            Destroy(item.gameObject);
            
            _isProcessingItem = false;
        }
        
        protected override void OnInteract(Player player)
        {
            player.stateMachine.GoToState<PlayerPutdownState>(new StateData(this));
        }

        private void Awake()
        {
            _inventory.onItemAdded += item => {
                StartCoroutine(DisposeItem(item));
                logicScript.instance.addScore(item.itemData.deliverScore);
            };
        }
    }
}