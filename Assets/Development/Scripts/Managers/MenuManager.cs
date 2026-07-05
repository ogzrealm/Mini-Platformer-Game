using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip playSound, quitSound;

    private void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }
    public void PlayButton()
    {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        audioSource.PlayOneShot(playSound);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(1);
    }
    
    public void QuitButton()
    {
        StartCoroutine(QuitCoroutine());
    }
    
    private IEnumerator QuitCoroutine()
    {
        audioSource.PlayOneShot(quitSound);
        yield return new WaitForSeconds(0.4f);
        Application.Quit();
    }
    
}
