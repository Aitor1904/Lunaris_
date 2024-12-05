using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    GameObject playerBullet;
    [SerializeField]
    Transform bulletOrigin;
    [SerializeField]
    float shotForce;
    [SerializeField]
    private float shotRate;
    float shotRateTime;
    public void Shot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(Time.time > shotRateTime)
            {
                GameObject newPlayerBullet;
                newPlayerBullet = Instantiate(playerBullet, bulletOrigin.position, bulletOrigin.rotation);
                newPlayerBullet.GetComponent<Rigidbody>().AddForce(bulletOrigin.right * shotForce);
                shotRateTime = Time.time + shotRate;
                Destroy(newPlayerBullet, 3f);
                Debug.Log("Shot");
            }  
        }
    }
}
