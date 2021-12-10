using UnityEngine;

public class PlayerMove : MonoBehaviour {
    CharacterController controller; 
    void Start() {
       this.controller = this.GetComponent<CharacterController>(); 
    }

    void Update() {
        // TODO: avoid allocate a new variable for vector3
        Vector3 movement = new Vector3(SimpleInput.GetAxis("Vertical"), 0f, SimpleInput.GetAxis("Horizontal") * -1);
        this.controller.Move(movement * Time.deltaTime * 3f);
    }
}
