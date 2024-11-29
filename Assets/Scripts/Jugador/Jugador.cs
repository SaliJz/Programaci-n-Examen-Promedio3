using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    [Header("Resistencia")]
    [SerializeField] private float resistenciaMaxima = 100f;
    [SerializeField] private float resistenciaActual;
    [SerializeField] private float resistenciaRecuperacion = 10f;
    [SerializeField] private float resistenciaCorrer = 10f;

    [Header("Movimiento")]
    [SerializeField] private float velocidadCaminar = 10f;
    [SerializeField] private float velocidadCorrer = 20f;
    [SerializeField] private float fuerzaSalto = 10f;
    [SerializeField] private float escalaAgachar = 0.4f;
    private bool enSuelo = false;

    [SerializeField] private Vector2 sensibilidad;
    private Transform camaraJugador;
    private Rigidbody rb;

    [Header("Muerte")]
    [SerializeField] private string escenaMuerte;

    void Awake()
    {
        resistenciaActual = resistenciaMaxima;
        rb = GetComponent<Rigidbody>();

        camaraJugador = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movimiento();
        Camara();
        RecuperarResistencia();
        Saltar();
        Agachar();
    }

    void Movimiento()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        Vector3 velocidad = Vector3.zero;

        if (movimientoHorizontal != 0 || movimientoVertical != 0)
        {
            Vector3 direccion = (transform.forward * movimientoVertical + transform.right * movimientoHorizontal);
            // Correr o caminar segÚn la resistencia
            if (Input.GetKey(KeyCode.LeftShift) && resistenciaActual > 0)
            {
                velocidad = direccion * velocidadCorrer;
                resistenciaActual -= resistenciaCorrer * Time.deltaTime;
            }
            else
            {
                velocidad = direccion * velocidadCaminar;
            }
        }

        velocidad.y = rb.velocity.y;
        rb.velocity = velocidad;
    }

    void Saltar()
    {
        if (Input.GetButtonDown("Jump") && enSuelo == true)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    void Agachar()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {
            transform.localScale = new Vector3(transform.localScale.x, escalaAgachar, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }
    }

    void Camara()
    {
        float movimientoHorizontal = Input.GetAxis("Mouse X");
        float movimientoVertical = Input.GetAxis("Mouse Y");

        if (movimientoHorizontal != 0)
        {
            transform.Rotate(Vector3.up, movimientoHorizontal * sensibilidad.x);
        }

        if (movimientoVertical != 0)
        {
            float angulo = (camaraJugador.localEulerAngles.x - movimientoVertical * sensibilidad.y + 360) % 360;

            if (angulo > 180)
            {
                angulo -= 360;
            }
            angulo = Mathf.Clamp(angulo, -80, 80); // Limitar el ángulo de rotación vertical
            camaraJugador.localEulerAngles = Vector3.right * angulo;
        }
    }

    void RecuperarResistencia()
    {
        if (resistenciaActual < resistenciaMaxima && rb.velocity.magnitude == 0)
        {
            resistenciaActual += resistenciaRecuperacion * Time.deltaTime;
        }

        else if (resistenciaActual > resistenciaMaxima)
        {
            resistenciaActual = resistenciaMaxima;
        }
    }

    public float ResistenciaActual()
    {
        return resistenciaActual;
    }

    public float ResistenciaMaxima()
    {
        return resistenciaMaxima;
    }

    public bool EstaEnSuelo()
    {
        return enSuelo;
    }

    public bool EstaEnAire()
    {
        return !EstaEnSuelo();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }
}