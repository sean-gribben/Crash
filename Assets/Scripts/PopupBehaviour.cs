using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum popupTypes {
    yesno,
    ok
}


public struct popupCall {
    public popupCall(string title, string text, popupTypes type, System.Action hookFunc = null) {
        this.title = title;
        this.text = text;
        this.type = type;
        this.hookFunc = hookFunc;
    }

    

    public string title;
    public string text;
    public popupTypes type;
    public System.Action hookFunc;
}

public class PopupBehaviour : MonoBehaviour
{



    Image mainIm;

    Text titleText;
    Text mainText;

    Button yesButton;
    Button noButton;
    Button okButton;

    bool popupShowing = false;
    List<popupCall> popupQueue = new List<popupCall>();

    AudioSource noise;

    popupTypes type;

    System.Action hook;

    // Start is called before the first frame update
    void Awake()
    {
        mainIm = GetComponent<Image>();

        noise = GetComponent<AudioSource>();

        titleText = transform.Find("popupTitle").GetComponent<Text>();
        mainText = transform.Find("popupText").GetComponent<Text>();

        yesButton = transform.Find("YesButton").GetComponent<Button>();
        noButton = transform.Find("NoButton").GetComponent<Button>();
        okButton = transform.Find("OkButton").GetComponent<Button>();

        yesButton.onClick.AddListener(CallHook);
        yesButton.onClick.AddListener(ClosePopup);

        noButton.onClick.AddListener(ClosePopup);
        okButton.onClick.AddListener(CallHook);
        okButton.onClick.AddListener(ClosePopup);
    }

    public void DoPopup(string title, string text, popupTypes type, System.Action hookFunc) {

        DoPopup(title, text, type, hookFunc, false);
    }

    private void DoPopup(string title, string text, popupTypes type, System.Action hookFunc, bool queued) {

        if (popupShowing && !queued) {
            popupQueue.Add(new popupCall(title, text, type, hookFunc));
            return;
        }

        popupShowing = true;

        titleText.text = title;
        mainText.text = text;

        titleText.gameObject.SetActive(true);
        mainText.gameObject.SetActive(true);
        mainIm.enabled = true;

        hook = hookFunc;

        if(type == popupTypes.ok) {
            okButton.gameObject.SetActive(true);
        } else if (type == popupTypes.yesno) {
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);

            if (hookFunc == null) Debug.LogWarning("Should not have yes/no box with no hook function!");
        }

        noise.Play();

    }

    void CallHook() {
        hook?.Invoke();
    }


    void ClosePopup() {
        titleText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        mainIm.enabled = false;

        okButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        if(popupQueue.Count > 0) {
            popupCall call = popupQueue[0];
            DoPopup(call.title, call.text, call.type, call.hookFunc, true);
            popupQueue.RemoveAt(0);
        } else {
            popupShowing = false;
        }
    }

}
