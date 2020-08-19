﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;





public class GameController : MonoBehaviour
{
    
    
    public int TurnCount;
    public int whoturn;
    public GameObject[] turnIcons;
    public Sprite[] playIcon;
    public Button[] CaroSpaces;
    public int[] markedSpaces;
    int[,] matrix = new int[21,21];
    public Text WinerText;
    public Image think;
    public AudioSource ButtonClick;
    public AudioSource WinAudio;

    private bool player;
    private bool computer;
    public float delay;
    public int value;

    void Start()
    {
        GameSetup();
    }
    void GameSetup()
    {
        
        whoturn = 0;
        TurnCount = 0;
        
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < CaroSpaces.Length; i++)
        {
            
            CaroSpaces[i].interactable = true;
            CaroSpaces[i].GetComponent<Image>().sprite = null;           
           
        }
        for(int j = 0; j < CaroSpaces.Length ; j++)
        {
            markedSpaces[j] = -1;
            
        }
        for (int h = 0; h < 21; h++)
        {
            for (int k = 0; k < 21; k++)
            {
                matrix[h,k] = -1;
            }
        }
    }
    
    void Update()
    {
       
    }
    
    void Winer(string wintext){

        WinerText.gameObject.SetActive(true);
        think.gameObject.SetActive(true);
        WinerText.text = wintext;
       
        WinerText.text = wintext;
       
        for(int i = 0; i<CaroSpaces.Length;i++)
        {
            CaroSpaces[i].interactable = false;
        }
    }
    public void ButtonAudio(){
        ButtonClick.Play();

    }
    public void WinerSound()
    {
        WinAudio.Play();
    }
    public void carobutton(int WhichNumber)
    {
        CaroSpaces[WhichNumber].image.sprite = playIcon[whoturn];
        CaroSpaces[WhichNumber].interactable = false;
        markedSpaces[WhichNumber] = whoturn;
        TurnCount++;
        int ypos = (int)(WhichNumber) / 21;
        int xpos = (int)(WhichNumber) % 21;       
        matrix[xpos,ypos] = whoturn;
        //Debug.Log(xpos + " " + ypos + " " + matrix[xpos,ypos]);
        if (whoturn == 0)
        {
            player = true;
            whoturn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);

        } else
        {
            player = false;
            whoturn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        if (isEndGame(xpos,ypos) == 1)
        {
            Winer("Player X Win!!!!!!");
            WinerSound();
        }
        if (isEndGame(xpos, ypos) == -1)
        {
            Winer("Player O Win!!!!!!");
            WinerSound();
        }
    }
    
    private int isEndGame(int x, int y)
    {
        if ((isEndGameVer(x,y) == 10) || (isEndGameHori(x,y) == 10) || (isEndGamePrimory(x,y) == 10) || (isEndGameSub(x,y) == 10))
        {
            return 1;
        }
        if ((isEndGameVer(x,y) == -10) || (isEndGameHori(x,y) == -10) || (isEndGamePrimory(x,y) == -10) || (isEndGameSub(x,y) == -10))
        {
            return -1;
        }
        return 0;
    }
     private int isEndGameVer(int x, int y)
    {
        int countTopO = 0;
        for(int i = y; i >= 0 ; i--)
        {
            if(matrix[x,i] == 1)
            {
                countTopO++;
                
            }
            else
            {
                break;
            }
        }
        int countButtonO = 0;
        for (int i = y+1; i < 21; i++)
        {
            if (matrix[x, i] == 1)
            {
                countButtonO++;
                
            }
            else
            {
                break;
            }
        }
        int countTopX = 0;
        for (int i = y; i >= 0; i--)
        {
            if (matrix[x, i] == 0)
            {
                countTopX++;

            }
            else
            {
                break;
            }
        }
        int countButtonX = 0;
        for (int i = y + 1; i < 21; i++)
        {
            if (matrix[x, i] == 0)
            {
                countButtonX++;

            }
            else
            {
                break;
            }
        }
        if (countTopO + countButtonO == 5)
        { return -10; }
        if (countTopX + countButtonX == 5)
        { return 10; }
        return 0;
    }
    private int isEndGameHori(int x, int y)
    {
        int countLeftO = 0;
        for (int i = x; i >= 0; i--)
        {
            if (matrix[i, y] == 1)
            {
                countLeftO++;
                
            }
            else
            {
                break;
            }
        }
        int countRightO = 0;
        for (int i = x + 1; i < 21; i++)
        {
            if (matrix[i, y] == 1)
            {
                countRightO++;
                
            }
            else
            {
                break;
            }
        }
        int countLeftX = 0;
        for (int i = x; i >= 0; i--)
        {
            if (matrix[i, y] == 0)
            {
                countLeftX++;

            }
            else
            {
                break;
            }
        }
        int countRightX = 0;
        for (int i = x + 1; i < 21; i++)
        {
            if (matrix[i, y] == 0)
            {
                countRightX++;

            }
            else
            {
                break;
            }
        }
        if (countLeftO + countRightO == 5)
        {
            return -10;
        }
        if(countLeftX + countRightX == 5)
        {
            return 10;
        }
        return 0;
    }
    private int isEndGamePrimory(int x, int y)
    {

        int countTopO = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x - i < 0 || y - i < 0)
                break;
            if (matrix[x - i, y - i] == 1)
            {
                countTopO++;

            }
            else
            {
                break;
            }
        }
        int countButtonO = 0;
        for (int i = 1; i < 5; i++)
        {
            if (x + i > 20 || y + i > 20)
                break;
            
            if (matrix[x+i, y+i] == 1)
            {
                countButtonO++;

            }
            else
            {
                break;
            }
        }
        int countTopX = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x - i < 0 || y - i < 0)
                break;
            if (matrix[x - i, y - i] == 0)
            {
                countTopX++;

            }
            else
            {
                break;
            }
        }
        int countButtonX = 0;
        for (int i = 1; i < 5; i++)
        {
            if (x + i > 20 || y + i > 20)
                break;
            if (matrix[x + i, y + i] == 0)
            {
                countButtonX++;

            }
            else
            {
                break;
            }
        }
        if (countTopO + countButtonO == 5)
        {
            return -10;
        }
        if (countTopX + countButtonX == 5)
        {
            return 10;
        }
        return 0;
    }
    private int isEndGameSub(int x, int y)
    {
        int countTopO = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x + i > 20 || y - i < 0)
                break;
            if (matrix[x + i, y - i] == 1)
            {
                countTopO++;

            }
            else
            {
                break;
            }
        }
        int countButtonO = 0;
        for (int i = 1; i < 5; i++)
        {
            if (x - i < 0 || y + i > 20)
                break;

            if (matrix[x - i, y + i] == 1)
            {
                countButtonO++;
            }
            else
            {
                break;
            }
        }
        int countTopX = 0;
        for (int i = 0; i < 5; i++)
        {
            if (x + i > 20 || y - i < 0)
                break;
            if (matrix[x + i, y - i] == 0)
            {
                countTopX++;

            }
            else
            {
                break;
            }
        }
        int countButtonX = 0;
        for (int i = 1; i < 5; i++)
        {
            if (x - i < 0 || y + i > 20)
                break;
            if (matrix[x - i, y + i] == 0)
            {
                countButtonX++;

            }
            else
            {
                break;
            }
        }
        if (countTopO + countButtonO == 5)
        {
            return -10;
        }
        if ( countTopX + countButtonX == 5)
        {
            return 10;
        }
        return 0;
    }
    #region  buttoncontrol
    public void exitbutton()
    {
        Application.Quit();
    }
    public void newgamebutton()
    {
        GameSetup();
        WinerText.gameObject.SetActive(false);
        think.gameObject.SetActive(false);
    }
    public void ButtonAudio(){
        ButtonClick.Play();
    }
    public void WinerSound()
    {
        WinAudio.Play();
    }
    public void LoseSound()
    {
        LoseAudio.Play();
    }
    public void playervscom()
    {
        GameSetup();
        com = false;
        player = true;
        WinerText.gameObject.SetActive(false);
        think.gameObject.SetActive(false);
    }
    public void playervsplayer()
    {
        GameSetup();
        player = false;
        com = true;
        WinerText.gameObject.SetActive(false);
        think.gameObject.SetActive(false);
    }
