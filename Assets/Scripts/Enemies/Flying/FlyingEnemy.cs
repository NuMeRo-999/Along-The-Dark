using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private Vector2 direction;
    private GameObject player;
    public bool atacando;
    private Vector3 puntoInicial;
    public Health health;
    [SerializeField] float enemyHealth;
    private Animator animator;
    public AudioSource audio;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        puntoInicial = new Vector3(transform.position.x, transform.position.y);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(player == null) { // En caso de que no encuentre nada dará error
            return;
        }

        if (atacando) {
            seguir(); // Sigue al jugador con el condicional
        }else {
            irAPuntoInicial(); // Vuelve al punto inicial
        }
    }

    private void seguir()
    {
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        Vector2 cursorPos = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, cursorPos, speed * Time.deltaTime);
    }
    private void irAPuntoInicial()
    {
        direction = puntoInicial - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        Vector3 cursorPos = puntoInicial;
        transform.position = Vector2.MoveTowards(transform.position, cursorPos, speed * Time.deltaTime);
    }

    //Dañar al jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            health.takeDamage();
        }
    }

    //El enemigo recibe daño
    public void TakeDamage()
    {
        if (enemyHealth < 1)
        {
            audio.Play();
            animator.SetTrigger("die");
        }
        else
        {
            enemyHealth--;
            animator.SetTrigger("hit");
        }
    }

    public void RemoveObject() //Metodo que se le aplica al final del la animación del morir con un evento
    {
        Destroy(gameObject);
    }
}
