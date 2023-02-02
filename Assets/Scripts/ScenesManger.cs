using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManger : MonoBehaviour
{
    public static ScenesManger Instance;

    private void Awake()
    {
        Instance = this;
    }
    public enum Scene
    {
        Menu,
        ArenaDash
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.ArenaDash.ToString());
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(Scene.Menu.ToString());
    }
}
