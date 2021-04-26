using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private float slider_max_val = 0;
    private float progressTime = 0;
    private Coroutine _coroutine;
    private IEnumerator co;
    private List<GameController> game_controller;
    public Text rest_time_text;
    private float rest_time;
    private GameObject[] objs;
    private bool progressBarPaused = true; 
    
    
     private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        co = ProgressCount();
    }

    private void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("CardPanel");
        slider.maxValue = slider_max_val = Globals.game_time_sec;
        // StartCoroutine(co);
    }
    
    IEnumerator ProgressCount()
    {
        while (this.progressBarPaused)
        {
            yield return new WaitForSeconds(0.1f);
            progressTime += 0.1f;
            slider.value = progressTime;
            rest_time = slider_max_val - progressTime;
            set_rest_time_text(rest_time);
            // print(progressTime);
            if (progressTime >= slider_max_val)  // 슬라이더 max 시간에 정지 
            {
                print("count end");
                StopCoroutine(co);
                for (int i = 0; i < objs.Length; i++)
                {
                    objs[i].GetComponent<GameController>().finish_game(); // 게임 종료
                }
                set_rest_time_text(0.0f);
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
    
    private void set_rest_time_text(float restTime)
    {
        rest_time_text.text = "남은시간 " + string.Format("{0:F1}", restTime);
    }

}//.class
