using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemUseable{
	void Use();
}

public interface IItemInstanceStackable {
	int Amount { get; set; }
}
public interface IItemStackable{
	int StackMax { get; }
}

public interface IItemStats{
	Stats Stats { get; }
}
public interface IItemBase{
	string Name { get; }
	ItemType Type { get; }
	string Description { get; }
}
