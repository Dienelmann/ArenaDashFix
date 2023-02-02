using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField]  Button _Menu;
    void Start()
    {
        _Menu.onClick.AddListener(StartMenu);
        
        
    }

    private void StartMenu()
    {
        ScenesManger.Instance.LoadMenu();
    }
}
