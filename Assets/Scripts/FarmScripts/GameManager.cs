using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;

    public static GameManager instance;
    float CurrentBalance = 0;
    public string CompanyName;
 
     

    // Start is called before the first frame update
    void Start()
    {
   
           if (OnUpdateBalance != null)
        {
            OnUpdateBalance();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() 
    {
     if (instance == null)
        {
            instance = this;
        }
    }

    public void AddToBalance(float amt) 
    {
        CurrentBalance += amt;
        if (OnUpdateBalance != null)
            OnUpdateBalance();
    }

    public bool CanBuy (float AmtToSpend)
    {
        if (AmtToSpend > CurrentBalance)
            return false;
        else
        {
            return true;
        }

    }
     public float GetCurrentBalance() {
            return CurrentBalance;
        }

}
