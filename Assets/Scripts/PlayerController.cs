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

   [SerializeField]
   private AudioClip _captureClip = null;
   private AudioSource _source = null;



   void Start()
   {
       characterController = GetComponent<CharacterController>();
       playerCamera = GameObject.Find("Main Camera");
       cameraRotation = 0.0f;
       gm = GameManager.GetInstance();

    _source = GetComponent<AudioSource>();
    if (_source == null)
    {
        Debug.Log("Audio Source is NULL");
    }
    else
    {
        _source.clip = _captureClip;
    }

   }

   void Update()
   {
       if (gm.gameState != GameManager.GameState.GAME) { return; }

       if (gm.playerFirstUpdate) 
       { 
           characterController.transform.position = new Vector3(0, 1, 0);
           // characterController.transform.rotation = new Quaternion(0, 0, 0);
           gm.playerFirstUpdate = false;
           return; 
       }


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
               gm.keyObject.SetActive(false);
               _source.Play();
           }
           if (objectName == "Mask") 
           {
               gm.MaskCaptured = true;
               gm.maskObject.SetActive(false);
               _source.Play();
           }
           if (objectName == "Gas")  
           {
               gm.GasCaptured = true;
               gm.gasObject.SetActive(false);
               _source.Play();
           }
           if (objectName == "R2D2")
           {
               gm.R2D2Captured = true;
               gm.r2d2Object.SetActive(false);
               _source.Play();
           }
           if ((objectName == "SpaceShip") && (gm.GetPontos() == 4))
           {
               gm.spaceShipObject.SetActive(false);
               gm.spaceShipBoarded = true;
               gm.ChangeState(GameManager.GameState.ENDGAME);
           }
           Debug.Log(hit.collider.name);

       }
    }
}