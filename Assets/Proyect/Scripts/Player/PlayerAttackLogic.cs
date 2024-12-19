using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackLogic : MonoBehaviour
{
    [Header("Shot")]
    [SerializeField]
    GameObject playerBullet;
    [SerializeField]
    Transform bulletOrigin;
    [SerializeField]
    float shotForce;
    [SerializeField]
    private float shotRate;
    float shotRateTime;
    private bool isShooting = false;
    [SerializeField]
    ParticleSystem shotParticles;

  

    [Header("SolveLogic")]
    [SerializeField]
    WeaponChange weaponChange;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    LayerMask whatIsEnemy;
    RaycastHit hit;
    [SerializeField]
    float raycastLenth;
    [SerializeField]
    Animator playerAnimator;
    [SerializeField]
    Movement movement;

    private void Update()
    {
        if (GameManager.Instance.gunAmmo == 0)
        {
            weaponChange.canShoot = false;
        }

        if (isShooting && weaponChange.canShoot)
        {
            ContinuousShot();
        }
    }

    /*public void Shot(InputAction.CallbackContext context)
    {
        if (context.performed && weaponChange.canShoot == true)
        {
            if(Time.time > shotRateTime && GameManager.Instance.gunAmmo > 0)
            {
                GameManager.Instance.gunAmmo--;
                GameObject newPlayerBullet;
                newPlayerBullet = Instantiate(playerBullet, bulletOrigin.position, bulletOrigin.rotation);
                newPlayerBullet.GetComponent<Rigidbody>().AddForce(bulletOrigin.forward * shotForce);
                shotRateTime = Time.time + shotRate;
                Destroy(newPlayerBullet, 3f);
                Debug.Log("Shot");
                shotParticles.Play();
            }  
        }
    }*/

    public void Shot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isShooting = true;
        }

        if (context.canceled)
        {
            isShooting = false;
        }
    }

    private void ContinuousShot()
    {
        if (Time.time > shotRateTime && GameManager.Instance.gunAmmo > 0)
        {
            GameManager.Instance.gunAmmo--;
            GameObject newPlayerBullet;
            newPlayerBullet = Instantiate(playerBullet, bulletOrigin.position, bulletOrigin.rotation);
            newPlayerBullet.GetComponent<Rigidbody>().AddForce(bulletOrigin.forward * shotForce);
            shotRateTime = Time.time + shotRate;
            Destroy(newPlayerBullet, 3f);
            Debug.Log("Shot");
        }
    }

    public void SolveLogic(InputAction.CallbackContext context)
    {
        if (context.performed && weaponChange.canShoot == false)
        {
            if(movement.isLookingRight == true)
            {
                Debug.Log("right");
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, raycastLenth, whatIsGround))
                {
                    playerAnimator.SetTrigger("SolveGroundAttack");
                }
                else
                {
                    playerAnimator.SetTrigger("SolveEnemyAttack");
                }
            }
            else if(movement.isLookingRight == false)
            {
                Debug.Log("left");
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, raycastLenth, whatIsGround))
                {
                    playerAnimator.SetTrigger("SolveGroundAttack");
                }
                else
                {
                    playerAnimator.SetTrigger("SolveEnemyAttack");
                }
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * raycastLenth, Color.red);
    }
}
