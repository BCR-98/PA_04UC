using UnityEngine;
using UnityEngine.UI; // Necesario para controlar el Slider y la Image
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class VidaJugador : MonoBehaviour
{
    [Header("Ajustes de Salud")]
    public int vidaMaxima = 100;
    private int vidaActual;

    [Header("Componentes de Interfaz")]
    public Slider barraVidaUI;          
    public Image imagenRellenoVida;     // ¡NUEVO! Aquí arrastraremos el objeto "Fill" para cambiar su color
    public GameObject panelGameOverUI;  

    [Header("Ajustes de Color Dinámico")]
    public Gradient gradienteColor;     // ¡NUEVO! El degradado de Verde a Rojo

    private bool estaMuerto = false;

    void Start()
    {
        vidaActual = vidaMaxima;

        if (barraVidaUI != null)
        {
            barraVidaUI.maxValue = vidaMaxima;
            barraVidaUI.value = vidaMaxima;
        }

        // Al empezar, pintamos la barra con el color máximo del gradiente (Verde)
        ActualizarColorBarra();

        if (panelGameOverUI != null)
        {
            panelGameOverUI.SetActive(false);
        }
    }

    void Update()
    {
        if (estaMuerto && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void RecibirDano(int cantidad)
    {
        if (estaMuerto) return;

        vidaActual -= cantidad;
        Debug.Log("¡Te han golpeado! Vida restante: " + vidaActual);

        if (barraVidaUI != null)
        {
            barraVidaUI.value = vidaActual;
        }

        // ¡NUEVO! Cada vez que recibes daño, recalculamos el color de la barra
        ActualizarColorBarra();

        if (vidaActual <= 0)
        {
            ActivarMuerte();
        }
    }

    // ¡NUEVA FUNCIÓN! Se encarga de evaluar el porcentaje de vida y aplicar el color exacto
    void ActualizarColorBarra()
    {
        if (imagenRellenoVida != null && barraVidaUI != null)
        {
            // Calculamos un porcentaje entre 0f (0% vida) y 1f (100% vida)
            float porcentajeVida = (float)vidaActual / vidaMaxima;
            
            // Evaluamos el gradiente basándonos en ese porcentaje y pintamos la imagen
            imagenRellenoVida.color = gradienteColor.Evaluate(porcentajeVida);
        }
    }

    void ActivarMuerte()
    {
        estaMuerto = true;
        Debug.Log("¡Has muerto!");

        if (panelGameOverUI != null)
        {
            panelGameOverUI.SetActive(true);
        }

        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
