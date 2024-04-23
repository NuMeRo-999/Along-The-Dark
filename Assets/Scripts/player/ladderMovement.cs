using UnityEngine;

public class ladderMovement : MonoBehaviour
{
    private float vertical;
    public float ladderVelocity = 1f;
    private bool escalando;
    private bool tocandoEscalera;
    private Rigidbody2D rb;
    private Animator animator;
    public AudioSource audio;
    public GameObject laddersText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        animator = rb.GetComponent<Animator>();

        if(tocandoEscalera && Mathf.Abs(vertical) > 0f){
            escalando = true;

            if (escalando)
            {
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
                /*animator.SetBool("upLadder", true);*/
            }
            else
            {
                audio.Stop();
                /*animator.SetBool("upLadder", false);*/
            }
        }        
    }
    private void FixedUpdate()
    {
        if (escalando){
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * ladderVelocity);
        }else{
            rb.gravityScale = 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera")){
            tocandoEscalera = true;
            laddersText.active = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera")){
            tocandoEscalera = false;
            laddersText.active = false;
            escalando = false;
        }
    }

}
