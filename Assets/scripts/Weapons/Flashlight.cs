using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
	private bool shiningLight;

	GameObject flashlightObj;
	GameObject equipObj;

	EquipmentManager equip;

	// Use this for initialization
	void Start () {
		flashlightObj = GameObject.Find("Flashlight");
		equipObj = GameObject.Find("Character Sheet");
		equip = equipObj.GetComponent<EquipmentManager>();
		shiningLight = false;
	}
	
	// Update is called once per frame
	void Update () {
		Item inSlotOffhand = equip.GetItemInSlot(4);

		if(inSlotOffhand.itemName != "Flashlight") {
			shiningLight = false;
		}

		if(shiningLight) {
			flashlightObj.GetComponent<Light>().intensity = 4;
		} else {
			flashlightObj.GetComponent<Light>().intensity = 0;
		}
	}

	public void SetLightStatus() {
		shiningLight = !shiningLight;
	}
}
