using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInsertedEventArgs: EventArgs{
	public int SlotIndex { get; set; }
	public ItemInstance NewItem { get; set; }
	public ItemInstance OldItem { get; set; }
}

public class ItemRemovedEventArgs: EventArgs{
	public int SlotIndex { get; set; }
	public ItemInstance RemovedItem { get; set; }
}

public interface IInventory{
	void OnItemInserted(object sender, ItemInsertedEventArgs args);
	void OnItemRemoved(object sender, ItemRemovedEventArgs args);
}