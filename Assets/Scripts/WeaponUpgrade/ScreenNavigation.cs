using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Arttu Paldán 1.10.2020:
public class ScreenNavigation : MonoBehaviour
{
    public GameObject shop;
    public GameObject forge;

    void Start()
    {
        shop.SetActive(false);
    }

    public void OpenShop()
    {
        forge.SetActive(false);
        shop.SetActive(true);
    }

    public void OpenForge()
    {
        shop.SetActive(false);
        forge.SetActive(true);
    }
}
