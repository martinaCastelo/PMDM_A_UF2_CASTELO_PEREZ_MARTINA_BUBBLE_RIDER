using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDropsController : MonoBehaviour
{
    //[SerializeField] GameObject lifeObject;
    public int lifeToAdd;
    //AudioSource sfx;
    //[SerializeField] AudioClip sfxLife;

    private void Start()
    {
        //sfx = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            //sfx.clip = sfxLife;
            //sfx.Play();
            GameManager.GetInstance().ExtraLife(lifeToAdd);
            //Instantiate(lifeObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
