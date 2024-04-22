using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchScript : MonoBehaviour
{
    private CapsuleCollider2D playerCollider;
    private Animator animator;
    //private bool crouching;
    private Movement velocity;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        velocity = FindObjectOfType<Movement>();
    }

    void Update()
    {
            crouch();
    }

    private void crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Al presionar la tecla de control izquierdo
            // se ejecuta la animación de agacharse y 
            // se activa la condicion crouch
            animator.SetBool("crouch", true);
            //crouching = true;
            playerCollider.size = new Vector2(1.599731f, 3.4f);
            playerCollider.offset = new Vector2(0.4482422f, -0.2385788f);

            velocity.Velocity = 7f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            // Al soltar la tecla de control izquierdo
            // se ejecuta la animación de levantarse y
            // se desactiva la condicion crouch en caso de que pueda hacerlo
            animator.SetBool("crouch", false);
            //crouching = false;
            playerCollider.size = new Vector2(1.725619f, 4.686248f); 
            playerCollider.offset = new Vector2(0.006063094f, -0.1714785f);

            velocity.Velocity = 15;
        }
    }
}