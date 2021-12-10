using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static string ColorToPick;
    public static bool Reversable = true;

    public int level;
    public SceneFader fader;
    public Renderer playerCube;
    public GameObject colorMemoryUIWrapper;
    public GameObject colorMemoryItemPrefab;
    public GameObject ErrorCanvas;
    public GameObject congratsCanvas;
    
    [Header("Ads")]
    public AdService adService;
    public GameObject rewardCanvas;

    [Header("Buttons")]
    public Button pickColorButton;
    public Button reverseColorButton;

    [Header("Colors")]
    public Color white;
    public Color purple;
    public Color orange;
    public Color red;
    
    [Header("Doors")]
    public Animator[] whiteDoors;
    public Animator[] purpleDoors;
    public Animator[] orangeDoors;
    public Animator[] redDoors;

    [Header("Audio Clips")]
    public AudioClip cubeClip;
    public AudioClip errorClip;
    
    public static Dictionary<string, Color> Colors; 

    private Stack<string> ColorMemory;
    private Dictionary<string, Animator[]> doors;
    private string currentColor;
    private AudioSource a_source;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("there are more than one GameManager");
            return;
        }
        instance = this;
    }

    void Start() {
        Colors = new Dictionary<string, Color>();
        Colors.Add("white", this.white);
        Colors.Add("purple", this.purple);
        Colors.Add("orange", this.orange);
        Colors.Add("red", this.red);
        this.ColorMemory = new Stack<string>();
        this.ColorMemory.Push("white");
        ColorToPick = "white";
        this.currentColor = "white";

        this.doors = new Dictionary<string, Animator[]>();
        this.doors["white"] = this.whiteDoors;
        this.doors["purple"] = this.purpleDoors;
        this.doors["orange"] = this.orangeDoors;
        this.doors["red"] = this.redDoors;

        this.a_source = this.GetComponent<AudioSource>();

        this.SwitchDoors();
        AnalyticsResult result = Analytics.CustomEvent("level_start", new Dictionary<string, object>{
            {"level_index", this.level.ToString()}
        });

        if (result != AnalyticsResult.Ok)
            Debug.LogError("ANALYTICS ERRORS: " + result.ToString());
    }

    // void Update() {
    //     if(Input.GetKeyDown(KeyCode.F)) {
    //         PickColor();
    //     }

    //     if(Input.GetKeyDown(KeyCode.R)) {
    //         ReverseColor();
    //     }
    // }

    void printStack() {
        foreach(string c in this.ColorMemory) {
            Debug.Log(c);
        }
    }

    public void MemoryError() {
        this.a_source.PlayOneShot(this.errorClip);
    }

    public void PickColor() {
        if(this.ColorMemory.Peek() == ColorToPick && ColorToPick != "white" || ColorToPick == "white" || this.ColorMemory.Count >= 6){
            this.MemoryError();
            return;
        }
        this.ColorMemory.Push(ColorToPick);
        this.currentColor = ColorToPick;
        this.playerCube.material.color = Colors[ColorToPick];
        ColorToPick = "white";
        this.SwitchDoors();
        this.addColorToMemoryUI();
        SetReversable(true);
    }

    public void ReverseColor() {
        if(this.ColorMemory.Peek() == "white" || !Reversable)
            return;
        this.ColorMemory.Pop();
        string prevColor = this.ColorMemory.Peek();
        this.currentColor = prevColor;
        this.playerCube.material.color = Colors[prevColor];
        this.SwitchDoors();
        this.removeColorFromMemoryUI();
        if(this.ColorMemory.Count == 1)
            SetReversable(false);
    }

    public static void SetReversable(bool reversable) {
        Reversable = reversable;
        instance.reverseColorButton.interactable = reversable;
    }

    public static void SetPickable(bool pickable, string colorToPick) {
        instance.pickColorButton.interactable = pickable;
        ColorToPick = colorToPick;
    }

    public void addColorToMemoryUI() {
        GameObject colorItem = (GameObject) Instantiate(this.colorMemoryItemPrefab, this.colorMemoryUIWrapper.transform.position, Quaternion.identity);
        Image colorImage = colorItem.GetComponent<Image>();
        Color imageColor = Colors[this.currentColor];
        imageColor.a = 1;
        colorImage.color = imageColor;
        colorItem.transform.SetParent(this.colorMemoryUIWrapper.transform);
    }

    public void removeColorFromMemoryUI() {
        GameObject lastColorItem = this.colorMemoryUIWrapper.transform.GetChild(this.colorMemoryUIWrapper.transform.childCount - 1).gameObject;
        Destroy(lastColorItem);
    }

    private void SwitchDoors() {
        this.a_source.PlayOneShot(this.cubeClip);
        foreach(Animator door in this.doors["white"]) {
            door.SetBool("open", false);
        }

        foreach(Animator door in this.doors["purple"]) {
            door.SetBool("open", false);
        }

        foreach(Animator door in this.doors["orange"]) {
            door.SetBool("open", false);
        }

        foreach(Animator door in this.doors["red"]) {
            door.SetBool("open", false);
        }

        foreach(Animator door in this.doors[this.currentColor]) {
            door.SetBool("open", true);
        }
    }

    public void RewardPlayer() {
        this.rewardCanvas.SetActive(true);
    }

    public void RestartLevel() {
        AnalyticsResult result = Analytics.CustomEvent("level_restart", new Dictionary<string, object>{
            {"level_index", this.level.ToString()}
        });
        if (result != AnalyticsResult.Ok)
            Debug.LogError("ANALYTICS ERRORS: " + result.ToString());
        this.fader.FadeTo("Level" + (this.level).ToString());
    }

    public void GoToMenu() {
        this.fader.FadeTo("MainMenu");
    }

    public void CompleteLevel() {
        if(Application.internetReachability == NetworkReachability.NotReachable){
            this.ErrorCanvas.SetActive(true);
            return;
        }
        this.ErrorCanvas.SetActive(false);

        AnalyticsResult result = Analytics.CustomEvent("level_complete", new Dictionary<string, object>{
            {"level_index", this.level.ToString()}
        });
        if (result != AnalyticsResult.Ok)
            Debug.LogError("ANALYTICS ERRORS: " + result.ToString());

        if (this.level % 3 == 2) {
            this.adService.DisplayNonRewardedAd();
        }

        if (this.level == 19) {
            this.congratsCanvas.SetActive(true);
        } else {
            int levelReached = Mathf.Max(this.level + 1, PlayerPrefs.GetInt("LevelReached"));
            PlayerPrefs.SetInt("LevelReached", levelReached);
            this.fader.FadeTo("Level" + (this.level + 1).ToString());
        }
    }
}