#endregion
#region Cắt tỉa Alpha betal
    bool catTia(int i, int j)
    {
        if (catTiaNgang(i,j) && catTiaDoc(i,j) && catTiaCheoPhai(i,j) && catTiaCheoTrai(i,j))
            return true;

        return false;
    }

    bool catTiaNgang(int i, int j)
    {
        if (j <= 20 - 5)
            for (int k = 1; k <= 4; k++)
                if (matrix[i,j+k] != -1)
                    return false;

        if (j >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i,j-k] != -1)
                    return false;

        return true;
    }
    bool catTiaDoc(int i, int j)
    {
        if (i <= 20 - 5)
            for (int k = 1; k <= 4; k++)
                if (matrix[i+k,j] != -1)
                    return false;


        if (i >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i-k,j] != -1)
                    return false;

        return true;
    }
    bool catTiaCheoPhai(int i, int j)
    {
        if (i <= 20 - 5 && j >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i+k,j-k] != -1)
                    return false;

        if (j <= 20 - 5 && i >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i-k,j+k] != -1)
                    return false;

        return true;
    }
    bool catTiaCheoTrai(int i, int j)
    {
        if (i <= 20 - 5 && j <= 21 - 5)
            for (int k = 1; k <= 4; k++)
                if (matrix[i+k,j+k] != -1)
                    return false;

        if (j >= 4 && i >= 4)
            for (int k = 1; k <= 4; k++)
                if (matrix[i-k,j-k] != -1)
                    return false;

        return true;
    }
#endregion
}


