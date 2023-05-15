using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene1to2 : MonoBehaviour
{
    [SerializeField] public string scene2 = "Scene_2_Basement";

    public float transitionDuration = 1f;
    public Image fadeImage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            //Debug.Log("Player entered collider!!!!");
            StartCoroutine(TransitionToScene(scene2));
        }
    }

    IEnumerator TransitionToScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);

        Color fadeColor = fadeImage.color;
        fadeColor.a = 0f;
        fadeImage.color = fadeColor;

        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime / transitionDuration;
            fadeImage.color = fadeColor;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime / transitionDuration;
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }
}