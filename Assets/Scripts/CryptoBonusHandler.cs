using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CryptoBonusHandler : MonoBehaviour
{
    public float showTime = 1f;
    public float moveSpeed = 0.5f;
    public string text;
    Text bonusText;

    // Start is called before the first frame update
    void Start()
    {
        bonusText = GetComponent<Text>();
        bonusText.text = text;
        Invoke("kill", showTime);
    }

    void kill() {
        Destroy(gameObject);
    }

    private void Update() {
        RectTransform trans = GetComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, trans.anchoredPosition.y + moveSpeed * Time.deltaTime);
    }
}
