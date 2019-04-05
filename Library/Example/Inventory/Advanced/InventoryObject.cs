using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType {
	A, B, C
}


public class Stats{

}


public class InventoryObject : MonoBehaviour, IInventory {

	[SerializeField] GameObject _containerBox;
	[SerializeField] GameObject _slotPrefab;
	[SerializeField] string _inventoryKey;
	[SerializeField] PlayerObject _player;
	[SerializeField] int _inventorySlot;

	private InventoryExtention _inventory;

	private GameObject[] _slots;
	private GameObject[] _slotsIcon;

	public void Start(){
		ReloadInventory ();

		gameObject.transform.parent.gameObject.SetActive (false);
	}

	public void ReloadInventory(){
		_inventory = _player.Player.Inventories [_inventorySlot];
		int size = _player.Player.Inventories [_inventorySlot].Size;
		_slots = new GameObject[size];
		_slotsIcon = new GameObject[size];

		_ClearSlots ();
		_FillSlots (size);
	}


	private void _ClearSlots(){
		_inventory.ItemInserted -= OnItemInserted;
		_inventory.ItemRemoved -= OnItemRemoved;
		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			if (t != transform && t.gameObject.name != "ContentBox") {
				Destroy (t.gameObject);
			}
		}
	}

	private void _FillSlots(int size){
		_inventory.ItemInserted += OnItemInserted;
		_inventory.ItemRemoved += OnItemRemoved;
		for (int i = 0; i < size; i++) {
			GameObject slot = GameObject.Instantiate(_slotPrefab);
			slot.transform.SetParent(_containerBox.transform);
			slot.name = "Slot "+ i;

			int inventorySlotIndex = i;

			ItemButton button = slot.AddComponent<ItemButton>();
			button.Inventory = _inventory;
			button.ItemIndex = inventorySlotIndex;

			_slots [inventorySlotIndex] = slot;
			_slotsIcon [inventorySlotIndex] = slot.GetComponentsInChildren<Image> ()[1].gameObject;
			if (!_inventory.IsEmpty (inventorySlotIndex)) {
				_SetItemIcon (inventorySlotIndex, _inventory.Items [inventorySlotIndex].RawItem);
				_SetItemText (inventorySlotIndex, _inventory.Items [inventorySlotIndex]);
			}
		}
	}

	private void RemoveItemUI(int slotIndex){
		_inventory.RemoveItem(slotIndex);
		if (_inventory.IsEmpty ()) {
			Debug.Log ("Inventory is Empty");
		}
	}

	private void _SetItemIcon(int slotIndex, Item item){
		if (item.Data is I2DImage) {
			_slotsIcon [slotIndex].SetActive (true);
			_slots [slotIndex].GetComponentsInChildren<Image> () [1].sprite = (item.Data as I2DImage).Sprite;
		}
	}

	private void _SetItemText(int slotIndex, ItemInstance item){
		if (item.RawItem.Data is IItemStackable) {
			_slots [slotIndex].GetComponentInChildren<Text> ().text = (item.InstanceData as ItemInstanceData).Amount.ToString ();
		} else {
			_slots [slotIndex].GetComponentInChildren<Text> ().text = "";
		}
	}

	private void _ClearItemIcon(int slotIndex){

		_slotsIcon [slotIndex].SetActive (false);
		_slots [slotIndex].GetComponentInChildren<Text> ().text = "";
	}

	#region IInventory implementation

	public void OnItemInserted (object sender, ItemInsertedEventArgs args)
	{
		if (args.OldItem == null) {
//			Debug.Log (((args.NewItem.RawItem.Data is ItemDataStack) ? "Ist Stackable " : "" ) + (args.NewItem.RawItem.Data as ItemDataVisual).Name);
		} else {
			Debug.Log ("An item in your inventory was replaced");
		}
		_SetItemIcon (args.SlotIndex, args.NewItem.RawItem);
		_SetItemText (args.SlotIndex, args.NewItem);

	}

	public void OnItemRemoved (object sender, ItemRemovedEventArgs args)
	{
		if (args.RemovedItem != null) {
			Debug.Log("'" + (args.RemovedItem.RawItem.Data as IItemBase).Name + "' was removed from your inventory");
			_ClearItemIcon (args.SlotIndex);
		}

	}

	#endregion


	public void AddItem(GameObject inputArea)
	{
		int itemId = int.Parse( inputArea.GetComponentsInChildren<InputField> ()[0].text );
		int amount = int.Parse( inputArea.GetComponentsInChildren<InputField> ()[1].text );

		if (amount > 0) {
			ItemInstance item = new ItemInstance (itemId, new ItemInstanceData(amount));
			ItemInstance remaining = _inventory.PutItem (item);
			if (remaining != null) {
				Debug.Log ("Your Inventory is Full, failed to store " + (remaining.InstanceData as ItemInstanceData).Amount + "x '" + (remaining.RawItem.Data as IItemBase).Name + "'");
			}
		}
	}
}


