using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    Button continueButton;

    private void Start() {
        continueButton = transform.Find("Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(CloseTutorial);
    }

    void CloseTutorial() {
        MarketController.instance.contSound.Play();
        MarketController.instance.paused = false;
        gameObject.SetActive(false);
    }
}
