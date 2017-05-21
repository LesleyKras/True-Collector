﻿using System.Collections;using System.Collections.Generic;using UnityEngine;using System;using System.Xml;using System.Diagnostics;public class Player : MonoBehaviour, IHumanoid{	//Stats	public float Health;	public float max_Health;	public float Speed;	public float Power;	public int Armour;    //private float speed = 10.0F;	//public float speed = 10.0f;	public float jumpSpeed = 8.0f;	public bool jumped = false;    private bool isRunning = true;	public GUISkin skin;    Game game;    private Behaviour behaviour;

    // Use this for initialization
    void Start () {        game = GameObject.Find("Game").GetComponent<Game>();

        max_Health = 10;
        Health = max_Health;        this.behaviour = gameObject.AddComponent<Running>();    }		// Update is called once per frame	void Update () {        Die();		if(Input.GetMouseButtonDown(0)) {			Attack(0);		} else if(Input.GetMouseButtonDown(1)) {			Attack(1);		}		if(Input.GetKeyDown("space")) {			if(!jumped) {				transform.position += transform.up * jumpSpeed * Time.deltaTime;				jumped = true;			}		}        if (Input.GetKeyDown("n")) {
            ChangeBehaviour();
        }		// Show the cursor again while in-game incase you need your cursor for settings.		if(Input.GetKeyDown("escape")) {			Cursor.lockState = CursorLockMode.None;			}	}    public void ChangeBehaviour() {
        if (isRunning) {
            Destroy(this.behaviour.GetComponent<Running>());
            this.behaviour = gameObject.AddComponent<Walking>();
            isRunning = false;
            //this.behaviour = gameObject.AddComponent<Walking>();
            //this.behaviour = new Walking(this);
        } else {
            Destroy(this.behaviour.GetComponent<Walking>());
            this.behaviour = gameObject.AddComponent<Running>();
            isRunning = true;
            //this.behaviour = gameObject.AddComponent<Running>();
            //this.behaviour = new Running(this);
        }
    }	public void Attack(int i) {		GameObject equipObj = GameObject.Find("Character Sheet");		GameObject invenObj = GameObject.Find("Inventory");		EquipmentManager equip = equipObj.GetComponent<EquipmentManager>();		Inventory inven = invenObj.GetComponent<Inventory>();		Item itemInSlotWeapon = equip.GetItemInSlot(3);		Item itemInSlotOffhand = equip.GetItemInSlot(4);		string attackStyle = itemInSlotWeapon.attackStyle;		/*		 * 0 == helmet		 * 1 == armour		 * 2 == boots		 * 3 == weapon		 * 4 == offhand		 */		if(i == 0) {			if(!equip.getShowCharacterStats() && !inven.getShowInven()) {				if(attackStyle == "shoot") {					if(itemInSlotWeapon.itemAmmo <= 0) {						print("You're out of ammo!");					} else {						print("pew pew pew pew pew");						itemInSlotWeapon.itemAmmo--;						}					// Show  bullets				} else if(attackStyle == "throw") {					print("Argh!");					equip.RemoveItem(itemInSlotWeapon.itemID);					equip.IsSlotOccupied(itemInSlotWeapon.getItemTypeIndex());								// Show a rock thrown in game				} else {					print("You're not wearing any weapon!");				}			}		}else if(i == 1) {			if(itemInSlotOffhand.itemName == "Ammo Clip") {				if(itemInSlotWeapon.itemName == "Hand Gun") {					int currentAmmo = equip.GetAmmo();					if(currentAmmo >= 12) {						print("Your mag is already full");					} else {						itemInSlotWeapon.itemAmmo = currentAmmo += itemInSlotOffhand.itemAmmo;						equip.RemoveItem(itemInSlotOffhand.itemID);						equip.IsSlotOccupied(itemInSlotOffhand.getItemTypeIndex());					}				}			} else if(itemInSlotOffhand.itemName == "Flashlight") {				GameObject flashlightObj = GameObject.Find("Flashlight");				Flashlight flashlight = flashlightObj.GetComponent<Flashlight>();				flashlight.SetLightStatus();			}		}	}	public void Die() {        if(Health <= 0) {            game.showGameOver();        }    }	public void OnCollisionEnter (Collision col)	{		if (col.gameObject.tag == "floor") {			jumped = false;		}//		if(col.gameObject.name == "Cube")//		{//			transform.Translate(-5.0f, 0, 0);//		}	}	public void WearingEquipment(float hp, int arm, float pwr, float spd)	{		max_Health += hp;		Health += hp;		Armour += arm;		Power += pwr;		Speed += spd;	}	public void NotWearingEquipment(float hp, int arm, float pwr, float spd)	{		max_Health -= hp;		Health -= hp;		Armour -= arm;		Power -= pwr;		Speed -= spd;	}}