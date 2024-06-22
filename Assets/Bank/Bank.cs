using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Deposit(int amount){
        currentMoney += Mathf.Abs(amount);
    }

    public void Retrive(int amount){
        currentMoney -= Mathf.Abs(amount);
    }
}
