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
}
