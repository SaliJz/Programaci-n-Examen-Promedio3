using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalObjetivo : MonoBehaviour
{
    private ControladorDerrota controladorDerrota;

    private void Start()
    {
        controladorDerrota = GameObject.FindObjectOfType<ControladorDerrota>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            controladorDerrota.EnemigoAlcanzoObjetivo();
            Destroy(other.gameObject); // El enemigo se elimina al llegar al final
        }
    }
}
