using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int misses = 0;
    public int maxMisses = 3;

    public bool gameOver = false;
    public bool hasWon = false;

    public AudioClip loseSFX;
    public AudioClip winSFX;

    public GameObject winEffectPrefab;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void RegisterMiss()
    {
        if (gameOver || hasWon) return;

        misses++;

        if (misses >= maxMisses)
        {
            TriggerLose();
        }
    }

    public void RegisterBombShot()
    {
        if (gameOver || hasWon) return;

        TriggerLose();
    }

    public void TriggerLose()
    {
        if (gameOver || hasWon) return;

        gameOver = true;

        if (loseSFX != null)
        {
            AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position, 1f);
        }

        StartCoroutine(LoseRoutine());
    }

    public void TriggerWin()
    {
        if (gameOver || hasWon) return;

        hasWon = true;

        if (winSFX != null)
        {
            AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position, 1f);
        }

        if (winEffectPrefab != null)
        {
            Instantiate(winEffectPrefab, Vector3.zero, Quaternion.identity);
        }

        StartCoroutine(WinRoutine());
    }

    IEnumerator LoseRoutine()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        RestartScene();
    }

    IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(10f);
        RestartScene();
    }

    void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}