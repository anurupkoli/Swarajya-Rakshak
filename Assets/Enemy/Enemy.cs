using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int rewardMoney = 25;
    [SerializeField] int deductMoney = 25;

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardMoney()
    {
        if (bank == null) { return; }
        bank.Deposit(rewardMoney);
    }

    public void DeductMoney()
    {
        if (bank == null) { return; }
        bank.Retrive(deductMoney);
    }
}
