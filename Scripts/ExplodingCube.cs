using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]

public class ExplodingCube : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ExplodingCube _cubePrefab;
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private Explosion _explosion;
    
    private Transform _transform;
    private MeshRenderer _meshRenderer;
    private float _scale = 1;
    private float _explosionForce = 10f;
    private float _childrenSpawnRadius = 0.5f;
    private float _nextGenScaleDivider = 2f;
    private float _divisionChance = 1f;
    private float _divisionChanceDivider = 2f;
    private int _minChildren = 2;
    private int _maxChildren = 6;
    private float _minColorValue = 0.25f;
    private float _maxColorValue = 0.9f;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        Rigidbody = GetComponent<Rigidbody>();
        Colorize();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Explode();
    }

    public void Init(float scale, float divisionChance)
    {
        _scale = scale;
        _transform.localScale = _scale * Vector3.one;
        _divisionChance = divisionChance;
    }

    private void Explode()
    {
        if (CalculateDivisionChance())
        {
            ExplodingCube[] spawnedCubes = SpawnCubes();
            _explosion.AddExplosionForce(Rigidbody.position, spawnedCubes, _explosionForce * _scale);
        }
        
        Destroy(gameObject);
    }

    private void Colorize()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = GetRandomColor();
    }
    
    private bool CalculateDivisionChance()
    {
        float chance = Random.Range(0f, 1f);
        
        return chance <= _divisionChance;
    }

    private ExplodingCube[] SpawnCubes()
    {
        int floatCompensation = 1;
        int cubesToSpawn = Random.Range(_minChildren, _maxChildren + floatCompensation);
        
        return _spawner.SpawnCubes(Rigidbody.position, _childrenSpawnRadius, cubesToSpawn, _scale / _nextGenScaleDivider, _divisionChance / _divisionChanceDivider);
    }
    
    private Color GetRandomColor()
    {
        return new Color(Random.Range(_minColorValue, _maxColorValue), Random.Range(_minColorValue, _maxColorValue), Random.Range(_minColorValue, _maxColorValue));
    }
}
