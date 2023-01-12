using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDisk : MonoBehaviour
{
    bool peuxRamasserCannette;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            peuxRamasserCannette = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            peuxRamasserCannette = false;
        }
    }
}
