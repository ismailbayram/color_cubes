using UnityEngine;

public class ColorPicker : MonoBehaviour {
    public string color;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            GameManager.SetPickable(true, this.color);
        }
    }
    
    private void OnTriggerStay(Collider other) {
        if (other.transform.tag == "Player") {
            GameManager.SetPickable(true, this.color);
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Player") {
            GameManager.SetPickable(false, "white");
        }
    }
}
