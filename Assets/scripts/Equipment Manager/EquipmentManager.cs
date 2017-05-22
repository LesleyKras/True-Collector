using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipmentManager : MonoBehaviour {
	public ItemDatabase database;
	public Inventory inven;

	public List<Item> equipment = new List<Item>();
	public List<Item> slots = new List<Item>();

	private string tooltip;
	private bool showCharacterStats;
	private bool showTooltip;

	private float max_health;
	private float health; //A new variable to save the conversation string in
	private float speed;
	private float power;
	private int armour;
	private int ammo;

	public int slotsX, slotsY;

	//Finding Player declarations
	GameObject playerObj; //Find the object inside unity
	Player player; //Declare the class
    //Gun gun;

	//GUI
	public GUISkin skin;

	void Start() {
		playerObj = GameObject.Find("Player");
		player = playerObj.GetComponent<Player>();

		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		inven = GameObject.Find ("Inventory").GetComponent<Inventory>();
        //gun = GameObject.Find("Weapon").GetComponent<Gun>();
        

		//Create empty slots
		for(int i = 0; i < (slotsX * slotsY); i++) {
			slots.Add (new Item ());	//Create empty slots
			equipment.Add (new Item ());	//Add these empty slots to equipment
		}
	}

	void Update() {
		if(Input.GetButtonDown("Equipment")) {
			showCharacterStats = !showCharacterStats;

			if(showCharacterStats) {
				Camera.main.GetComponent<camMouseLook>().StopCamera();
			} else {
				Camera.main.GetComponent<camMouseLook>().StartCamera();
			}
		}

		max_health = player.max_Health;
		health = player.Health;
		speed = player.Speed;
		power = player.Power;
		armour = player.Armour;
		ammo = GetItemInSlot(3).itemAmmo;
	}

	void OnGUI() {
		tooltip = "";

		if(showCharacterStats) {
			CharacterScreen();

			if(showTooltip) {
				GUI.Box(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y + 15, 200, 200), tooltip, skin.FindStyle("Tooltip"));
			}
		}
	}

	private Vector2[] itemSlotDimensions = new Vector2[5]{ new Vector2(105,0), new Vector2(105, 50), new Vector2(105, 100), new Vector2(160, 50), new Vector2(50, 50) };
	void CharacterScreen() {
        string charStats = "Health: " + health + "/" + max_health + "\n" + "Armour: " + armour + "\n" + "Attack Speed: " + speed + "\n" + "Power: " + power + "\n\n" + "Ammo: " + ammo; ;

		// Full Equipment Manager
		GUI.Box(new Rect(Screen.width - 500, 5, 500, 300), "", skin.FindStyle("Equipment"));

		// Stats
		GUI.Box(new Rect(Screen.width - 165, 125, 152, 187), charStats, skin.FindStyle("None"));

		int i = 0;
		for(int j = 0; j < 5; j++) {
			int x = (int) itemSlotDimensions[i].x;
			int y = (int) itemSlotDimensions[i].y;
			slots[i] = equipment[i];

			Rect itemSlot = new Rect(Screen.width - (250 + x), (125 + y), 52, 47);
			GUI.Box(itemSlot, "", skin.FindStyle("slot"));

			Event e = Event.current;

			if(slots[i].itemName != null) {
				GUI.DrawTexture(itemSlot, slots[i].itemIcon);
			}

			if(itemSlot.Contains(e.mousePosition)) {
				if(slots[i].itemName != null) {
					tooltip = CreateTooltip(slots[i]);
					showTooltip = true;
				}

				if(e.isMouse && e.type == EventType.mouseDown && e.button == 1) {
					inven.AddItem(slots[i].itemID);
					equipment[i] = new Item();
					player.NotWearingEquipment (database.FindItem (slots[i].itemID).itemHealth, database.FindItem (slots[i].itemID).itemArmour, database.FindItem (slots[i].itemID).itemPower, database.FindItem (slots[i].itemID).itemSpeed);
				}
			}

			if(tooltip == "") {
				showTooltip = false;
			}
			i++;
		}
	}

	public void RemoveItem(int id) {
		for(int i = 0; i < equipment.Count; i++) {
			if(equipment[i].itemID == id) {
				equipment[i] = new Item();
				player.NotWearingEquipment (database.FindItem (slots[i].itemID).itemHealth, database.FindItem (slots[i].itemID).itemArmour, database.FindItem (slots[i].itemID).itemPower, database.FindItem (slots[i].itemID).itemSpeed);
			}
		}
	}

	public void AddItemInSlot(int id, int slotID) {
		equipment[slotID] = database.FindItem(id);
		player.WearingEquipment(database.FindItem(id).itemHealth, database.FindItem(id).itemArmour, database.FindItem(id).itemPower, database.FindItem(id).itemSpeed);
	}

	public void RemoveItemInSlot(int slotID) {
		equipment[slotID] = new Item();
	}

	public bool IsSlotOccupied(int itemTypeIndex) {
		player.Health -= equipment [itemTypeIndex].itemHealth;
		player.max_Health -= equipment[itemTypeIndex].itemHealth;
		player.Armour -= equipment [itemTypeIndex].itemArmour;
		player.Power -= equipment [itemTypeIndex].itemPower;
		player.Speed -= equipment [itemTypeIndex].itemSpeed;

		return(equipment[itemTypeIndex].itemID != -1);
	}

	public Item GetItemInSlot(int slotID) {
		return equipment[slotID];
	}

	string CreateTooltip(Item item) {
		tooltip = item.itemName + "\n" + item.itemType + "\n\n" + item.itemDesc;

		if (item.itemHealth > 0f) {
			tooltip = tooltip + " \n\n Health: " + item.itemHealth;
		}
		if (item.itemArmour > 0) {
			tooltip = tooltip + " \n\n Armour: " + item.itemArmour;
		}
		if (item.itemSpeed > 0) {
			tooltip = tooltip + " \n\n Speed: " + item.itemSpeed;
		}
		if (item.itemPower > 0) {
			tooltip = tooltip + " \n\n Power: " + item.itemPower;
		}

		return tooltip;
	}

	public bool getShowCharacterStats() {
		return showCharacterStats;
	}

	public int GetAmmo() {
		return ammo;
	}

    public float GetWeaponSpeed() {
        return GetItemInSlot(3).itemSpeed;
    }
}