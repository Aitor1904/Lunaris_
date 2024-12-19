using UnityEngine;

public class Tuneladora : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float speed;
    private void Update()
    {
        rb.linearVelocity = new Vector2(speed, 0);
    }
    //Cuando colisione con el enemigo enemigo muere
}
