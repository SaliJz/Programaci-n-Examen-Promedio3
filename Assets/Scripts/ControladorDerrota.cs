using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDerrota : MonoBehaviour
{
    [SerializeField] private int maximoEnemigosPermitidos = 10;
    private int enemigosEnObjetivo = 0;
    [SerializeField] private string escenaDerrota;

    public void EnemigoAlcanzoObjetivo()
    {
        enemigosEnObjetivo++;
        if (enemigosEnObjetivo >= maximoEnemigosPermitidos)
        {
            SceneManager.LoadScene(escenaDerrota);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemigo"))
        {
            EnemigoAlcanzoObjetivo();
        }
    }
}