using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
   float _baseSpeed = 10.0f;
   float _gravidade = 9.8f;

   GameManager gm;

   CharacterController characterController;

   //Referência usada para a câmera filha do jogador
   GameObject playerCamera;
   //Utilizada para poder travar a rotação no angulo que quisermos.
   float cameraRotation;

   GameObject keyObject;
   GameObject maskObject;
   GameObject gasObject;
   GameObject r2d2Object;
   GameObject spaceShipObject;


   void Start()
   {
       characterController = GetComponent<CharacterController>();
       playerCamera = GameObject.Find("Main Camera");
       cameraRotation = 0.0f;
       gm = GameManager.GetInstance();

       keyObject = GameObject.Find("Key");
       maskObject = GameObject.Find("Mask");
       gasObject = GameObject.Find("Gas");
       r2d2Object = GameObject.Find("R2D2");
       spaceShipObject = GameObject.Find("SpaceShip");


   }

   void Update()
   {
       if (gm.gameState != GameManager.GameState.GAME) { return; }

       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");
       
       //Verificando se é preciso aplicar a gravidade
       float y = 0;
       if(!characterController.isGrounded){
           y = -_gravidade;
       }
       
       //Tratando movimentação do mouse
       float mouse_dX = Input.GetAxis("Mouse X");
       float mouse_dY = Input.GetAxis("Mouse Y");

       //Tratando a rotação da câmera
       cameraRotation += mouse_dY;
       Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

       Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;

       characterController.Move(direction * _baseSpeed * Time.deltaTime);

       transform.Rotate(Vector3.up, mouse_dX);

       playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);

 

       if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) {
       gm.ChangeState(GameManager.GameState.PAUSE);
   }
   }

   void LateUpdate()
   {
       RaycastHit hit;
       Debug.DrawRay(playerCamera.transform.position, transform.forward*10.0f, Color.magenta);
       if(Physics.Raycast(playerCamera.transform.position, transform.forward, out hit, 1.0f))
       {
           string objectName = hit.collider.name;
           if (objectName == "Key") 
           {
               gm.KeyCaptured = true;
               keyObject.SetActive(false);
           }
           if (objectName == "Mask") 
           {
               gm.MaskCaptured = true;
               maskObject.SetActive(false);
           }
           if (objectName == "Gas")  
           {
               gm.GasCaptured = true;
               gasObject.SetActive(false);
           }
           if (objectName == "R2D2")
           {
               gm.R2D2Captured = true;
               r2d2Object.SetActive(false);
           }
           if ((objectName == "SpaceShip") && (gm.GetPontos() == 4))
           {
               spaceShipObject.SetActive(false);
               gm.spaceShipBoarded = true;
               gm.ChangeState(GameManager.GameState.ENDGAME);
           }
           Debug.Log(hit.collider.name);

       }
    }
}