using UnityEngine;
using System.Collections;

public class Walking : MonoBehaviour, IBehaviour {
    Player player;
    public float speed;

    public Walking(Player p) {
        player = p;
    }
    
    void Start() {
        speed = 1.0F;
    }

    // Update is called once per frame
    public void Update() {
        // Perhaps adding in stuff like weight to reduce run / walk speed.
        float horizontalMovement = Input.GetAxis("Horizontal") * speed;
        float verticalMovement = Input.GetAxis("Vertical") * speed;

        horizontalMovement *= Time.deltaTime;
        verticalMovement *= Time.deltaTime;

        transform.Translate(horizontalMovement, 0, verticalMovement);
    }

	public void ChangeBehaviour() {
		gameObject.AddComponent<Running>();
		Destroy(this);
	}
}
