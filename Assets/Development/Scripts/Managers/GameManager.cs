using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int lives=5;
    private bool isDead = false;
    
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Transform playerLocation;
    
    
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private CapsuleCollider2D playerCollider2;
    [SerializeField] private CinemachineStateDrivenCamera playerCamera;
    
    private PlayerMovement playerMovement;
    private Triggers triggers;

    private AudioSource audiosource;

    private void Awake()
    {
        if (instance != null )
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        playerMovement=FindAnyObjectByType<PlayerMovement>();
        triggers=FindAnyObjectByType<Triggers>();
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
        StartCoroutine(NextLevelCoroutine());
    }

    private IEnumerator NextLevelCoroutine()
    {
        audiosource.Stop();
        ObjectsAudio.instance.PlayerLevelCompleteSound();
        yield return new WaitForSecondsRealtime(3f);
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
            StartCoroutine(ResetGame());
        }
        
    }

    void TakeLife()
    {
        lives--;
        UIManager.instance.HowManyLives(lives);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2.5f);
        playerAnimator.ResetTrigger("Dying");
        playerCollider.enabled = true;
        playerCollider2.enabled = true;
        playerLocation.position = respawnPoint.position;
        playerMovement.canMove = true;
        playerAnimator.Play("Idling");
        playerCamera.enabled = true;
        triggers.isTriggered = false;
    }

    private IEnumerator ResetGame()
    {
        UIManager.instance.HowManyLives(0);
        yield return new WaitForSeconds(2.5f);
        UIManager.instance.OpentoPanel();
    }
    
}
