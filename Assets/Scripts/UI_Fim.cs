using System.Collections;
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

       if ((gm.spaceShipBoarded) && (gm.GetPontos() == 4))
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
       gm.KeyCaptured = false;  
       gm.MaskCaptured = false;
       gm.GasCaptured = false;
       gm.R2D2Captured = false;
       gm.R2D2Captured = false;
       gm.spaceShipBoarded = false;
       gm.timeRemaining = 60;
       gm.ChangeState(GameManager.GameState.MENU);
}


}


