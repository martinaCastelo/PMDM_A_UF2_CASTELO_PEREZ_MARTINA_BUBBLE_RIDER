using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public float transitionDuration = 2f;
    public Image fadeImage;
    const int LIVES = 100;
    public static GameManager instance;
    [SerializeField] Text life;
    [SerializeField] Text message;
    [SerializeField] GameObject box;
    [SerializeField] GameObject player;
    int lives = LIVES;
    bool gameOver;
    bool paused;
    bool quitConfirmation;
    bool isQuitting = false;
    bool showingDialog = false;
    public bool isGameOver() { return gameOver; }
    public bool isGamePaused() { return paused; }
    public static GameManager GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    public void LoseLife()
    {
        if (lives > 0)
            lives--;
        if (lives == 0)
            GameOver();
    }

    void GameOver()
    {
        gameOver = true;
        SceneManager.LoadScene("GameOver");
    }

    public void FullLife()
    {
        lives = 100;
    }

    public void ExtraLife(int lifeToAdd)
    {
        if (lives < 100)
        {
            lives += lifeToAdd;
            if (lives > 100)
            {
                lives = 100;
            }
        }
    }

    private void OnGUI()
    {
        //actualizar la vida
        life.text = string.Format("{0,3:D3}", lives);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isQuitting = true;
            box.SetActive(true);
            PauseGame();
            message.text = "Are you sure you want to quit?\nYES -Y-  NO -N-";
        }

        if (isQuitting)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                ResumeGame();
                isQuitting = false;
                box.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                ResumeGame();
                isQuitting = false;
                box.SetActive(false);
                StartCoroutine(TransitionToScene("Intro"));
            }
        }
        else if (!gameOver && Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
    }



    void Quit()
    {
        isQuitting = true;
    }

    void PauseGame()
    {
        box.SetActive(true);
        paused = true;
        Camera.main.GetComponent<AudioSource>().Stop();
        message.text = "PAUSED\nPRESS -P- TO RESUME";

        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        box.SetActive(false);
        paused = false;
        Camera.main.GetComponent<AudioSource>().Play();
        message.text = "";
        Time.timeScale = 1;
        quitConfirmation = false;
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
