using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    AudioSource sfx;
    [SerializeField] AudioClip sfxStart;
    public string scene = "MortimersLetter";
    public float transitionDuration = 2f;
    public Image fadeImage;
    public float blinkSpeed = 5f;
    public float lerpDuration = 1f; // duración de la interpolación en segundos
    private Text text;
    private float timer = 0f;

    // nuevas variables para la interpolación de color
    private Color lerpStartColor;
    private Color lerpEndColor;

    private void Start()
    {
        Cursor.visible = false;
        sfx = GetComponent<AudioSource>();
        text = GetComponent<Text>();
        //colores de inicio y fin para la interpolación
        lerpStartColor = text.color;
        lerpEndColor = new Color(1f, 1f, 1f, 0f);
    }

    private void Update()
    {
        //parpadeo
        timer += Time.deltaTime;

        if (timer >= blinkSpeed)
        {
            text.enabled = !text.enabled;
            timer = 0f;
        }

        //interpolación de color
        float t = Mathf.PingPong(Time.time, lerpDuration) / lerpDuration;
        text.color = Color.Lerp(lerpStartColor, lerpEndColor, t);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // mostrar mensaje "PRESS ENTER"
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            sfx.clip = sfxStart;
            sfx.Play();
            StartCoroutine(TransitionToScene(scene));
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
