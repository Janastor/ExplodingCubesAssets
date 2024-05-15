using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void AddExplosionForce(Vector3 explosionCenter, ExplodingCube[] cubes, float explosionForce)
    {
        foreach (ExplodingCube cube in cubes)
        {
            Vector3 explosionDirection = CalculateExplosionDirection(explosionCenter, cube.Rigidbody.position);
            cube.Rigidbody.AddForce(explosionForce * explosionDirection, ForceMode.Impulse);
        }
    }
    
    private Vector3 CalculateExplosionDirection(Vector3 initialPosition, Vector3 position)
    {
        return (position - initialPosition).normalized;
    }
}
