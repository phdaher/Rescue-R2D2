using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Tempo : MonoBehaviour
{
   Text textComp;
   GameManager gm;


   void Start()
   {
       textComp = GetComponent<Text>();
       gm = GameManager.GetInstance();     
   }
   
   void Update()
   {
       if (gm.gameState == GameManager.GameState.GAME)
        {   
            if (Math.Truncate(gm.timeRemaining) == 0) 
            {
                gm.ChangeState(GameManager.GameState.ENDGAME);
            }
            else 
            {
                gm.timeRemaining -= Time.deltaTime;
            }
        }
       textComp.text = $"Restam: {Math.Truncate(gm.timeRemaining)} s";
   }
}