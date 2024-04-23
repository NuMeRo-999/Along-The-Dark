using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private CapsuleCollider2D playerCollider;
    public bool alive = true;
    private float maxHealth = 0.95f;
    public AudioSource audio;
    public SpriteRenderer gunSprite;
    public Image image;
    public Animator animator;
    public GameObject Linterna;
    public GameObject UIRevive;
    
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        image.fillAmount= maxHealth;
        UIRevive.SetActive(false);
    }

    public void takeDamage()
    {
        image.fillAmount -= 0.10f;

        Shake.Instance.shakeCamera(0.2f, 0.1f);

        //Muere el player
        if(image.fillAmount <= 0.36f)
        {
            if (alive)
            {
                animator.SetTrigger("Dead");
                alive= false;
                UIRevive.SetActive(true);
            }
            
            playerCollider.size = new Vector2(1.725619f, 3.8f);
            GetComponent<Movement>().enabled = false;
            GetComponentInChildren<AimWeapon>().enabled = false;
            gunSprite.enabled = false;
            Linterna.SetActive(false);
        }
    }

    public void Heal()
    {
        image.fillAmount += 0.20f;
        audio.Play();
        if (image.fillAmount > maxHealth)
        {
            image.fillAmount = maxHealth;
        }
    }
}
