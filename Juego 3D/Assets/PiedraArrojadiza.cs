using UnityEngine;

public class PiedraArrojadiza : MonoBehaviour
{
    public int danoPiedra = 10;

    void OnCollisionEnter(Collision collision)
    {
        VidaEnemigo enemigo = collision.gameObject.GetComponent<VidaEnemigo>();
        if (enemigo != null)
        {
            enemigo.RecibirDano(danoPiedra);
            Destroy(gameObject); 
        }
        
        
    }
}
