using UnityEngine;

public class DestroyRockParticles : MonoBehaviour
{
    private void Update()
    {
        Invoke("DestroyParticles", 3f);
    }
    private void DestroyParticles()
    {
        Destroy(this.gameObject);
    }
}
