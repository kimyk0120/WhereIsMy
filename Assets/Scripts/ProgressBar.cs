using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private float slider_max_val = 0;
    private float progressTime = 0;
    private Coroutine _coroutine;
    private IEnumerator co;
    public GameController game_controller;
    
     private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        
        slider.maxValue = slider_max_val = Globals.game_time_sec;
        co = ProgressCount();
        StartCoroutine(co);
    }
    
    IEnumerator ProgressCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            progressTime++;
            slider.value = progressTime; 
            // print(progressTime);
            if (progressTime > slider_max_val)  // 슬라이더 max 시간에 정지 
            {
                print("count end");
                StopCoroutine(co);
                game_controller.finish_game(); // 게임 종료 
            }
        }
    }
    
    public void StartCountTime()
    {
        StartCoroutine(co);
    }
    
    public void StopCountTime()
    {
        StopCoroutine(co);
    }
    
}//.class
