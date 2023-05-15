using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverController : MonoBehaviour
{
    public string scene = "Scene_2_Basement";

    public float transitionDuration = 2f;
    public Image fadeImage;
    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(TransitionToScene(scene));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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