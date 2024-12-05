using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    //Fields
    private int _currentHealth;
    [SerializeField] private int _maxHealth;
    
    
    //Actions/Events
    public UnityAction<int> CurrentHealthUpdatedAction;
    public UnityAction<int> MaxHealthUpdatedAction;

    public UnityAction<int> DamageTakenAction;
    public UnityAction<int> HealedAction;
    
    public UnityAction DiedAction;
    public UnityAction RevivedAction;
    
    //UnityFunctions
    private void Start()
    {
        _currentHealth = _maxHealth;
        CurrentHealthUpdatedAction?.Invoke(_currentHealth);
    }

    //Functions
    void TakeDamage(int inDamageTaken)
    {
        if(inDamageTaken <= 0) return;

        _currentHealth = Mathf.Max(0, _currentHealth - inDamageTaken);
        CurrentHealthUpdatedAction?.Invoke(_currentHealth);
        
        DamageTakenAction?.Invoke(inDamageTaken);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Heal(int inHealAmount)
    {
        if(inHealAmount <= 0) return;

        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + inHealAmount);
        CurrentHealthUpdatedAction?.Invoke(_currentHealth);

        HealedAction?.Invoke(inHealAmount);        
    }
    
    void Die()
    {
        DiedAction?.Invoke();
    }

    void Revive(int inReviveHealthAmount)
    {
        RevivedAction?.Invoke();
    }
}
