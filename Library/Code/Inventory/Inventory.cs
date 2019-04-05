using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory
{

    private int _size = 64;
    protected Dictionary<int, ItemInstance> _items = new Dictionary<int, ItemInstance>();

    public event EventHandler<ItemInsertedEventArgs> ItemInserted;
    public event EventHandler<ItemRemovedEventArgs> ItemRemoved;

    #region "Constructor"
    public Inventory(int size = 64)
    {
        _size = size;
    }
    public Inventory(int size = 64, IInventory iInventory = null)
    {
        _size = size;
    }
    #endregion

    #region "Getter/Setter"
    public Dictionary<int, ItemInstance> Items
    {
        get
        {
            return _items;
        }
    }
    public int Size
    {
        get
        {
            return _size;
        }
    }

    #endregion

    protected bool _IsSlotInRange(int slot)
    {
        return (slot >= 0 && slot < _size);
    }


    /* Add a new Item to Inventory on given slot
	 * param slot: index of slot in Inventory
	 * param item: Item that will put in given slot
	 * return: Item that was previously on that slot or null if slot was empty.
	 */
    public virtual ItemInstance PutItem(int slotIndex, ItemInstance item)
    {
        if (!_IsSlotInRange(slotIndex))
        {
            throw new UnityException("selected itemslot is not in range.");
        }
        if (item == null)
        {
            throw new UnityException("item cant be null. Use RemoveItem function");
        }

        ItemInstance oldItem = null;
        if (!_items.ContainsKey(slotIndex))
        {
            _items.Add(slotIndex, item);
        }
        else
        {
            oldItem = _items[slotIndex];
            _items[slotIndex] = item;
        }

        OnItemInserted(new ItemInsertedEventArgs()
        {
            SlotIndex = slotIndex,
            NewItem = item,
            OldItem = oldItem
        });


        return oldItem;
    }

    /* Add a new Item to Inventory on the next free slot
	 * param item: Item that will put in given slot
	 * return int: slotIndex of Item. -1 if no empty slot found
	 */
    public virtual int PutItem(ItemInstance item)
    {
        if (item == null)
        {
            throw new UnityException("item cant be null. Use RemoveItem function");
        }

        // Check if Inventory has free space
        if (IsFull())
        {
            throw new UnityException("inventory full");
        }

        int index = GetFreeIndex();
        PutItem(index, item);
        return index;
    }

    public int GetFreeIndex()
    {
        if (IsFull())
        {
            throw new UnityException("no free spaces found.");
        }
        for (int i = 0; i < _size; i++)
        {
            if (!_items.ContainsKey(i))
            {
                //PutItem (i, item);
                return i;
            }
        }
        throw new UnityException("Internal Error on: Inventory.GetFreeIndex()");
    }

    public List<int> GetIndeces(ItemInstance item)
    {
        if (item == null)
        {
            throw new UnityException("item cant be null. Use RemoveItem function");
        }

        List<int> keys = new List<int>();
        foreach (KeyValuePair<int, ItemInstance> pair in _items)
        {
            if (pair.Value.RawItem == item.RawItem)
            {
                keys.Add(pair.Key);
            }
        }

        return keys;
    }

    /* Check if Slot is Empty
	 * param slotIndex: index of slot in Inventory
	 * return bool: true if slot is empty else false
	 */
    public bool IsEmpty(int slotIndex)
    {
        if (!_IsSlotInRange(slotIndex))
        {
            throw new UnityException("selected itemslot is not in range.");
        }
        return (!_items.ContainsKey(slotIndex));
    }

    public bool IsEmpty()
    {
        return _items.Count == 0;
    }

    public bool IsFull()
    {
        return _items.Count == _size;
    }

    /* Removes an Item from Inventory on given slot.
	 * param slot: index of slot in Inventory
	 * return InventoryItem: Item that was Removed or null if slot was empty
	 */
    public ItemInstance RemoveItem(int slotIndex)
    {
        if (!_IsSlotInRange(slotIndex))
        {
            throw new UnityException("selected itemslot is not in range.");
        }
        ItemInstance removedItem = null;
        if (_items.ContainsKey(slotIndex))
        {
            removedItem = _items[slotIndex];
            _items.Remove(slotIndex);
        }

        OnItemRemoved(new ItemRemovedEventArgs()
        {
            SlotIndex = slotIndex,
            RemovedItem = removedItem
        });


        return removedItem;
    }

    /* Swaps 2 item places
	 * param slot1: index of slot for first item.
	 * param slot2: index of slot for seconde item.
	 */
    /*public void Swap(int slotIndex1, int slotIndex2){
		if (_IsSlotInRange (slotIndex1)) {
			throw new UnityException ("selected itemslot (slot1) is not in range");
		}
		if (_IsSlotInRange (slotIndex2)) {
			throw new UnityException ("selected itemslot (slot2) is not in range");
		}

		if (!_items.ContainsKey (slotIndex1)) {
			_items.Add (slotIndex1, null);
		}

		if (!_items.ContainsKey (slotIndex2)) {
			_items.Add (slotIndex2, null);
		}

		ItemInstance s1Item = _items [slotIndex1];
		_items [slotIndex1] = _items [slotIndex2];
		_items [slotIndex2] = s1Item;


		//Interface Call
		if (_iInventory != null) {
			_iInventory.OnItemsSwapped (_items [slotIndex1], _items [slotIndex2]);
		}

	}
*/

    public bool HasItem(Item item)
    {
        foreach (ItemInstance itemInstance in _items.Values)
        {
            if (itemInstance.RawItem == item)
            {
                return true;
            }
        }
        return false;
    }




    protected virtual void OnItemInserted(ItemInsertedEventArgs args)
    {
        ItemInserted?.Invoke(this, args);
    }

    protected virtual void OnItemRemoved(ItemRemovedEventArgs args)
    {
        ItemRemoved?.Invoke(this, args);
    }

}
