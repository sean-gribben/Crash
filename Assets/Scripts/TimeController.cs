using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    private static TimeController timer;
    public static TimeController instace { get { return timer; } }
    void OnEnable() { timer = this; MarketController.onMarketTick += UpdateTime; }
    void OnDisable() { timer = null; MarketController.onMarketTick -= UpdateTime; }


    Text timeText;
    public int days = 365;
    public int hours = 0;


    void Start()
    {
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
