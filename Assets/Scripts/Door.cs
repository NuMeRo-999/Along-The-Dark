using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Door : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D doorCollider;
    private ShadowCaster2D SC2D;
    public AudioSource Audio;

    private void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<BoxCollider2D>();
        SC2D = GetComponent<ShadowCaster2D>();
    }

    public void openDoor()
    {
        animator.SetBool("open", true);
        Audio.Play();

        doorCollider.enabled = false;
        SC2D.enabled = false;
    }
}
