using System;
using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	public List<Item> items = new List<Item>();

	/*id
		 * name
		 * desc
		 * health
		 * power
		 * speed
		 * armour
		 * type
		 */
	void Start() {
		items.Add(new Item(0, "Hat", "A very comfortable woolen hat. I always enjoyed the color green!", 2.0F, 0.0F, 0.0F, 3, Item.ItemType.Helmet, "none", 0));
		items.Add(new Item(1, "Hand Gun", "Make sure the safety is on!", 0.0F, 25.0F, 3.0F, 0, Item.ItemType.Weapon, "shoot", 12));
		items.Add(new Item(2, "Rock", "Don't you go throwing this at people now!", 1.0F, 5.0F, 1.0F, 0, Item.ItemType.Weapon, "throw", 1));
		items.Add(new Item(3, "Flashlight", "Does not help you against monsters.", 0.0F, 0.0F, 0.0F, 0, Item.ItemType.Offhand, "use", 0));
		items.Add(new Item(4, "Ammo Clip", "Just in case you don't want the safety on.", 0.0F, 0.0F, 0.0F, 0, Item.ItemType.Offhand, "use", 12));
	}

	public Item FindItem(int itemID) {
		return(items.Find(x => x.itemID == itemID));
	}
}

