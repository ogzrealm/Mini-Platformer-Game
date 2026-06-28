using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return;
        isCollected = true;

        PlayerAudio.instance.PlayCoinPickUp();
        UIManager.instance.AddScore(100);
        Destroy(gameObject);
    }
}


