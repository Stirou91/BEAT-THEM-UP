using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDisk : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sound;
    public int points = 200;
    public float time = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //AudioSource.PlayClipAtPoint(sound, transform.position);
            audioSource.Play();
            Inventory.instance.AddCoints(points);
            Destroy(gameObject, time);
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
