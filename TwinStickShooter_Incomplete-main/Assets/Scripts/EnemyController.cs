using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float speedRotation;
    public float stoppingDistance;
    public float moveSpeed;
    public float timer;

    public Transform player;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        LookAtPlayer();
        MoveTowardsPlayer();
    }

    public void Kill()
    {
        StartCoroutine(KillDelay());
    }

    void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;


            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation);
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            float velocity;

            if (distance > stoppingDistance)
            {
                velocity = moveSpeed;
            }
            else
            {
                velocity = (distance / stoppingDistance) * moveSpeed;
            }

            Vector3 move = transform.forward * velocity * Time.deltaTime;
            transform.position += move;
        }
    }

    IEnumerator KillDelay()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        animator.enabled = false;

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(3f, transform.position, 2f, 1f, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);

    }
}
