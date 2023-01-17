using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTape : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sound;
    public int points = 50;
    public float time = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(sound, transform.position,1000);
            //audioSource.Play();
            Inventory.instance.AddCoints(points);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
