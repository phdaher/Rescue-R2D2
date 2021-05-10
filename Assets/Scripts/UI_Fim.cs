﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Fim : MonoBehaviour
{
   public Text message;

    GameManager gm;
   private void OnEnable()
   {
       gm = GameManager.GetInstance();

       if ((gm.gameFinished) && (gm.GetPontos() == 4))
       {
           message.text = "Você Ganhou!!";
       }
       else 
       {
           message.text = "Você Perdeu!!!";
       }
   }

   public void Voltar()
{
   gm.ChangeState(GameManager.GameState.GAME);
}


}


