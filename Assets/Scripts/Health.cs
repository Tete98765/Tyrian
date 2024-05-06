using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100.0f;
    [SerializeField]
    private HealthBar healthBar;

    public float _currentHealth;

    private bool isDead = false;
    private float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            healthBar.SetFillLevel(_currentHealth / maxHealth);
            if(_currentHealth <= 0)
            {
                if(gameObject.CompareTag("Enemy")) {
                    EnemiesFactory.enemiesNum--;
                    ShipController.totalKillEnemies++;
                    Destroy(gameObject);
                } 
                else if(gameObject.CompareTag("Player")) {
                    isDead = true;
                    ScoreManager.Instance.YouDead();
                    //todo stop the game 
                }
                else if(gameObject.CompareTag("Boss")) {
                    ScoreManager.Instance.FinalWin();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }
    public void DealDamage(float damage)
    {
        if(gameObject.CompareTag("Player") && !ShipController.isMortal) {
            CurrentHealth -= damage;
        }
        else if(gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss")) {
            CurrentHealth -= damage;
        }
    }
    public void Heal(float heal)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + heal);
    }
    public void Kill()
    {
        CurrentHealth = 0;
        Debug.Log("You died!");
    }

}
