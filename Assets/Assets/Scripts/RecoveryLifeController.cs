using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryLifeController : MonoBehaviour
{
    GameManager game;
    AudioSource sfx;
    [SerializeField] AudioClip sfxRecovery;


    void Start()
    {
        sfx = GetComponent<AudioSource>();
        game = GameManager.GetInstance();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        sfx.clip = sfxRecovery;
        sfx.Play();
        game.FullLife();
    }
}
