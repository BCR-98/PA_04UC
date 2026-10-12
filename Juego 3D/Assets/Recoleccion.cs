using UnityEngine;
using TMPro;

public class Recoleccion : MonoBehaviour
{
    [Header("Ajustes de Teclas")]
    public KeyCode teclaGuardar = KeyCode.E;   
    public KeyCode teclaSoltar = KeyCode.G;    
    public float distanciaInteraccion = 4.5f;  

    [Header("Ajustes de Lanzamiento (Ataque)")]
    public float fuerzaLanzamiento = 25f; 

    [Header("Interfaz de Usuario")]
    public TextMeshProUGUI textoContador; 

    [Header("Plantilla de la Piedra")]
    [Tooltip("Arrastra aquí el Prefab de tu piedra")]
    public GameObject prefabPiedra; 

    private int piedrasGuardadas = 0;
    private Camera camaraPrincipal;

    void Start()
    {
        
        camaraPrincipal = Camera.main;
        if (camaraPrincipal == null)
        {
            camaraPrincipal = GetComponentInChildren<Camera>();
        }

        ActualizarInterfaz();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(teclaGuardar))
        {
            IntentarGuardar();
        }

        
        if (Input.GetKeyDown(teclaSoltar))
        {
            IntentarSoltar();
        }

        
        if (Input.GetMouseButtonDown(0) && piedrasGuardadas > 0)
        {
            LanzarPiedraAtaque();
        }
    }

    void IntentarGuardar()
    {
        
        if (camaraPrincipal == null) return;

        Ray rayo = camaraPrincipal.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit golpe;

        if (Physics.Raycast(rayo, out golpe, distanciaInteraccion))
        {
            if (golpe.collider.CompareTag("Recurso"))
            {
                Recurso objetoRecurso = golpe.collider.GetComponent<Recurso>();

                if (objetoRecurso != null)
                {
                    objetoRecurso.Recolectar(); 
                    piedrasGuardadas += 1;      
                    ActualizarInterfaz();
                }
            }
        }
    }

    void IntentarSoltar()
    {
        if (piedrasGuardadas > 0 && camaraPrincipal != null)
        {
            Vector3 puntoAparicion = camaraPrincipal.transform.position + (camaraPrincipal.transform.forward * 2f);
            Instantiate(prefabPiedra, puntoAparicion, Quaternion.identity);

            piedrasGuardadas -= 1; 
            ActualizarInterfaz();
        }
    }
    
      void LanzarPiedraAtaque()
    {
        if (camaraPrincipal == null) return;

        piedrasGuardadas -= 1; 
        ActualizarInterfaz();   

       
        Vector3 puntoLanzamiento = camaraPrincipal.transform.position + (camaraPrincipal.transform.forward * 1.5f);
        GameObject piedraClonada = Instantiate(prefabPiedra, puntoLanzamiento, camaraPrincipal.transform.rotation);

        
        piedraClonada.AddComponent<PiedraArrojadiza>();

        
        Rigidbody rb = piedraClonada.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(camaraPrincipal.transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }
    }
   

    void ActualizarInterfaz()
    {
        if (textoContador != null)
        {
            textoContador.text = "Piedras: " + piedrasGuardadas;
        }
    }
}
