using UnityEngine;

public class Recurso : MonoBehaviour
{
    [Header("Configuración del Recurso")]
    public string nombreRecurso = "Piedra"; 

    public void Recolectar()
    {
        Debug.Log("¡Has recogido: " + nombreRecurso + "!");
        Destroy(gameObject); 
    }
}
