using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static  UIManager instance;
    
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject panel;

    private AudioSource audioSource;
    [SerializeField] private AudioClip playAgainClip;
    [SerializeField] private AudioClip backtoMenuClip;

    private int score;
    private int howManyLives;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore(int addingScore)
    {
        score += addingScore;
        scoreText.text = "Score: "+score.ToString();
        StartCoroutine(UIEffect());
    }

    IEnumerator UIEffect()
    {
        Vector2 currentScale=scoreText.rectTransform.localScale;
        scoreText.rectTransform.localScale =currentScale * 0.5f;
        
        yield return new WaitForSeconds(0.15f);
        scoreText.rectTransform.localScale = currentScale;
    }

    public void HowManyLives(int lives)
    {
        howManyLives=lives;
        StartCoroutine(UIEffect());
        livesText.text = "Lives: "+howManyLives.ToString();
    }

    public void OpentoPanel()
    {
        panel.SetActive(true);
    }

    public void PlayAgainButton()
    {
        StartCoroutine(PlayAgainCoroutine());
    }

    IEnumerator PlayAgainCoroutine()
    {
        audioSource.PlayOneShot(playAgainClip);
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenuButton()
    {
        StartCoroutine(BackToMenuCoroutine());
    }
    
    IEnumerator BackToMenuCoroutine()
    {
        audioSource.PlayOneShot(backtoMenuClip);
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene("Menu");
    }
}
