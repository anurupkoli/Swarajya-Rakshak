using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateGold : MonoBehaviour
{
    Bank bank;
    TMP_Text textMeshPro;
    void Start(){
        bank = FindAnyObjectByType<Bank>();
        textMeshPro = GetComponent<TMP_Text>();
    }

    void Update(){
        UpdateText();
    }

    void UpdateText(){
        textMeshPro.text = "Gold : " +  bank.CurrentMoney.ToString();
    }

    
}
