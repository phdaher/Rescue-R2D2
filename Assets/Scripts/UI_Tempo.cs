using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Tempo : MonoBehaviour
{
   Text textComp;
    GameManager gm;


   public float timeRemaining = 60;
   void Start()
   {
       textComp = GetComponent<Text>();
       gm = GameManager.GetInstance();     
   }
   
   void Update()
   {
       if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else 
        {
            timeRemaining = 5;
            // gm.NextLevel();
        }
       textComp.text = $"Restam: {Math.Truncate(timeRemaining)} s";
   }
}