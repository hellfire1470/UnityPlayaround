using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Manager<Player>.Add ("Player", new Player ());
		ContentParser.InsertReplace ("%class%", "warrior");
		ContentParser.InsertReplace ("%Class%", "Warrior");
		ItemManager.AddItem(new Item (0, new ItemDataStack ("test", ItemType.A, new Stats (), ContentParser.ReplaceFromList("hello, %class%"), Resources.LoadAll<Sprite> ("Images/Items/items-3")[0], 100)));
		ItemManager.AddItem(new Item (1, new ItemDataVisual ("test2", ItemType.A, new Stats (), "description2", Resources.LoadAll<Sprite> ("Images/Items/items-3")[1])));

	}
	
	// Update is called once per frame
	void Update () {
	}

}
