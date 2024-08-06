using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class cutsceenTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayableDirector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayableDirector.Play();
        }   
    }
}
