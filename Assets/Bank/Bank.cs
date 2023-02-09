using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField]
    int StartingBalance = 150;
    [SerializeField]
    int currantBalance;
    [SerializeField]
    TextMeshProUGUI displayBalance;

    public int CurrantBalance { get { return currantBalance; } }

    private void Awake()
    {
        currantBalance = StartingBalance;
        updateDisplay();
    }

    public void Deposit(int amount)
    {
        currantBalance += Mathf.Abs(amount);
        updateDisplay();
    }

    public void Withdraw(int amount)
    {
        currantBalance -= Mathf.Abs(amount);
        updateDisplay();
        if (currantBalance < 0)
        {
            ReloadScane();
        }
    }

    void updateDisplay()
    {
        displayBalance.text = "Gold: " + currantBalance.ToString();
    }

    void ReloadScane()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
