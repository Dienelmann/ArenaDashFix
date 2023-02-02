using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField]  Button _NewGame;
    [SerializeField] Button _Exit;
    void Start()
    {
        _NewGame.onClick.AddListener(StartNewGame);
        _Exit.onClick.AddListener(ExitGame);
        
    }

    private void StartNewGame()
    {
        ScenesManger.Instance.LoadNewGame();
    }
    private void ExitGame()
    {
        Application.Quit(); 
    }

}
