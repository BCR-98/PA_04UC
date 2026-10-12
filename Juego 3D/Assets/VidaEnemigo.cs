using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int vida = 30; 
    private Animator animadorAraña;
    private bool estaMuerto = false; 

    void Start()
    {
        
        animadorAraña = GetComponentInChildren<Animator>();
    }

    public void RecibirDano(int cantidad)
    {
        if (estaMuerto) return; 

        vida -= cantidad;
        Debug.Log("El enemigo recibió daño. Vida restante: " + vida);

        if (vida <= 0)
        {
            MorirEnemigo();
        }
    }

    void MorirEnemigo()
    {
        estaMuerto = true;
        Debug.Log("Enemigo derrotado");

        
        if (animadorAraña != null)
        {
            animadorAraña.SetTrigger("Morir");
        }

        
        UnityEngine.AI.NavMeshAgent agente = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agente != null)
        {
            agente.isStopped = true; 
        }

        
        Collider colisionador = GetComponent<Collider>();
        if (colisionador != null) colisionador.enabled = false;

        // 4. Espera 2.5 segundos (tiempo de la animación) y luego borra el objeto de la escena
        Destroy(gameObject, 2.5f); 
    }
}
