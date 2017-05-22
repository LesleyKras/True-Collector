using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

[System.Serializable]
public class Item {
	public int itemID;
	public string itemName;
	public string itemDesc;
	public string attackStyle;
	public Texture2D itemIcon;
	public float itemHealth;
	public float itemPower;
	public float itemSpeed;
	public int itemArmour;
	public int itemAmmo;

	public ItemType itemType;

	// List of constants
	public enum ItemType {
		Helmet,
		Armour,
		Boots,
		Weapon,
		Offhand,
		Consumable,
        Misc
	}
		
	public int getItemTypeIndex() {
		return((int)itemType);
	}

	// Returns if the item is a comsumable
	public bool sConsumable() {
		return (itemType == ItemType.Consumable);
	}
		
	/*
	 * Items can actually contain item information
	 * Constructor
	 */ 
	public Item(int id, string name, string desc, float health, float power, float speed, int armour, ItemType type, string style, int ammo) {
		itemID = id;
		itemName = name;
		itemDesc = desc;
		itemHealth = health;
		itemPower = power;
		itemSpeed = speed;
		itemArmour = armour;
		itemType = type;
		attackStyle = style;
		itemAmmo = ammo;
		itemIcon = Resources.Load<Texture2D> ("Item Icons/" + name);
	}

	// So we can have "empty" items
	public Item() {
		itemID = -1;
	}
}
