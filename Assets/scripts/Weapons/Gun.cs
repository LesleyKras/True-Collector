using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    EquipmentManager equip;
    public Transform shootPoint;

    public float range = 100.0F;
    public float fireTimer;

    // Use this for initialization
    void Start() {
        equip = GameObject.Find("Character Sheet").GetComponent<EquipmentManager>();
    }

    // Update is called once per frame
    void Update() {
        if(fireTimer < equip.GetWeaponSpeed()) {
            fireTimer += Time.deltaTime;
        }
    }

    public void Fire() {
        if (equip.GetAmmo() >= 1) {
            if (fireTimer < equip.GetWeaponSpeed()) return;

            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name + " found!");
            }

            fireTimer = 0.0F;
            // Bugged
            equip.GetItemInSlot(3).itemAmmo--;
        } else {
            print("You're out of ammo!");
        }
    }
}
