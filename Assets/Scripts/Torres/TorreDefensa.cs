using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreDefensa : TorreBase
{
    [Header("Atributos de Defensa")]
    [SerializeField] private int vidaExtra = 100;

    protected void Start()
    {
        vida += vidaExtra; // Aumenta la vida inicial
        costo = 10;
    }

    protected override void DestruirTorre()
    {
        base.DestruirTorre();
    }
}