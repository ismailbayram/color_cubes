using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class Reward : MonoBehaviour {
    public GameObject rewardMessage;
    
    private Dictionary<string, object> parameters = new Dictionary<string, object>();

    private void Start() {
        this.parameters.Add("level_index", GameManager.instance.level.ToString());
        this.parameters.Add("red", 0);
        this.parameters.Add("orange", 0);
        this.parameters.Add("purple", 0);
    }

    public void GiveColor(string color) {
        this.parameters[color] = 1;
        AnalyticsResult result = Analytics.CustomEvent("take_color", parameters);
        GameManager.ColorToPick = color;
        GameManager.instance.PickColor();
        this.gameObject.SetActive(false);
        this.rewardMessage.SetActive(true);
        Destroy(this.rewardMessage, 3f);
    }
}
