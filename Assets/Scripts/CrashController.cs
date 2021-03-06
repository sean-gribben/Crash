using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashController : MonoBehaviour
{

    private static CrashController crashCtr;
    public static CrashController instance { get { return crashCtr; } }
    void OnEnable() { crashCtr = this; MarketController.onMarketTick += crashTick; }
    void OnDisable() { crashCtr = null; MarketController.onMarketTick -= crashTick; }

    public AudioSource crashWarningNoise;

    public int crashFrequency; // The average number of in game hours until a crash occurs
    public float crashStrength; // The degree in which the stocks will plummet (on average)
    public int crashWarning; // How many hours before hand to start sending warnings
    public float crashChance; // The chance of a crash being triggered on an hour once allowed
    public int crashLength; // The number of hours to spend crashing
    public int recoveryLength; // The number of hours to spend bouncing back from a crash
    public float recoveryStrength; // The strength of the recovery


    public int initialDelay; // Initial delay on the crash

    public bool crashHappening = false;
    public int timeSinceLast;
    public int crashDelay;
    public int crashing;
    public int recovery;

    System.Random rand; 

    private void Start() {
        rand = MarketController.instance.rand;
    }

    public void CauseCrash() {
        timeSinceLast = 0;
        crashDelay = 1;
    }

    void crashTick(string p) {
        if(crashDelay > 0) {
            // Counting down to crash
            crashDelay--;
            if(crashDelay <= 0) {
                // Time to crash
                crashing = crashLength;
                MarketController.instance.UpdateStocks(crashStrength);
            }
            return;
        }

        if(crashing > 0) {
            if (!crashHappening) {
                crashHappening = true;
                crashWarningNoise.Play();
                MusicController.instance.PlayCrashMusic();
                NotificationController.instance.ShowNotification("The market is crashing! Quick, sell your stocks so you can buy them back when they're cheap!", 5f);
            }
            
            crashing--;
            if(crashing <= 0) {
                crashHappening = false;
                recovery = recoveryLength;
                MarketController.instance.UpdateStocks(recoveryStrength);
                MusicController.instance.StopCrashMusic();
                NotificationController.instance.ShowNotification("The stocks are cheap! Buy them up now before they bounce back!", 5f);
            }
            return;
        }

        if(recovery > 0) {
            recovery--;
            if(recovery <= 0) {
                MarketController.instance.ResetStocks();
            }
            return;
        }


        timeSinceLast++;
        if(timeSinceLast <= crashFrequency + initialDelay) {
            return;
        }
        initialDelay = 0; // Ensure the initialdelay only delays...initially
        // After the time is up, crashes can begin. They will always have a n hour delay before they occur to give player warning with news outlets/other means

        if(rand.NextDouble() < crashChance) {
            timeSinceLast = 0;
            crashDelay = crashWarning;
            NewsController.instance.SendCrashWarning();
        }




    }
}
