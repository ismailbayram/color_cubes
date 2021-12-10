using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    public GameObject buttonText;
    public GameObject buttonImage;
    public int level;
    public bool locked = true;
    public SceneFader fader;

    private void Start() {
        this.buttonText.SetActive(!this.locked);
        this.GetComponent<Button>().interactable = !this.locked;
        if (!this.locked)
            this.buttonText.GetComponent<Text>().text = this.level.ToString();
        this.buttonImage.SetActive(this.locked);
    }

    public void LoadLevel() {
        this.fader.FadeTo("Level" + (this.level).ToString());
    }
}
