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
    private float shotRate;
    float shotRateTime;
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

    private void Update()
    {
        if(GameManager.Instance.gunAmmo == 0)
        {
            weaponChange.canShoot = false;
        }
    }
    public void Shot(InputAction.CallbackContext context)
    {
        if (context.performed && weaponChange.canShoot == true)
        {
            if(Time.time > shotRateTime && GameManager.Instance.gunAmmo > 0)
            {
                GameManager.Instance.gunAmmo--;
                GameObject newPlayerBullet;
                newPlayerBullet = Instantiate(playerBullet, bulletOrigin.position, bulletOrigin.rotation);
                newPlayerBullet.GetComponent<Rigidbody>().AddForce(bulletOrigin.right * shotForce);
                shotRateTime = Time.time + shotRate;
                Destroy(newPlayerBullet, 3f);
                Debug.Log("Shot");
                shotParticles.Play();
            }  
        }
    }
    public void SolveLogic(InputAction.CallbackContext context)
    {
        if (context.performed && weaponChange.canShoot == false)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, raycastLenth, whatIsGround))
            {
                playerAnimator.SetTrigger("SolveGroundAttack");
            }
            else
            {
                playerAnimator.SetTrigger("SolveEnemyAttack");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * raycastLenth, Color.red);
    }
}
