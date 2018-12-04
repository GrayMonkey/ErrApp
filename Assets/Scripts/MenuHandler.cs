﻿#pragma warning disable 649   // Disable [SerializeField] warnings CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuHandler : MonoBehaviour 
{
    public static MenuHandler uiMenus;
    public enum MenuOverlay { Options, FailAnswer, CorrectAnswer, NewQuestion, WinningPlayer, 
        PlayerInfo, RemovePlayer, PlayerStats, GameResults, QuitGame, Credits };

    [SerializeField] GameObject backPanel;
    [SerializeField] GameObject[] menuArray;

    private void Awake()
    {
        uiMenus = this;
    }

    public void ShowMenu(MenuOverlay menuID)
    {
        int iD = (int)menuID;
        menuArray[iD].SetActive(true);
        backPanel.SetActive(true);
    }

    public void CloseMenu(MenuOverlay menuID)
    {
        int iD = (int)menuID;
        menuArray[iD].SetActive(false);
        backPanel.SetActive(false);
    }
}
