using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class note : MonoBehaviour
{
    private Animator animator;
    private Light2D light;
    private bool leyendo;
    public TextMeshProUGUI noteText;
    public AudioSource audio;
    public GameObject panel;


    void Start()
    {
        animator = GetComponent<Animator>();
        light = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Highlight", true);
        noteText.gameObject.SetActive(true);
        light.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Highlight", false);
        noteText.gameObject.SetActive(false);
        light.enabled = false;
        panel.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision) // si está dentro de la colision
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            leyendo = true;
            audio.Play();
            panel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && leyendo){
            panel.SetActive(false);
            audio.Play();
        }
    }
}
