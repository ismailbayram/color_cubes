using UnityEngine;

public class Door : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            GameManager.SetReversable(false);
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Player") {
            GameManager.SetReversable(true);
        }
    }
}
