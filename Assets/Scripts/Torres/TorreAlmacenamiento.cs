using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreAlmacenamiento : TorreBase
{
    [Header("Atributos de Almacenamiento")]
    [SerializeField] private int incrementoLimiteTorres = 2;
    private ControladorJugador controladorJugador;

   private void Start()
   {
        costo = 25;
        vida = 100;

       controladorJugador = GameObject.FindWithTag("Player").GetComponent<ControladorJugador>();
       
        if (controladorJugador != null)
       {
           controladorJugador.AumentarLimiteTorres(incrementoLimiteTorres);
       }
   }
   
   protected override void DestruirTorre()
   {
       if (controladorJugador != null)
       {
           controladorJugador.AumentarLimiteTorres(-incrementoLimiteTorres);
       }
       base.DestruirTorre();
   }
}