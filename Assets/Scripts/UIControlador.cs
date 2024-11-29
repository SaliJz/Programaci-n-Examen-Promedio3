using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControlador : MonoBehaviour
{
    //public static UIControlador Instancia { get; private set; }

    [Header("Referencias UI")]
    [SerializeField] private Image barraVida;
    [SerializeField] TextMeshProUGUI cantidadVida;
    [SerializeField] private Image barraResistencia;
    [SerializeField] TextMeshProUGUI cantidadResistencia;
    [SerializeField] private TextMeshProUGUI textoMonedas;

    private Jugador jugador;
    private VidaJugador vidaJugador;
    private ControladorJugador controladorJugador;

    private void Awake()
    {
        /*
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        */
    }

    private void Start()
    {
        // Asignamos las referencias a los scripts de Jugador y ControladorJugador
        jugador = GameObject.FindWithTag("Player").GetComponent<Jugador>();
        vidaJugador = GameObject.FindWithTag("TorrePrincipal").GetComponent<VidaJugador>();
        controladorJugador = GameObject.FindWithTag("Player").GetComponent<ControladorJugador>();
    }

    private void Update()
    {
        ActualizarHUD();
    }

    private void ActualizarHUD()
    {
        if (jugador != null)
        {
            // Actualizar las barras de vida y resistencia
            cantidadVida.text = $"{(int)vidaJugador.VidaActual()}";
            barraVida.fillAmount = (float)vidaJugador.VidaActual() / vidaJugador.VidaMaxima();

            cantidadResistencia.text = $"{(int)jugador.ResistenciaActual()}";
            barraResistencia.fillAmount = jugador.ResistenciaActual() / jugador.ResistenciaMaxima();
        }

        if (controladorJugador != null)
        {
            // Actualizar el texto de monedas
            textoMonedas.text = $"Monedas: {controladorJugador.ObtenerMonedas()}";
        }
    }
}
