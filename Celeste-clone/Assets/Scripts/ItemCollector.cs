using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    int apples = 0;
    [SerializeField] Text applesText;
    [SerializeField] AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            collectSoundEffect.Play();
            apples++;
            applesText.text = "Apples: " + apples;
        }
    }
}
