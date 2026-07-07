using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(creditsEnd());
    }

    private IEnumerator creditsEnd()
    {
        yield return new WaitForSecondsRealtime(18.5f);
        SceneManager.LoadScene("Menu");
    }
}
