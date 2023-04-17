using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
 
    public Text CurrentBalanceText;
    public Text CompanyNameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable() 
    {
        GameManager.OnUpdateBalance += UpdateUI;   
    }

    void OnDisable() 
    {
        GameManager.OnUpdateBalance -= UpdateUI;      
    }

    public void UpdateUI() {
        CurrentBalanceText.text = GameManager.instance.GetCurrentBalance().ToString("C2");
        CompanyNameText.text = GameManager.instance.CompanyName;
    }
}
