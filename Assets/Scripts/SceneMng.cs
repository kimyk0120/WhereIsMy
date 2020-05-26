using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMng : MonoBehaviour
{
    
    public void go_to_scene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void go_to_main_scene()
    {
        if (Globals.game_player == 2)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }
}
