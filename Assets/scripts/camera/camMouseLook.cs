using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour {
	Vector2 mouseLook;
	Vector2 smoothV;

	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	private bool canMoveCamera;

	GameObject player;

	GameObject equipObj;
	GameObject invenObj;

	EquipmentManager equip;
	Inventory inven;

	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject;
		equipObj = GameObject.Find("Character Sheet");
		invenObj = GameObject.Find("Inventory");

		equip = equipObj.GetComponent<EquipmentManager>();
		inven = invenObj.GetComponent<Inventory>();
		canMoveCamera = true;
	}
	
	// Update is called once per frame
	void Update () {
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		if(canMoveCamera) {
			md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
			mouseLook += smoothV;
			mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
			player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);			
		}
	}

	public void StopCamera() {
		if(inven.getShowInven() || equip.getShowCharacterStats()) {
			canMoveCamera = false;	
		}
	}

	public void StartCamera() {
		if(!inven.getShowInven() && !equip.getShowCharacterStats()) {
			canMoveCamera = true;
		}
	}
}
