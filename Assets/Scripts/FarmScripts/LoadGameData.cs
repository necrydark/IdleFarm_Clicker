using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class LoadGameData : MonoBehaviour
{

    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;

    public TextAsset GameData;
    public GameObject StorePrefab;
    public GameObject StorePanel;

    public void Start()
    {
       
        if (OnLoadDataComplete != null)
        {
            OnLoadDataComplete();
        }
        LoadData();
    }

    public void LoadData()
    {
        //Creates new XMLDocument
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(GameData.text);

        //Loads GameManager and store Data
        LoadStore(xmlDoc);
        LoadGameManagerData(xmlDoc);
    }

    void LoadGameManagerData(XmlDocument xmlDoc)
    {
        //Load GameManagerInfo
        float StartingBalance = float.Parse(xmlDoc.GetElementsByTagName("StartingBalance")[0].InnerText);
        GameManager.instance.AddToBalance(StartingBalance);

        //Company Name Load
        string CompanyName = xmlDoc.GetElementsByTagName("CompanyName")[0].InnerText;
        GameManager.instance.CompanyName = CompanyName;
    }

    void SetStoreObj(Store storeobj, XmlNode StoreNode, GameObject NewStore)
    {
        if (StoreNode.Name == "Name")
        {
            Text StoreText = NewStore.transform.Find("Name").GetComponent<Text>();
            StoreText.text = StoreNode.InnerText;
        }

        if (StoreNode.Name == "BaseStoreCost")
        {
            storeobj.BaseStoreCost = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "BaseStoreProfit")
        {
            storeobj.BaseStoreProfit = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "StoreTimer")
        {
            storeobj.StoreTimer = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "StoreMultiplier")
        {
            storeobj.StoreMultiplier = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "StoreTimeDivision")
        {
            storeobj.StoreTimerDivision = int.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "storeCount")
        {
            storeobj.storeCount = int.Parse(StoreNode.InnerText);
            Text StoreText = NewStore.transform.Find("StoreNumber").GetComponent<Text>();
            StoreText.text = StoreNode.InnerText;
        }
    }

    void LoadStoreNodes(XmlNode StoreInfo)
    {

        GameObject NewStore = (GameObject)Instantiate(StorePrefab);

        Store storeobj = NewStore.GetComponent<Store>();

        XmlNodeList StoreNodes = StoreInfo.ChildNodes;
        foreach (XmlNode StoreNode in StoreNodes)
        {
            SetStoreObj(storeobj, StoreNode, NewStore);
        }
        //Next Store Cost and next store parent panel
        storeobj.SetNextStoreCost(storeobj.BaseStoreCost);
        NewStore.transform.SetParent(StorePanel.transform);
    }

    void LoadStore(XmlDocument xmlDoc)
    {
        //Loads Stores
        XmlNodeList StoreList = xmlDoc.GetElementsByTagName("store");

        foreach (XmlNode StoreInfo in StoreList)
        {

            //Loads Store nodes
            LoadStoreNodes(StoreInfo);

        }
    }
}
