using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionForce = 1000f; // ������
    [SerializeField] private float explosionRadius = 15f; // �������a

    public void Blast()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            // FixedJoint���폜����
            FixedJoint[] allJoints = FindObjectsOfType<FixedJoint>();
            foreach (FixedJoint joint in allJoints)
            {
                Destroy(joint);
            }

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 3.0F);
            }
        }
    }
}