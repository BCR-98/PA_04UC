using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA : MonoBehaviour
{
    private Transform jugador;      
    public float radioDeteccion = 20f;  
    public float distanciaAtaque = 2f;   
    public int danoAtaque = 15;
    public float velocidadAtaque = 1.5f;

    private NavMeshAgent agente;
    private Animator animadorAraña; 
    private float siguienteAtaque;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        
        
        animadorAraña = GetComponent<Animator>();
        if (animadorAraña == null)
        {
            animadorAraña = GetComponentInChildren<Animator>();
        }

        GameObject jugadorObj = GameObject.FindWithTag("Player");
        if (jugadorObj != null)
        {
            jugador = jugadorObj.transform;
        }
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);

        if (distancia <= radioDeteccion)
        {
            agente.SetDestination(jugador.position);

            if (distancia <= distanciaAtaque && Time.time >= siguienteAtaque)
            {
                AtacarJugador();
                siguienteAtaque = Time.time + velocidadAtaque;
            }
        }

        
        if (animadorAraña != null && agente != null)
        {
          
            bool estaCaminando = agente.velocity.magnitude > 0.1f;
            
            
            animadorAraña.SetBool("Caminando", estaCaminando);
        }
    }

    void AtacarJugador()
    {
        if (jugador != null)
        {
            VidaJugador vida = jugador.GetComponent<VidaJugador>();
            if (vida != null)
            {
                vida.RecibirDano(danoAtaque);
            }

             
            if (animadorAraña != null)
            {
                animadorAraña.SetTrigger("Atacar");
            }
        }
    }
}
