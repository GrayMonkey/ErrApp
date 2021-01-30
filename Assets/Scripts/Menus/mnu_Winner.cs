﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Menus = MenuHandler.MenuOverlay;

public class mnu_Winner : MonoBehaviour 
{
    [SerializeField] Text winningPlayer;

    GameManager gameManager;
    PlayerController playerController;
    MenuHandler uiMenu;
    Player activePlayer;

    private void Awake()
    {
        gameManager = GameManager.instance;
        playerController = PlayerController.instance;
        uiMenu = MenuHandler.instance;
    }

    private void OnEnable()
    {
        activePlayer = playerController.activePlayer;
        winningPlayer.text = activePlayer.playerName;
    }

    public void CloseMenu(bool home)
    {
        if (home)
        {
            // Update player stats
            activePlayer.gamesWon++;
            activePlayer.questionsThisGame++;
            activePlayer.answersThisGame++;
            activePlayer.pointsThisGame += gameManager.activeQuestion.maxPoints;
            playerController.SavePlayerData();
            uiMenu.CloseMenu(Menus.WinningPlayer);
            uiMenu.ShowMenu(Menus.GameResults);
        } else {
            uiMenu.CloseMenu(Menus.WinningPlayer);
            uiMenu.ShowMenu(Menus.CorrectAnswer);
        }
    }
}
