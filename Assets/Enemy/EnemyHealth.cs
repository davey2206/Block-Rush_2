using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] 
    int MaxHealth = 5;
    [Tooltip("Adds amount to MaxHealth when enemy dies.")]
    [SerializeField]
    int difficultyRamp = 1;

    int Health = 0;

    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        Health = MaxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        Health--;

        if (Health <= 0)
        {
            enemy.RewardGold();
            MaxHealth += difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
