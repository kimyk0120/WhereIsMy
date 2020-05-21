using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
   public static int game_no = 0;
   public static int game_time_sec = 60;
   public static int game_difficult = 1;
   public static int game_player = 1;
   
   private void Awake()
   {
      DontDestroyOnLoad(this);
   }

   public static void print_globals()
   {
      print("game_no : " + game_no);
      print("game_time_sec : " + game_time_sec);
      print("game_difficult : " + game_difficult);
      print("game_player : " + game_player);
   }
}
