using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject MainMenuCanvas;
    public GameObject LevelWrapperCanvas;
    public GameObject LevelWrapper;
    public GameObject LevelButton;
    public GameObject ErrorCanvas;
    public SceneFader fader;

    private AudioSource a_source;

    private void Start() {
        this.a_source = this.GetComponent<AudioSource>();
    }

    public void Play() {
        if(Application.internetReachability == NetworkReachability.NotReachable){
            this.ErrorCanvas.SetActive(true);
            return;
        }
        this.ErrorCanvas.SetActive(false);
        this.MainMenuCanvas.SetActive(false);
        this.a_source.Play();
        this.fader.FadeTo("Level" + (PlayerPrefs.GetInt("LevelReached")).ToString());
    }

    public void ShowLevels() {
        this.MainMenuCanvas.SetActive(false);
        this.LevelWrapperCanvas.SetActive(true);
        StartCoroutine(DrawLevelButtons());
    }

    IEnumerator DrawLevelButtons() {
        for(int i = 0; i < 20; i++) {
            GameObject button = (GameObject) Instantiate(this.LevelButton, this.LevelWrapper.transform.position, Quaternion.identity);
            button.transform.SetParent(this.LevelWrapper.transform);
            LevelButton levelButton = button.GetComponent<LevelButton>();
            levelButton.level = i;
            levelButton.fader = this.fader;
            if (i == 0 || i <= PlayerPrefs.GetInt("LevelReached"))
                levelButton.locked = false;
            else
                levelButton.locked = true;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
