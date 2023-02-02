using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
   bool gameHasEnded = false;

   public void GameOver()
   {
      if (gameHasEnded == false)
      {
         gameHasEnded = true;
         
         Invoke("Restart", 4f);
      }
   }

   void Restart()
   {
      SceneManager.LoadScene("ArenaDash");
   }

   
}
