using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Atributos del Enemigo")]
    [SerializeField] private int daño = 10;
    [SerializeField] private float rangoAtaque = 5f;
    [SerializeField] private float tiempoEntreAtaques = 1.5f;

    private float tiempoUltimoAtaque;
    private Transform objetivoActual;

    [SerializeField] private float velocidad = 10f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tiempoUltimoAtaque = -tiempoEntreAtaques; // Permitir un ataque inmediato al inicio si está en rango
    }

    private void Update()
    {
        // Actualizar objetivo si es necesario
        if (objetivoActual == null || !EsObjetivoValido(objetivoActual))
        {
            BuscarObjetivo();
        }

        // Mover o atacar según la distancia
        if (objetivoActual != null)
        {
            float distancia = Vector3.Distance(transform.position, objetivoActual.position);

            if (distancia > rangoAtaque)
            {
                Mover();
            }
            else
            {
                AtacarObjetivo();
            }
        }
        else
        {
            rb.velocity = Vector3.zero; // Detenerse si no hay objetivo
        }
    }

    private void Mover()
    {
        if (objetivoActual == null) return;

        Vector3 direccion = (objetivoActual.position - transform.position).normalized;
        rb.velocity = direccion * velocidad;
    }

    private void BuscarObjetivo()
    {
        Collider[] objetosEnRango = Physics.OverlapSphere(transform.position, rangoAtaque);
        Transform mejorObjetivo = null;
        int mejorPrioridad = int.MaxValue;

        foreach (Collider obj in objetosEnRango)
        {
            int prioridad = ObtenerPrioridad(obj.gameObject);

            // Buscar el objetivo con la menor prioridad
            if (prioridad < mejorPrioridad && EsObjetivoValido(obj.transform))
            {
                mejorPrioridad = prioridad;
                mejorObjetivo = obj.transform;
            }
        }

        objetivoActual = mejorObjetivo;
    }

    private int ObtenerPrioridad(GameObject obj)
    {
        if (obj.CompareTag("TorrePrincipal"))
            return 1; // Mayor prioridad: Torre Principal
        else if (obj.CompareTag("TorreDefensa"))
            return 2; // Segunda prioridad: Torre de Defensa
        else if (obj.CompareTag("TorreAlmacenamiento"))
            return 3; // Tercera prioridad: Torre de Almacenamiento
        else if (obj.CompareTag("TorreAtaque"))
            return 4; // Última prioridad: Torre de Ataque
        else
            return int.MaxValue; // No relevante
    }

    private bool EsObjetivoValido(Transform objetivo)
    {
        if (objetivo == null) return false;

        VidaJugador vida = objetivo.GetComponent<VidaJugador>();
        return vida != null && vida.VidaActual() > 0; // Solo objetivos vivos
    }

    private void AtacarObjetivo()
    {
        if (Time.time >= tiempoUltimoAtaque + tiempoEntreAtaques)
        {
            VidaBase vidaObjetivo = objetivoActual?.GetComponent<VidaBase>();
            if (vidaObjetivo != null)
            {
                vidaObjetivo.RecibirDaño(daño);
                tiempoUltimoAtaque = Time.time;
            }
        }
    }
}