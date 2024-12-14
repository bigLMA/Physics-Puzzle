using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class Mutant : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 12f;
    [SerializeField]
    private float injuredSpeed = 8f;
    [SerializeField]
    private float fallDuration = 3f;
    [SerializeField]
    private float attackRange = 1.9f;
    [SerializeField]
    private float delayBetweenAttacks = 2.2f;
    [SerializeField]
    private float impactForce = 8.5f;

    [SerializeField]
    private PlayerController playerRef;
    [SerializeField]
    [Tooltip("Point from which mutant will start on level and try to reach it if player is not ")]
    private Transform startPosition;
    [SerializeField]
    [Tooltip("Mutant will push player on attack in the direction of this position")]
    private Transform oppositePosition;
    [SerializeField]
    [Tooltip("If player enters zone, mutant will try to chase and attack player.\nIf player is not inside zone, mutant will return to start position")]
    private GameObject chaseZone;
    

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private int injuredIdx;
    private int attackIdx;

    private bool onGround = false;
    private bool canAttack = true;
    private bool playerInChaseZone = false;

    private float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = maxSpeed;
        currentSpeed = maxSpeed;
        navMeshAgent.isStopped = true;

        injuredIdx = animator.GetLayerIndex("Injured Layer");
        attackIdx = animator.GetLayerIndex("Attack Layer");

        Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());
       
    }

    // Update is called once per frame
    void Update()
    {
        onGround = animator.GetBool("Fall");

        if (!onGround && playerInChaseZone)
        {
            if ((transform.position - playerRef.transform.position).sqrMagnitude > attackRange * attackRange)
            {
                navMeshAgent.isStopped = false;
                ChasePlayer();
            }
            else
            {
                if (canAttack) Attack();
            }
        }
        else if (!onGround && !playerInChaseZone)
        {
            ReturnToStartPosition();

            if ((transform.position - startPosition.position).sqrMagnitude < 1.2) navMeshAgent.isStopped = true;
        }
        else if (onGround) navMeshAgent.isStopped = true;

        // Update animator speed
        animator.SetFloat("Speed", navMeshAgent.speed);
        animator.SetBool("Walks", !navMeshAgent.isStopped);
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(playerRef.transform.position);
    }

    public void StartChase() => playerInChaseZone = true;

    public void StopChase()=>playerInChaseZone = false; 

    private void ReturnToStartPosition()
    {
        navMeshAgent.SetDestination(startPosition.position);
    }

    public void Injure()
    {
        animator.SetLayerWeight(injuredIdx, 1f);
        navMeshAgent.speed = injuredSpeed;
        currentSpeed = injuredSpeed;
    }

    private void Attack()
    {
        playerRef.ConstainedMovement = true;
        navMeshAgent.isStopped = true;
        animator.SetLayerWeight(attackIdx, 1f);
        animator.SetTrigger("Attack");
        canAttack = false;
    }

    /// <summary>
    /// Push player out of the zone
    /// </summary>
    public void AttackImpact()
    {
        var playerRb = playerRef.GetComponent<Rigidbody>();
        Vector3 forceDirection = (oppositePosition.transform.position - startPosition.position).normalized * impactForce;
        forceDirection.y =0f;
        print(forceDirection);
        playerRef.ConstainedMovement = false;
        playerRb.AddForce(playerRef.transform.up * 5.5f, ForceMode.VelocityChange);
        playerRb.AddForce(forceDirection, ForceMode.Impulse);
    }

    public void StopAttack()
    {
        navMeshAgent.isStopped = false;
        animator.SetLayerWeight(attackIdx, 0f);
        StartCoroutine(AttackCoroutine());
        animator.ResetTrigger("Attack");
        print("STOP");
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(delayBetweenAttacks);
        canAttack = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ice")
        {
            Fall();
        }
    }

    public void Fall()
    {
        animator.SetBool("Fall", true);
        Injure();
    }

    public void StopFall()
    {
        StartCoroutine(FallCoroutine());
    }

    private IEnumerator FallCoroutine()
    {
        yield return new WaitForSeconds(fallDuration);
        animator.SetBool("Fall", false);
    }
}
