using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask mask;
    public float launchForce;
    public float timer;
    public float radius;
    public float explosionForce;
    public GameObject particles;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(
            Random.Range(-5f, 5f),
            Random.Range(-5f, 5f),
            Random.Range(-5f, 5f)
        );
        rb.AddTorque(randomTorque, ForceMode.Impulse);

        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(timer);

        if (particles != null)
        {
            Instantiate(particles, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, mask);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody nearbyRb = nearbyObject.GetComponent<Rigidbody>();
            if (nearbyRb != null)
            {
                nearbyRb.AddExplosionForce(explosionForce, transform.position, radius);
            }

            EnemyController enemy = nearbyObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Kill();
            }
        }

        Destroy(gameObject);
    }

}
