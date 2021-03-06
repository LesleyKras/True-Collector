﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Diagnostics;
using System.Security;

public enum behaviourType {
	Running,
	Walking
}

public class Player : MonoBehaviour, IHumanoid{
	//Stats
	public float Health;
	public float max_Health;
	public float Speed;
	public float Power;
	public int Armour;

	private float jumpSpeed = 8.0f;

	private bool jumped = false;

	public GUISkin skin;
    Game game;

    private IBehaviour behaviour;
	public behaviourType behaviourType;

    // Use this for initialization
    void Start () {
        game = GameObject.Find("Game").GetComponent<Game>();

        max_Health = 10;
        Health = max_Health;

//		Ding d = new Ding();

        behaviour = gameObject.AddComponent<Running>();
    }
	
	// Update is called once per frame
	void Update () {
        Die();

		if(Input.GetMouseButton(0)) {
			Attack(0);
		} else if(Input.GetMouseButtonDown(1)) {
			Attack(1);
		}

		if(Input.GetKeyDown("space")) {
			if(!jumped) {
				transform.position += transform.up * jumpSpeed * Time.deltaTime;
				jumped = true;
			}
		}

        if (Input.GetKeyDown("n")) {
            ChangeBehaviour();
        }

		// Show the cursor again while in-game incase you need your cursor for settings.
		if(Input.GetKeyDown("escape")) {
			Cursor.lockState = CursorLockMode.None;	
		}
	}

    public void ChangeBehaviour() {
		Component c = gameObject.GetComponent<IBehaviour>() as Component;

		if(c != null) {
			Destroy(c);
		}
	 	behaviour.ChangeBehaviour();
    }

	public void Attack(int i) {
		GameObject equipObj = GameObject.Find("Character Sheet");
		GameObject invenObj = GameObject.Find("Inventory");
		EquipmentManager equip = equipObj.GetComponent<EquipmentManager>();
		Inventory inven = invenObj.GetComponent<Inventory>();
        Gun gun = GameObject.Find("Weapon").GetComponent<Gun>();

		Item itemInSlotWeapon = equip.GetItemInSlot(3);
		Item itemInSlotOffhand = equip.GetItemInSlot(4);
		string attackStyle = itemInSlotWeapon.attackStyle;

		/*
		 * 0 == helmet
		 * 1 == armour
		 * 2 == boots
		 * 3 == weapon
		 * 4 == offhand
		 */
		if(i == 0) {
			if(!equip.getShowCharacterStats() && !inven.getShowInven()) {
                if(attackStyle == "shoot") {
                    gun.Fire();
                } else if(attackStyle == "throw") {
					print("Argh!");
					equip.RemoveItem(itemInSlotWeapon.itemID);
					equip.IsSlotOccupied(itemInSlotWeapon.getItemTypeIndex());			

					// Show a rock thrown in game
				}
			}
		}else if(i == 1) {
			if(itemInSlotOffhand.itemName == "Ammo Clip") {
				if(itemInSlotWeapon.itemName == "Hand Gun") {
					int currentWeaponAmmo = equip.GetAmmo();

                    // Offhand ammo needs to be checked differently
                    itemInSlotWeapon.itemAmmo = GetAmmoDifference(currentWeaponAmmo, itemInSlotOffhand.itemAmmo);
                }
			} else if(itemInSlotOffhand.itemName == "Flashlight") {
				GameObject flashlightObj = GameObject.Find("Flashlight");
				Flashlight flashlight = flashlightObj.GetComponent<Flashlight>();

				flashlight.SetLightStatus();
			}
		}
	}

    public int GetAmmoDifference(int weaponAmmo, int magAmmo) {
        //Mag does not lose charges yet... FIX

        int difference = magAmmo - weaponAmmo;

        if(difference < 0) {
            print("difference is less than 0");
        }

        int newWeaponAmmo = weaponAmmo + difference;
        int newMagAmmo = magAmmo - difference;

        //set mag ammo to max - difference

        if(newMagAmmo <= 0) {
            // destroy
        }
        return newWeaponAmmo;
    }

	public void Die() {
        if(Health <= 0) {
            game.showGameOver();
        }
    }

	public void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "floor") {
			jumped = false;
		}

//		if(col.gameObject.name == "Cube")
//		{
//			transform.Translate(-5.0f, 0, 0);
//		}
	}


	public void WearingEquipment(float hp, int arm, float pwr, float spd)
	{
		max_Health += hp;
		Health += hp;
		Armour += arm;
		Power += pwr;
		Speed += spd;
	}

	public void NotWearingEquipment(float hp, int arm, float pwr, float spd)
	{
		max_Health -= hp;
		Health -= hp;
		Armour -= arm;
		Power -= pwr;
		Speed -= spd;
	}
}
