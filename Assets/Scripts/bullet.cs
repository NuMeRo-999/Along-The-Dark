using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            if (collision.collider.gameObject.TryGetComponent<GroundEnemy>(out GroundEnemy enemyComponent)) //busca enemigo terrestre
            {

                enemyComponent.TakeDamage();//Daña al enemigo
            }

            if (collision.collider.gameObject.TryGetComponent<FlyingEnemy>(out FlyingEnemy enemyComponent2)) //busca enemigo volador
            {

                enemyComponent2.TakeDamage();//Daña al enemigo
            }
            Destroy(gameObject);//ha chocado con otra cosa y se destruye la bala
        }
    }
}
