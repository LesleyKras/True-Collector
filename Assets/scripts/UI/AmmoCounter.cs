using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {
    Text ammoCounter;
    EquipmentManager equip;

	// Use this for initialization
	void Start () {
        ammoCounter = GetComponent<Text>();
        equip = GameObject.Find("Character Sheet").GetComponent<EquipmentManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if(equip.GetAmmo() > 0) {
            ammoCounter.text = "Ammo: " + equip.GetAmmo().ToString();
        } else {
            ammoCounter.text = "";
        }
	}
}
