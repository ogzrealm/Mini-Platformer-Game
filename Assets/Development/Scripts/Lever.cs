using System;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject dungeonDoor;
    private void OnCollisionEnter2D(Collision2D other)
    {
        dungeonDoor.SetActive(false);
    }
}
