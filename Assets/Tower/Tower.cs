using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    int Cost = 75;
    public bool CreateTower(Tower tower, Vector3 pos)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrantBalance >= Cost)
        {
            Instantiate(tower.gameObject, pos, Quaternion.identity);
            bank.Withdraw(Cost);
            return true;
        }

        return false;
    }
}
