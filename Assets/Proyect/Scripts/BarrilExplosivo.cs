using UnityEngine;

public class BarrilExplosivo : MonoBehaviour
{
    [SerializeField]
    GameObject explosionParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            GameObject newExplosionParticles;
            
            newExplosionParticles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(newExplosionParticles, 2f);
            Destroy(gameObject);
        }
    }
}
