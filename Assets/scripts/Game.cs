using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
    void Awake() {
        Debug.Log("Your name is: " + Manager.Instance.characterName);
    }

    public void showGameOver() {
        print("You have died. R.I.P.");
    }
}
