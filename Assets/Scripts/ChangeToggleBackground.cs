using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToggleBackground : MonoBehaviour
{
    private GameObject[] toggleBtns;
    
    void Start()
    {    
        toggleBtns = GameObject.FindGameObjectsWithTag("ConfigToggleBtn");
        changeToggleBack();
    }

    public void changeToggleBack()
    {
        for (int i = 0; i < toggleBtns.Length; i++)
        {
            Toggle a = this.toggleBtns[i].GetComponent<Toggle>();
            ColorBlock b = a.colors;
            if (a.isOn == true)
            {
                b.normalColor = Color.gray;
                b.selectedColor = Color.gray;
                a.colors = b;
            }
            else
            {
                b.normalColor = Color.white;
                b.selectedColor = Color.white;
                a.colors = b;
            }
        }
    }
}
