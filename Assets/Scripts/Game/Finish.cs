using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour {
    private bool triggered = false;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !this.triggered) {
            this.triggered = true;
            GameManager.instance.CompleteLevel();
        }
    }
}
