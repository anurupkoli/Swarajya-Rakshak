using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingMoney = 150;
    [SerializeField] int currentMoney;
    public int CurrentMoney
    {
        get
        {
            return currentMoney;
        }
    }

    void Awake()
    {
        currentMoney = startingMoney;
    }

    public void Deposit(int amount)
    {
        currentMoney += Mathf.Abs(amount);
    }

    public void Retrive(int amount)
    {
        currentMoney -= Mathf.Abs(amount);
        if (currentMoney < 0)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
