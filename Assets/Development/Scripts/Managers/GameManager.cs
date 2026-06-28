using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int lives=3;
    private bool isDead = false;

    private void Awake()
    {
        if (instance != null )
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UIManager.instance.HowManyLives(lives);
    }

    public void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void DeathState()
    {
        if (lives > 1)
        {
            TakeLife();
        }
        else
        {
            resetGameSystem();
            lives = 3;
        }
    }

    void TakeLife()
    {
        lives--;
        UIManager.instance.HowManyLives(lives);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        
    }

    void resetGameSystem()
    {
        lives = 3;
        SceneManager.LoadScene(0);
    }
    
}
