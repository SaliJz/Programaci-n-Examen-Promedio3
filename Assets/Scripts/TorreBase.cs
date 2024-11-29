using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreBase : MonoBehaviour
{
    [Header("Atributos Comunes de la Torre")]
    [SerializeField] protected int vida;
    [SerializeField] protected float rango = 5f;
    [SerializeField] protected int costo;

    private VidaBase vidaBase;

    private void Start()
    {
        vidaBase = GameObject.FindWithTag("VidaBase").GetComponent<VidaBase>();
        vida = vidaBase.VidaActual();
    }

    public virtual void RecibirDaño(int cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            DestruirTorre();
        }
    }

    protected virtual void DestruirTorre()
    {
        Destroy(gameObject);
    }

    public int ObtenerCosto()
    {
        return costo;
    }
}
