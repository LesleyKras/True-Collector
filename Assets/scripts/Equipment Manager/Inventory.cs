using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public List<Item> inventory = new List<Item>(); //will contain 
	public List<Item> slots = new List<Item>(); //Resemble

	public int slotsX, slotsY;

	private int dragIndex;

	private bool showInventory;
	private bool showTooltip;
	private bool draggingItem;

	private string tooltip;

	public GUISkin skin;

	private ItemDatabase database;
	private Item draggedItem;
	private EquipmentManager equip;

	GameObject playerObj;
	Player player;

	// Use this for initialization
	void Start () {
		playerObj = GameObject.Find("Player");
		player = playerObj.GetComponent<Player>();

		for(int i = 0; i < (slotsX * slotsY); i++) {
			slots.Add(new Item());
			inventory.Add(new Item());
		}

		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		equip = GameObject.Find("Character Sheet").GetComponent<EquipmentManager>();

		AddItem(0); // Hat
		AddItem(1); // Hand Gun
		AddItem(2); // Rock
		AddItem(3); // Flashlight
		AddItem(4); // Ammo Clip
	}
	
	// Update is called once per frame
	void Update () {
		// Opens & closes inventory when pressing on i
		if(Input.GetButtonDown("Inventory")) {
			showInventory = !showInventory;

			if(showInventory) {
				Camera.main.GetComponent<camMouseLook>().StopCamera();
			} else {
				Camera.main.GetComponent<camMouseLook>().StartCamera();
			}
		}
	}

	void OnGUI() {
		tooltip = "";
		GUI.skin = skin;

		if(showInventory) {
			DrawInventory();

			if(showTooltip) {
				GUI.Box(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y + 15, 200, 200), tooltip, skin.GetStyle("Tooltip"));
			}

			if(draggingItem) {
				GUI.DrawTexture(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y + 15, 50, 50), draggedItem.itemIcon);
			}
		}
	}

	void DrawInventory()
	{
		Event e = Event.current;
		int i = 0;
		GUI.Box (new Rect (0, 0, 300, 300), "", skin.GetStyle("Inventory")); //ADD STYLE HERE...

		//Create inventory on y position
		for(int y = 0; y < slotsY; y++)
		{
			//Create inventory on x position
			for(int x = 0; x < slotsX; x++)
			{
				Rect slotRect = new Rect ((x * 52) + 20, (y * 47) + 87, 52, 47);
				GUI.Box(new Rect((x * 52) + 20, (y * 47) + 87, 52, 47), "", skin.GetStyle("Slot"));
				slots [i] = inventory [i];

				if (slots [i].itemName != null) 
				{   //If there's a slot with a name
					GUI.DrawTexture (slotRect, slots [i].itemIcon); //Draw the icon from that item on the inventory slot/rectangle

					if (slotRect.Contains (e.mousePosition)) 
					{ //See if the slot position intersects with mouse position
						tooltip = CreateTooltip (slots [i]);
						showTooltip = true;

						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) 
						{ //if left clicked and mouse has been dragged
							draggingItem = true; //Check if player is currently dragging an item
							dragIndex = i; 
							draggedItem = slots [i]; 
							inventory [i] = new Item ();
						}

						if (e.type == EventType.mouseUp && draggingItem) 
						{ //Swap items
							inventory [dragIndex] = inventory [i];
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}

						//RIGHT CLICK TO CONSUME OR EQUIP
						if(e.isMouse && e.type == EventType.mouseDown && e.button == 1) //On right click
						{
//							if( slots[i].IsConsumable())  //Only if the item you're hovering over is of type consumable
//							{
//								UseConsumable (slots [i], i, true);
//							}
//							else{

								if( equip.IsSlotOccupied(slots[i].getItemTypeIndex()) ){
									AddItemToInventorySlot(equip.GetItemInSlot(slots[i].getItemTypeIndex()).itemID, i);
								}else{
									inventory[i] = new Item();
								}

								// add item to char with given slot id specified by GetItemTypeIndex (see enum inside item)
								equip.AddItemInSlot(slots[i].itemID, slots[i].getItemTypeIndex()); 

								//LOOK AT HOW YOU DO THE DRAGGING TO REMEMBER THIS ITEM SO YOU CAN REPLACE ITEMS !!!
							}
						}
					}
//				} 
				else //for putting an item in an empty slot
				{
					if(slotRect.Contains(e.mousePosition))
					{
						if (e.type == EventType.mouseUp && draggingItem) 
						{
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}

				if(tooltip == "" || draggingItem)
				{
					showTooltip = false;
				}
				i++;
			}
		}
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

	void RemoveItem(int id) {
		for(int i = 0; i < inventory.Count; i++) {
			if(inventory[i].itemID == id) {
				inventory[i] = new Item();
				break;
			}
		}
	}

	public void AddItemToInventorySlot(int id, int slotID){
		inventory[slotID] = database.items[id];
	}

	public void AddItem(int id) {
		for(int i = 0; i < inventory.Count; i++) {
			if (inventory [i].itemName == null) {
				for(int j = 0; j < database.items.Count; j++) {
					if (database.items [j].itemID == id) {
						inventory [i] = database.items [j]; //Make the empty slot equal the item id that you passed it
					}
				}
				break; //You found an empty slot, stop for loop
			}
		}
	}

	bool InventoryContains(int id) {
		return(inventory.Find(x => x.itemID == id) != null);
	}

	public bool getShowInven() {
		return showInventory;
	}
}
