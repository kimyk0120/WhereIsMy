using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
   
   public static int game_no = 1; //선택 게임 번호 1:똘똘이의 간식찾기, 2:이쁜이의 꽃찾기, 3:이장님의 모자찾기 
   public static int game_time_sec = 60; // 사용자 설정 - 게임 total 시간
   public static int game_difficult = 1; // 사용자 설정 - 게임 난이도 
   public static int game_player = 1; // 사용자 설정 - 사용자 숫자 
   
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
