using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField]
    Transform Weapon;
    [SerializeField]
    float Range = 15f;
    [SerializeField]
    ParticleSystem bulletParticles;

    Transform Target;

    void Update()
    {
        FindClosesttarget();
        AimWeapon();
    }

    void FindClosesttarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        Target = closestTarget;
    }
    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, Target.transform.position);

        Weapon.LookAt(Target);

        if (targetDistance < Range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    private void Attack(bool isActive)
    {
        var emissionModule = bulletParticles.emission;
        emissionModule.enabled = isActive;
    }
}
