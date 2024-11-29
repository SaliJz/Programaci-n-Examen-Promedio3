using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorOleadas : MonoBehaviour
{
    [Header("Configuración de Enemigos")]
    [SerializeField] private GameObject[] enemigoPrefabs; // Array de prefabs de enemigos
    [SerializeField] private Transform spawnPoint;       // Punto de spawn de los enemigos
    [SerializeField] private ControladorJugador jugador;

    [Header("Configuración de Oleadas")]
    [SerializeField] private int enemigosIniciales = 1;     // Número de enemigos en la primera oleada
    [SerializeField] private int incrementoEnemigos = 2;    // Cantidad de enemigos adicionales por oleada
    [SerializeField] private float tiempoEntreOleadas = 60f; // Tiempo entre oleadas

    [Header("UI del Temporizador")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Color colorAdvertencia = Color.yellow;
    [SerializeField] private Color colorCritico = Color.red;

    private int enemigosPorOleada; // Número actual de enemigos por oleada
    private int enemigosDerrotados = 0;
    private int oleadaActual = 0;

    private bool eventActive = false;
    private float timer;

    private void Start()
    {
        enemigosPorOleada = enemigosIniciales; // Inicializar la primera oleada
        StartCoroutine(IniciarOleadas());
    }

    private void Update()
    {
        if (eventActive)
        {
            ActualizarTemporizador();
        }
    }

    private IEnumerator IniciarOleadas()
    {
        while (true) // Bucle infinito para oleadas
        {
            oleadaActual++;
            eventActive = true;
            timer = tiempoEntreOleadas;
            timerText.gameObject.SetActive(true);

            Debug.Log($"Oleada {oleadaActual}: Generando {enemigosPorOleada} enemigos.");

            // Generar enemigos de la oleada actual
            for (int i = 0; i < enemigosPorOleada; i++)
            {
                SpawnEnemigo();
                yield return new WaitForSeconds(1f); // Separación entre spawns
            }

            // Esperar a que la oleada termine o el tiempo se agote
            yield return new WaitUntil(() => enemigosDerrotados >= enemigosPorOleada || timer <= 0f);

            // Preparar la siguiente oleada
            eventActive = false;
            enemigosDerrotados = 0;
            timerText.gameObject.SetActive(false);

            // Incrementar el número de enemigos para la siguiente oleada
            enemigosPorOleada += incrementoEnemigos;
        }
    }

    private void SpawnEnemigo()
    {
        GameObject enemigoPrefab = enemigoPrefabs[Random.Range(0, enemigoPrefabs.Length)];
        Instantiate(enemigoPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void EnemigoDerrotado()
    {
        enemigosDerrotados++;
    }

    private void ActualizarTemporizador()
    {
        timer -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";

        // Cambiar el color según el tiempo restante
        if (timer <= tiempoEntreOleadas / 2 && timer > 10)
        {
            timerText.color = colorAdvertencia;
        }
        else if (timer <= 10)
        {
            timerText.color = colorCritico;
        }

        // Ocultar el texto si el tiempo llega a 0
        if (timer <= 0)
        {
            timerText.gameObject.SetActive(false);
        }
    }
}