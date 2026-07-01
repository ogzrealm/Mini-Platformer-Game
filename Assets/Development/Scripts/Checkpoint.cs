using System.Collections;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private TextMeshProUGUI checkpointText;
    private bool isCheckpoint = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCheckpoint) return;
        if (other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            respawnPoint.position = transform.position;
            StartCoroutine(CheckPointTextAnim());
            isCheckpoint = true;
        }
    }

    private IEnumerator CheckPointTextAnim()
    {
        checkpointText.gameObject.SetActive(true);
        ObjectsAudio.instance.PlayCheckPointSound();
        yield return new WaitForSeconds(2f);
        checkpointText.gameObject.SetActive(false);
    }
}
