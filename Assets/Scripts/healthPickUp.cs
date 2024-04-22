using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class healthPickUp : MonoBehaviour
{
    private Animator animator;
    public TextMeshProUGUI healthText;
    private Light2D light;

    private void Start()
    {
        animator = GetComponent<Animator>();
        light = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Highlight", true);
        healthText.gameObject.SetActive(true);
        light.enabled= true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Highlight", false);
        healthText.gameObject.SetActive(false);
        light.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponentInChildren<Health>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            health.Heal();
            Destroy(gameObject);
            
        }
    }
}
