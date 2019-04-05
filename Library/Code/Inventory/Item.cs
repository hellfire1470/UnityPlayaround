using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item{
	
	private ItemData _data;
	private int _id;

	public int Id{
		get {
			return _id;
		}
	}

	public ItemData Data {
		get {
			return _data;
		}
	}

	public Item (int id, ItemData data){
		_id = id;
		_data = data;
	}
}