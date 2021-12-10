using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public GameObject mapCenter;
    public Vector3 cameraOffset;
    public Animator animator;

    private GameObject controller;
    private GameObject target;
    private bool isWide = false;

    void Start() {
        this.target = this.player;
        this.animator = this.GetComponent<Animator>();
        this.controller = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update() {
        this.transform.position = this.cameraOffset + this.target.transform.position;
    }

    public void SwitchAngle() {
        this.isWide = !this.isWide;
        this.controller.SetActive(!this.isWide);
        if (this.isWide) {
            this.target = this.mapCenter;
        } else {
            this.target = this.player;
        }
        this.animator.SetBool("wide", this.isWide);
    }
}
