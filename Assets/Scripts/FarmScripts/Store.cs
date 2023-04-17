using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    // Public Variables - Define Gameplay
    public float BaseStoreCost;
    public float BaseStoreProfit;
    public float StoreTimer;
    public bool ManagerUnlocked;
    public float StoreMultiplier;
    public bool StoreUnlocked;
    public int StoreTimerDivision;
    public int storeCount;
    float NextStoreCost;
    float CurrentTimer = 0;
    public bool StartTimer;

    

    // Start is called before the first frame update
    void Start()
    { 

        StartTimer = false;
        
    }

    public float GetCurrentTimer()
    {
        return CurrentTimer;
    }
    public float GetStoreTimer()
    {
        return StoreTimer;
    }
    public void SetNextStoreCost(float amt)
    {
        NextStoreCost = amt;
    }
    public float GetStoreCost()
    {
        return NextStoreCost;
    }
    // Update is called once per frame
    void Update()
    {
        if (StartTimer)
        {
            CurrentTimer += Time.deltaTime;
            if (CurrentTimer > StoreTimer)
            {
                if (!ManagerUnlocked)
                    StartTimer = false;
                CurrentTimer = 0f;
                GameManager.instance.AddToBalance(BaseStoreProfit * storeCount);
            }
        }    
    }
 

    public void BuyStore () 
    {
        storeCount++;
        float Amt = -NextStoreCost;
        NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier, storeCount));
        GameManager.instance.AddToBalance(Amt);
          if (storeCount % StoreTimerDivision == 0)
              StoreTimer = StoreTimer / 2;
          

        
    }

    public void OnStartTimer ()
    {
        if (!StartTimer && storeCount > 0)
        StartTimer = true; 
    }

}
