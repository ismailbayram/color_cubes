using UnityEngine;

public class Tutorial : MonoBehaviour {
    public GameObject Step;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && this.Step != null) {
            this.Step.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Continue() {
        Time.timeScale = 1f;
        this.Step.SetActive(false);
        Destroy(this);
    }
}
