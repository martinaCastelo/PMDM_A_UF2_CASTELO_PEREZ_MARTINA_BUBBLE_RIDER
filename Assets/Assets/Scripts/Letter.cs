using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    AudioSource sfx;
    [SerializeField] AudioClip sfxLetter;
    public Text textComponent;
    public string scene = "Scene_1_Outside 1";

    public float transitionDuration = 2f;
    public Image fadeImage;
    [SerializeField] private string[] introText; // Array de strings que se pueden modificar desde el editor

    private int currentTextIndex = 0;

    private bool isCoroutineRunning = false;
    private void Start()
    {
        Cursor.visible = false;
        sfx = GetComponent<AudioSource>();
        StartCoroutine(TypeIntroText());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Si se presiona enter, avanzamos al siguiente texto si la Coroutine ya ha terminado de correr
            if (!isCoroutineRunning)
            {
                currentTextIndex++;
                if (currentTextIndex >= introText.Length)
                {
                    StartCoroutine(TransitionToScene(scene));
                }
                else
                {
                    //corrutina para mostrar el siguiente texto
                    StartCoroutine(TypeIntroText());
                }
            }
        }
    }

    IEnumerator TypeIntroText()
    {
        isCoroutineRunning = true;

        if (currentTextIndex < introText.Length)
        {
            string currentText = introText[currentTextIndex];
            textComponent.text = "";

            // Mostramos cada letra poco a poco
            for (int i = 0; i < currentText.Length; i++)
            {
                sfx.clip = sfxLetter;
                sfx.Play();
                textComponent.text += currentText[i];
                yield return new WaitForSeconds(0.1f); // Esperamos un tiempo entre cada letra
            }
        }

        isCoroutineRunning = false;
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
