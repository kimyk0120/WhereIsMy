using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMng : MonoBehaviour
{
    
    public void SetGameNo(int gm_no)
    {
        Globals.game_no = gm_no;
    }

    public void Set_Game_Time(int time)
    {
        Globals.game_time_sec = time;
    }
    
    public void Set_Game_Difficult(int val)
    {
        Globals.game_difficult = val;
    }
    
    public void Set_Game_Player(int val)
    {
        Globals.game_player = val;
    }
    
    public void printGlobals(){
        Globals.print_globals();
    }
}
