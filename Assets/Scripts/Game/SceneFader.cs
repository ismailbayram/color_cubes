using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {
    public Animator anim;

    public void FadeTo(string scene) {
        this.anim.Play("FadeOutEffect");
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeOut(string scene)
	{
		float t = 0f;

		while (t < 2f)
		{
			t += Time.deltaTime;
			yield return 0;
		}

		SceneManager.LoadScene(scene);
	}
}
