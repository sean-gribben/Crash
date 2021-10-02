using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class NotificationController : MonoBehaviour
{
    private static NotificationController notifs;
    public static NotificationController instance { get { return notifs; } }
    void OnEnable() { notifs = this; }
    void OnDisable() { notifs = null; }

    public float notificationShowTime;
    public float notificationAppearTime;

    PopupBehaviour popup;

    List<string> queuedNotifs = new List<string>();

    RectTransform notifTrans;
    RectTransform startLoc;
    RectTransform endLoc;

    public float moving;
    public float showing;
    bool appearing = true;

    public Text notifText;

    AudioSource noise;

    private void Awake() {
        popup = transform.Find("popupBack").GetComponent<PopupBehaviour>();

        notifTrans = transform.Find("notifBack").GetComponent<RectTransform>();

        startLoc = transform.Find("notifStartLoc").GetComponent<RectTransform>();
        endLoc = transform.Find("notifEndLoc").GetComponent<RectTransform>();

        noise = GetComponent<AudioSource>();
    }

    private void Start() {
        // ShowPopup("GOD HELP YOU", "We've been trying to reach you concerning your vehicle's extended warranty. You should've received a notice in the mail about your car's extended warranty eligibility. Since we've not gotten a response, we're giving you a final courtesy call before we close out your file. Press 2 to be removed and placed on our do-not-call list. To speak to someone about possibly extending or reinstating your vehicle's warranty, press 1 to speak with a warranty specialist.", popupTypes.ok);
        ShowPopup("Welcome to Crash!", "After blowing 1 million dollars on hookers and cocain " +
            "you borrowed from the mob, you only have $20,000 and 1 month to pay them back! Do whatever you need to make money before the deadline!", popupTypes.ok);
    }

    public void ShowPopup(string title, string text, popupTypes type) {
        popup.DoPopup(title, text, type, null);
    }

    public void ShowPopup(string title, string text, popupTypes type, System.Action hook) {
        popup.DoPopup(title, text, type, hook);
    }

    public void ShowNotification(string text) {
        if(moving > 0 || showing > 0) {
            queuedNotifs.Add(text);
            return;
        }
        notifText.text = text;
        moving = notificationAppearTime;
        noise.Play();
    }


    private void Update() {
        if (moving > 0f) {
            moving -= Time.deltaTime;
            float percent = 1 - (moving / notificationAppearTime);
            if (appearing) {
                notifTrans.anchoredPosition = Vector2.Lerp(startLoc.anchoredPosition, endLoc.anchoredPosition, percent);
            } else {
                notifTrans.anchoredPosition = Vector2.Lerp(endLoc.anchoredPosition, startLoc.anchoredPosition, percent);
            }

            if(moving <= 0) {
                appearing = !appearing;
                if (!appearing) { // Still need to put notif away
                    showing = notificationShowTime;
                } else if (queuedNotifs.Count > 0){ // Done showing notif, check if there are queued notifs
                    ShowNotification(queuedNotifs[0]);
                    queuedNotifs.RemoveAt(0);
                }
            }
        }

        if(showing > 0f) {
            showing -= Time.deltaTime;

            if(showing <= 0) {
                moving = notificationAppearTime;
            }
        }
    }

}
