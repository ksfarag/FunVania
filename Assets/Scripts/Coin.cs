using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    private bool collected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player" && !collected)
        {
            collected = true;
            FindObjectOfType<GameSession>().ManageScore(10);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
