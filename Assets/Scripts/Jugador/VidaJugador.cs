using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJugador : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] private int vidaActual;
    [SerializeField] private string escenaMuerte;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(escenaMuerte);
        Destroy(gameObject);
    }

    public int VidaActual()
    {
        return vidaActual;
    }
    public int VidaMaxima()
    {
        return vidaMaxima;
    }
}
