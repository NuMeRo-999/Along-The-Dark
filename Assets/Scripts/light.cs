using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D luz;
    public float tiempoMinParpadeo = 0.1f;
    public float tiempoMaxParpadeo = 0.5f;

    void Start()
    {
        // Obtener el componente Light2D si no lo has asignado en el Inspector
        if (luz == null)
        {
            luz = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }

        // Iniciar la rutina de parpadeo
        StartCoroutine(ParpadearLuz());
    }

    IEnumerator ParpadearLuz()
    {
        while (true)
        {
            // Cambiar el estado de la luz (encendido/apagado)
            luz.enabled = !luz.enabled;

            // Esperar un tiempo aleatorio entre tiempoMinParpadeo y tiempoMaxParpadeo
            float tiempoEspera = Random.Range(tiempoMinParpadeo, tiempoMaxParpadeo);
            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
