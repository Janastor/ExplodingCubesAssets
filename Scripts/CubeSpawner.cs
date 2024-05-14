using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplodingCube _cubePrefab;
    
    public ExplodingCube[] SpawnCubes(Vector3 initialPosition, float radius, int count, float scale, float divisionChance)
    {
        ExplodingCube[] spawnedCubes = new ExplodingCube[count];
        
        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;
            Vector3 position = CalculateChildPosition(initialPosition, angle, radius * scale);
            
            spawnedCubes[i] = Instantiate(_cubePrefab, position, Quaternion.identity);
            spawnedCubes[i].Init(scale, divisionChance);
        }
        
        return spawnedCubes;
    }
    
    private Vector3 CalculateChildPosition(Vector3 initialPosition, float angle, float radius)
    {
        return initialPosition + (new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius));
    }
}
