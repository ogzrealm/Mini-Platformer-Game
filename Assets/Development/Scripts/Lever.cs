using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject dungeonDoor;
    [SerializeField] private Color color;
    
    private bool isLeverActivated = false;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isLeverActivated) return;
        isLeverActivated = true;
        ObjectsAudio.instance.PlayLeverSound();
        GetComponent<Tilemap>().color = color;
        dungeonDoor.SetActive(false);
    }
}
