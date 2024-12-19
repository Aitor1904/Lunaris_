using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public GameObject pointA;
    public GameObject pointB;
    public Rigidbody rb;
    private Transform currentPoint;
    public float speed;
    [SerializeField]
    LayerMask whatIsPlayer;

    [Header("States")]
    [SerializeField]
    float sightRange;
    [SerializeField]
    float attackRange;
    [SerializeField]
    bool playerInSightRange;
    [SerializeField]
    bool playerInAttackrange;
    [SerializeField]
    Transform player;
    RaycastHit hit;
    [SerializeField]
    float sightRayCastLenth;
    [SerializeField]
    float attackRayCastLenth;
    [SerializeField]
    bool isLookingRight = true;

    [Header("Attack")]
    [SerializeField]
    Transform enemyBulletOrigin;
    [SerializeField]
    GameObject enemyBullet;
    [SerializeField]
    float shotForce;
    float shotRate;
    float shotRateTime;

    [Header("Animator")]
    [SerializeField]
    Animator enemyAnimator;
    private void Start()
    {
        currentPoint = pointB.transform;
    }
    private void Update()
    {
        if (!IsPlayerInChaseRange() && !IsPlayerInAttackRange()) Patrol();
        if (IsPlayerInChaseRange() && !IsPlayerInAttackRange()) ChasePlayer();
        if (IsPlayerInChaseRange() && IsPlayerInAttackRange()) AttackPlayer();
    }

    bool IsPlayerInChaseRange()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(LookDirection()), out hit, sightRayCastLenth, whatIsPlayer);
        
    }
    
    bool IsPlayerInAttackRange()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(LookDirection()), out hit, attackRayCastLenth, whatIsPlayer);
    }

    Vector3 LookDirection()
    {
        return isLookingRight ? Vector3.right : Vector3.left;
    }
    void Patrol()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
            Flip();
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
            Flip();
        }
    }
    void ChasePlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        //MOve el enemigo en la dirección del directionToPlayer
        rb.AddForce(directionToPlayer);
        
    }
    void AttackPlayer()
    {
        if(Time.time > shotRateTime)
        {
            GameObject newEnemyBullet;
            newEnemyBullet = Instantiate(enemyBullet, enemyBulletOrigin.position, enemyBulletOrigin.rotation);
            newEnemyBullet.GetComponent<Rigidbody>().AddForce(enemyBulletOrigin.right * shotForce);
            shotRateTime = Time.time + shotRate;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.color = Color.red;
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * sightRayCastLenth, Color.yellow);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * attackRayCastLenth, Color.red);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Debug.DrawRay(transform.position, transform.TransformDirection(LookDirection()) * attackRayCastLenth, Color.red);
    }
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        isLookingRight = !isLookingRight;
    }
}
