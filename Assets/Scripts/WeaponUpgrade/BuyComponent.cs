using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Arttu Paldán 11.9.2020: This script will handle buying or unlocking component pieces.
public class BuyComponent : MonoBehaviour
{
    private Money money;

    private AbstractWeaponComponent[] weaponComponents;

    private int componentID;
    private int componentCost;

    public GameObject buyComponentScreen;
    private Text componentName;
    private Text componentDescription;
    private Text componentCostText;
    private Image componentImageBuyScreen;

    public Image[] componentImagesComponentHolder;
    public Sprite[] notBougtComponentImages;
    public Sprite[] boughtComponentImages;
    public Sprite[] placedComponentImages;

    
    void Awake()
    {
        money = GameObject.FindGameObjectWithTag("Money").GetComponent<Money>();

        componentName = GameObject.FindGameObjectWithTag("ComponentName").GetComponent<Text>();
        componentDescription = GameObject.FindGameObjectWithTag("ComponentDescription").GetComponent<Text>();
        componentCostText = GameObject.FindGameObjectWithTag("ComponentCost").GetComponent<Text>();
        componentImageBuyScreen = GameObject.FindGameObjectWithTag("BuyScreenComponentImage").GetComponent<Image>();

        SetUpWeaponComponentsArray();

        SetComponentsHolder();

        buyComponentScreen.SetActive(false);

        componentID = -1;
    }

    // Function to create an array of the abstract components.
    void SetUpWeaponComponentsArray()
    {
        TestComponent1 testComponent1 = new TestComponent1("Component 1", "Does things", 0, 50, notBougtComponentImages[0], boughtComponentImages[0], placedComponentImages[0]);
        TestComponent2 testComponent2 = new TestComponent2("Component 2", "Does things", 1, 25, notBougtComponentImages[1], boughtComponentImages[1], placedComponentImages[1]);
        TestComponent3 testComponent3 = new TestComponent3("Component 3", "Does things", 2, 100, notBougtComponentImages[2], boughtComponentImages[2], placedComponentImages[2]);
        TestComponent4 testComponent4 = new TestComponent4("Component 4", "Does things", 3, 150, notBougtComponentImages[3], boughtComponentImages[3], placedComponentImages[3]);

        weaponComponents = new AbstractWeaponComponent[] { testComponent1, testComponent2, testComponent3, testComponent4 };
    }

    void SetComponentsHolder()
    {
        for(int i = 0; i <  componentImagesComponentHolder.Length; i++)
        {
            componentImagesComponentHolder[i].sprite = weaponComponents[i].componentNotBougthImage;
        }
    }

    // Function to open the buy component screen.
    public void OpenComponent()
    {
        buyComponentScreen.SetActive(true);

        SetBuyComponentScreen();
    }

    // Function to close the buy component screen.
    public void CloseComponent()
    {
        buyComponentScreen.SetActive(false);

        componentID = -1;
    }

    // Sets the sprites and texts in the buy screen. These things are gotten from the abstract components
    void SetBuyComponentScreen()
    {
        AbstractWeaponComponent weaponsComponentArray = weaponComponents[componentID];

        componentImageBuyScreen.sprite = weaponsComponentArray.GetNotBoughtImage();
        componentName.text = weaponsComponentArray.GetName();
        componentDescription.text = weaponsComponentArray.GetDescription();
        componentCost = weaponsComponentArray.GetCost();
        componentCostText.text = "Cost: " + componentCost;
    }

    // Function for buying the components.
    public void Buy()
    {
        AbstractWeaponComponent weaponsComponentArray = weaponComponents[componentID];

        int currency = money.GetCurrentCurrency();

        if(currency >= componentCost)
        {
            money.ChangeCurrencyAmount(componentCost);

            componentImagesComponentHolder[componentID].sprite = weaponsComponentArray.GetBoughtImage();

            buyComponentScreen.SetActive(false);
        }
    }

    // A function I hope I can get rid of in the future. When player pressed one of the components in the screen, this will give us the id of the component, which can then be used by other functions.
    public void Component1Button()
    {
        componentID = weaponComponents[0].GetID();
    }

    public void Component2Button()
    {
        componentID = weaponComponents[1].GetID();
    }

    public void Component3Button()
    {
        componentID = weaponComponents[2].GetID();
    }

    public void Component4Button()
    {
        componentID = weaponComponents[3].GetID();
    }
}
