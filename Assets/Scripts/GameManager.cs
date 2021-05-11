using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
   private static GameManager _instance;
      public enum GameState { MENU, GAME, PAUSE, ENDGAME };
      public GameState gameState { get; private set; }

      public GameObject keyObject;
      public GameObject maskObject;
      public GameObject gasObject;
      public GameObject r2d2Object;
      public GameObject spaceShipObject;

      public bool KeyCaptured;  
      public bool MaskCaptured;
      public bool GasCaptured;
      public bool R2D2Captured;
      public bool spaceShipBoarded;
      public bool playerFirstUpdate;

      public float timeRemaining;


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
       keyObject = GameObject.Find("Key");
       maskObject = GameObject.Find("Mask");
       gasObject = GameObject.Find("Gas");
       r2d2Object = GameObject.Find("R2D2");
       spaceShipObject = GameObject.Find("SpaceShip");
       Initialize();
       gameState = GameState.MENU;

   }

   public void Initialize() 
   {
       KeyCaptured = false;  
       MaskCaptured = false;
       GasCaptured = false;
       R2D2Captured = false;
       R2D2Captured = false;
       spaceShipBoarded = false;
       playerFirstUpdate = true;
       keyObject.SetActive(true);
       maskObject.SetActive(true);
       gasObject.SetActive(true);
       r2d2Object.SetActive(true);
       spaceShipObject.SetActive(true);
       timeRemaining = 60;
   }
}
