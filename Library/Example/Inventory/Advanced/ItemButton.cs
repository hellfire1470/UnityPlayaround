using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemButton: Button, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{
	private Inventory _inventory;
	private int _itemIndex;

	public Inventory Inventory {
		get {
			return _inventory;
		}
		set{ 
			_inventory = value;
		}
	}

	public int ItemIndex {
		get {
			return _itemIndex;
		}
		set{
			_itemIndex = value;
		}
	}


	#region IPointerEnterHandler implementation

	public override void OnPointerEnter (PointerEventData eventData)
	{
		if (!_inventory.IsEmpty(_itemIndex)) {
			Manager<Tooltip>.Get ("Item").SetContent (new Tooltip.TooltipContent {
				name = (_inventory.Items [_itemIndex].RawItem.Data as ItemDataBase).Name,
				description = (_inventory.Items [_itemIndex].RawItem.Data as ItemDataBase).Description,
			});
			Manager<Tooltip>.Get ("Item").Show (transform.position);
		}
	}

	#endregion

	#region IPointerClickHandler implementation

	public override void OnPointerClick (PointerEventData eventData)
	{
		_inventory.RemoveItem (_itemIndex);
	}

	#endregion

	#region IPointerExitHandler implementation

	public override void OnPointerExit (PointerEventData eventData)
	{
		Manager<Tooltip>.Get("Item").Hide();
	}

	#endregion


}