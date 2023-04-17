using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    public Text StoreNumber;
   
    public Slider ProgressSlider;
    public Text BuyButtonText;
    public Button StoreButton;

    public Store Store;

    void OnEnable()
    {
        GameManager.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;
    }

    void OnDisable()
    {
        GameManager.OnUpdateBalance -= UpdateUI;
        LoadGameData.OnLoadDataComplete -= UpdateUI;
    }

    void Awake() 
    {
        Store = transform.GetComponent<Store>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProgressSlider.value = Store.GetCurrentTimer() / Store.GetStoreTimer();
        UpdateUI();
    }

    public void UpdateUI()
    {
        //Hide Panel
        CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();
        if (!Store.StoreUnlocked && !GameManager.instance.CanBuy(Store.GetStoreCost()))
        {

            cg.interactable = false;
            cg.alpha = 0;
        }
        else
        {
            cg.interactable = true;
            cg.alpha = 1;
            Store.StoreUnlocked = true;
        }

        if (GameManager.instance.CanBuy(Store.GetStoreCost()))
        {
            StoreButton.interactable = true;

        }
        else
            StoreButton.interactable = false;
    
        BuyButtonText.text = "Buy " + Store.GetStoreCost().ToString("C2");
       
    }
    public void BuyStoreOnClick()
    {
        if (!GameManager.instance.CanBuy(Store.GetStoreCost()))
            return;
        Store.BuyStore();
        StoreNumber.text = Store.storeCount.ToString();
        UpdateUI();
    }

    public void OnTimerClick()
    {
        Store.OnStartTimer();
    }
}
