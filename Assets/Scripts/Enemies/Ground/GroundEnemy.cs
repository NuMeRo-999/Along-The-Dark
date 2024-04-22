using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] float health;
    private int rutina;
    public float cronometro;
    private Animator animator;
    public float WalkSpeed;
    public float RunSpeed;
    public GameObject player;
    public bool atacando;
    public int direccion;

    public float rangoVision;
    public float rangoAtaque;
    public AudioSource audio;
    public AudioSource hitAudio;
    public GameObject rango;
    public GameObject hit;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("player");
    }

    private void Update()
    {
        Comportamientos();
    }

    public void Comportamientos() {

        if(Mathf.Abs(transform.position.x - player.transform.position.x) > rangoVision && !atacando) {
            animator.SetBool("walk", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0: //Se queda quieto
                    animator.SetBool("walk", false);
                    break;

                case 1: 
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;

                case 2:
                    switch (direccion)
                    {
                        case 0: //Se mueve derecha
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.right * WalkSpeed * Time.deltaTime);
                            break;
                        case 1: //Se mueve izquierda
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * WalkSpeed * Time.deltaTime);
                            break;
                    }
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if(Mathf.Abs(transform.position.x - player.transform.position.x) > rangoAtaque && !atacando)
            {
                if(transform.position.x < player.transform.position.x) // El enemigo persigue al jugador
                {
                    transform.Translate(Vector3.right * RunSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    animator.SetBool("attack", false);
                }
                else
                {
                    transform.Translate(Vector3.right * RunSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    animator.SetBool("attack", false);
                }
            }
            else
            {
                if (!atacando)
                {
                    if(transform.position.x < player.transform.position.x) //El enemigo ataca al jugador
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else 
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    animator.SetBool("walk", false);
                    atacando = true;
                }
            }
        }
    }

    public void FinalAni() //Se acaba la animación de atacar
    {
        animator.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue() //Aqui entra el hit de daño
    {
        hit.GetComponent<BoxCollider2D>().enabled = true;
        hitAudio.Play();

    }

    public void ColliderWeaponFalse() //Aqui sale el hit de daño
    {
        hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    //El enemigo recibe daño
    public void TakeDamage()
    {
        if (health < 1)
        {
            audio.Play();
            animator.SetTrigger("die");
        }
        else
        {
            health--;
            animator.SetTrigger("hit");
        }
    }
    public void RemoveObject()
    {
        Destroy(gameObject);
    }
}
