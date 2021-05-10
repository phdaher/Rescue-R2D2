using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
   private static GameManager _instance;
      public enum GameState { MENU, GAME, PAUSE, ENDGAME };
      public GameState gameState { get; private set; }

      public bool KeyCaptured;  
      public bool MaskCaptured;
      public bool GasCaptured;
      public bool R2D2Captured;
      public bool gameFinished;

      public delegate void ChangeStateDelegate();
      public static ChangeStateDelegate changeStateDelegate;

public void ChangeState(GameState nextState)
{
   gameState = nextState;
   changeStateDelegate();
}

    public int GetPontos()
    {
        int pontos = 0;
        if (KeyCaptured) { pontos++; };
        if (MaskCaptured) { pontos++; };
        if (GasCaptured) { pontos++; };
        if (R2D2Captured) { pontos++; };
        return pontos;
    }

   public static GameManager GetInstance()
   {
       if(_instance == null)
       {
           _instance = new GameManager();
       }

       return _instance;
   }
   private GameManager()
   {
       KeyCaptured = false;  
       MaskCaptured = false;
       GasCaptured = false;
       R2D2Captured = false;
       R2D2Captured = false;
       gameFinished = false;
       gameState = GameState.MENU;

   }
}
