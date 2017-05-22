using UnityEngine;
using System.Collections;

public class Running : MonoBehaviour, IBehaviour {
    Player player;
    public float speed;

    public Running(Player p){
        //player = p;
    }

    void Start() {
        speed = 5.0F;    
    }

    // Update is called once per frame
    public void Update() {
        float horizontalMovement = Input.GetAxis("Horizontal") * speed;
        float verticalMovement = Input.GetAxis("Vertical") * speed;

        horizontalMovement *= Time.deltaTime;
        verticalMovement *= Time.deltaTime;

        transform.Translate(horizontalMovement, 0, verticalMovement);
    }
}
