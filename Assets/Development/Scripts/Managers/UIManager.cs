using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static  UIManager instance;
    
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

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

    public void AddScore(int addingScore)
    {
        score += addingScore;
        scoreText.text = "Score: "+score.ToString();
        StartCoroutine(ScoreEffect());
    }

    IEnumerator ScoreEffect()
    {
        Vector2 currentScale=scoreText.rectTransform.localScale;
        scoreText.rectTransform.localScale =currentScale * 0.5f;
        
        yield return new WaitForSeconds(0.15f);
        scoreText.rectTransform.localScale = currentScale;
    }

    public void HowManyLives(int lives)
    {
        howManyLives=lives;
        livesText.text = "Lives: "+howManyLives.ToString();
    }
}
