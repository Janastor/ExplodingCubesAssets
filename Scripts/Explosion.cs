using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void AddExplosionForce(Vector3 explosionCenter, ExplodingCube[] cubes, float explosionForce)
    {
        Rigidbody[] rigidbodies = GetRigidBodies(cubes);

        foreach (Rigidbody rb in rigidbodies)
        {
            Vector3 explosionDirection = CalculateExplosionDirection(explosionCenter, rb.position);
            rb.AddForce(explosionForce * explosionDirection, ForceMode.Impulse);
        }
    }
    
    private Rigidbody[] GetRigidBodies(ExplodingCube[] cubes)
    {
        Rigidbody[] rigidbodies = new Rigidbody[cubes.Length];

        for (int i = 0; i < cubes.Length; i++)
            rigidbodies[i] = cubes[i].GetComponent<Rigidbody>();

        return rigidbodies;
    }
    
    private Vector3 CalculateExplosionDirection(Vector3 initialPosition, Vector3 position)
    {
        return (position - initialPosition).normalized;
    }
}
