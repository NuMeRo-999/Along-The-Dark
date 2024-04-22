using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    public float Horizontal;
    public float Velocity;
    public AudioSource Audio;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate(){

        Horizontal = Input.GetAxisRaw("Horizontal");
        Rigidbody2D.velocity = new Vector2(Horizontal * Velocity, Rigidbody2D.velocity.y);

        Animator.SetBool("running", Horizontal != 0.0f);

        if (Horizontal != 0.0f)
        {
            if (!Audio.isPlaying)
            {
                Audio.Play();
            }
        }
        else
        {
            Audio.Stop();
        }
    }
}
