using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] private int vidaActual;
    [SerializeField] private int monedasPorDerrota = 10;
    private ControladorJugador controladorJugador;

    private void Start()
    {
        vidaActual = vidaMaxima;
        controladorJugador = GameObject.FindWithTag("Player").GetComponent<ControladorJugador>();
    }

    public virtual void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        if (controladorJugador != null)
        {
            controladorJugador.AñadirMonedas(monedasPorDerrota);
        }
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