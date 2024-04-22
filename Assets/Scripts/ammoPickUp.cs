using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ammoPickUp : MonoBehaviour
{
    private Animator animator;
    private Light2D light;
    public TextMeshProUGUI ammoText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        light = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Highlight",true);
        ammoText.gameObject.SetActive(true);
        light.enabled = true;

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Highlight", false);
        ammoText.gameObject.SetActive(false);
        light.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision) // si está dentro de la colision
    {
        Ammo ammo = collision.gameObject.GetComponentInChildren<Ammo>(); // referencia a ammo

        if (ammo && Input.GetKeyDown(KeyCode.E))
        {
            ammo.addAmmo(Random.Range(3,10));
            Destroy(gameObject);
            
        }
    }
}