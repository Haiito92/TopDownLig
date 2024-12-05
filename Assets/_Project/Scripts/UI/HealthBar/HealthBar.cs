using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField] private HealthComponent _healthComponent;

    [SerializeField] private GameObject _healthPointPrefab;
    private List<HealthPoint> _healthPoints;
    
    
    [SerializeField] private Sprite _emptyHealthPointSprite;
    [SerializeField] private Sprite _fullHealthPointSprite;
    // Start is called before the first frame update
    void Start()
    {
        _healthPoints = new List<HealthPoint>();
        
        for (int i = 0; i < 5; i++)
        {
            GameObject goHealthPoint = Instantiate(_healthPointPrefab, transform);
            
            if (goHealthPoint.TryGetComponent(out HealthPoint healthPoint))
            {
                _healthPoints.Add(healthPoint);
            }
        }
    }
}
