using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplodingCube : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ExplodingCube _cubePrefab;
    
    private Transform _transform;
    private Rigidbody _rb;
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
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Explode();
    }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        Colorize();
    }

    private void Init(float scale, float divisionChance, Vector3 force)
    {
        _scale = scale;
        _transform.localScale = _scale * Vector3.one;
        _divisionChance = divisionChance;
        _rb.AddForce(force, ForceMode.Impulse);
    }

    private void Explode()
    {
        if (CalculateDivisionChance())
            GenerateChildren();
        
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
        print(chance);
        
        return chance <= _divisionChance;
    }

    private void GenerateChildren()
    {
        int floatCompensation = 1;
        ExplodingCube spawned;
        int childrenCount = Random.Range(_minChildren, _maxChildren + floatCompensation);

        for (int i = 0; i < childrenCount; i++)
        {
            float angle = i * Mathf.PI * 2 / childrenCount;
            Vector3 position = CalculateChildPosition(angle, _childrenSpawnRadius);
            Vector3 force = CalculateExplosionDirection(position) * _explosionForce * _scale;
            
            spawned = Instantiate(_cubePrefab, position, Quaternion.identity);
            spawned.Init(_scale / _nextGenScaleDivider, _divisionChance / _divisionChanceDivider, force);
        }
    }

    private Vector3 CalculateChildPosition(float angle, float radius)
    {
        return _transform.position + (new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius) * _scale);
    }
    
    private Vector3 CalculateExplosionDirection(Vector3 position)
    {
        return (position - _transform.position).normalized;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(_minColorValue, _maxColorValue), Random.Range(_minColorValue, _maxColorValue), Random.Range(_minColorValue, _maxColorValue));
    }
}
