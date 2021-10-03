using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    private static TimeController timer;
    public static TimeController instance { get { return timer; } }
    void OnEnable() { timer = this; MarketController.onMarketTick += UpdateTime; }
    void OnDisable() { timer = null; MarketController.onMarketTick -= UpdateTime; }


    Text timeText;
    public int days = 90;
    public int hours = 0;

    public int startDays;


    void Start()
    {
        startDays = days;
        timeText = GetComponent<Text>();
    }


    public void UpdateTime(string p) {
        hours--;
        if(hours < 0) {
            hours = 23;
            days--;
        }

        timeText.text = days + " Days, " + hours + " Hours";
    }
    
}
