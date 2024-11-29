using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private Transform objetivo;
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private int daño = 10;

    public void ConfigurarObjetivo(Transform objetivo)
    {
        this.objetivo = objetivo;
    }

    private void Update()
    {
        if (objetivo == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direccion = objetivo.position - transform.position;
        float distanciaPorFrame = velocidad * Time.deltaTime;

        if (direccion.magnitude <= distanciaPorFrame)
        {
            ImpactarObjetivo();
            return;
        }

        transform.Translate(direccion.normalized * distanciaPorFrame, Space.World);
    }

    private void ImpactarObjetivo()
    {
        VidaEnemigo vidaEnemigo = objetivo.GetComponent<VidaEnemigo>();
        if (vidaEnemigo != null)
        {
            vidaEnemigo.RecibirDaño(daño);
        }
        Destroy(gameObject);
    }

   //private void OnCollisionEnter(Collision collision)
   //{
   //    if (!collision.gameObject.CompareTag("Enemigo"))
   //    {
   //        ImpactarObjetivo();
   //    }
   //}
}
