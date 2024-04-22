using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator= GetComponent<Animator>();
    }

    public void FadeOut()
    {
        animator.Play("FadeOut");
    }

    public void FadeIn() 
    {
        animator.Play("FadeIn");
    }
}
