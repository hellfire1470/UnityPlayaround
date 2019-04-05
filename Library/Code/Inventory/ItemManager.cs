using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager{
	private static Dictionary<int, Item> _items = new Dictionary<int, Item> ();

	public static Dictionary<int, Item> Items{
		get{
			return _items;
		}
	}

	public static void AddItem(Item item){
		_items.Add(item.Id, item);
	}
}