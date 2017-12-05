﻿using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlayingGame : MonoBehaviour
{
    public Button ButtonLeft,ButtonRight;
    public Text ResultPanel;
    private GameButton GBL, GBR;
    private AI computer;
    private int[] BL = new int[] { 3, 2, 2 };
    private int[] BR = new int[] {2,5,1};
private char[] Act;
    public char BLA, BRA;
    private int i, res, fin, BLAI, BRAI;
    
    
   
    private void Start()
    {
        Act = new char[] { '*', '+' };
        GBL = new GameButton();
        GBR = new GameButton();
        computer = new AI();
        i = Random.Range(0, BL.Length);
        BLAI = Random.Range(0, Act.Length);
        BRAI = Random.Range(0, Act.Length);
        BRA = Act[BRAI];
        BLA = Act[BLAI];
        ButtonLeft.GetComponentInChildren<Text>().text =BLA+System.Convert.ToString(BL[i]);
        ButtonRight.GetComponentInChildren<Text>().text =BRA+System.Convert.ToString(BR[i]);
        GBL.SetGameButton(BLA, BL[i]);
        GBR.SetGameButton(BRA, BR[i]);
        res = Random.Range(2, 20);
        fin = Random.Range(20, 40);
        ResultPanel.GetComponent<Text>().text = System.Convert.ToString(res);
    }
    public void OnClickLeft()
    {
        res =  GBL.getResult(res);
        ResultPanel.GetComponent<Text>().text = System.Convert.ToString(res);
        new WaitForSeconds(5);
        res = computer.AiStep(GBL, GBR, res, fin);
        
        ResultPanel.GetComponent<Text>().text = System.Convert.ToString(res);
        
    }
    public void OnClickRight()
    {
        res = GBR.getResult(res);
        ResultPanel.GetComponent<Text>().text = System.Convert.ToString(res);
        res = computer.AiStep(GBL, GBR, res, fin);
       
        ResultPanel.GetComponent<Text>().text = System.Convert.ToString(res);

    }
    

}
