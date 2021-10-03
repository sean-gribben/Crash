using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CryptoController : MonoBehaviour
{
    private static CryptoController crpt;
    public static CryptoController instance { get { return crpt; } }
    void OnEnable() { crpt = this; }
    void OnDisable() { crpt = null; }

    public GameObject textPrefab;

    public AudioSource bonkPressSound;

    public float mineCost = 75f;
    public int mineValue = 1;

    Button bonkButton;
    RectTransform jumpableArea;

    System.Random rand;

    float lastClicked;

    // Start is called before the first frame update
    void Start()
    {
        rand = MarketController.instance.rand;

        bonkButton = transform.Find("bonk").GetComponent<Button>();
        jumpableArea = transform.Find("jumpableArea").GetComponent<RectTransform>();

        bonkButton.onClick.AddListener(ButtonClicked);

        lastClicked = Time.realtimeSinceStartup - 5f;
    }

    void ButtonClicked() {
        if (!MoneyController.instance.UpdateLiquid(-mineCost, false)) {
            ShopController.instance.ErrorSound.Play();
            if (Time.realtimeSinceStartup - lastClicked > 5f) {
                NotificationController.instance.ShowNotification("You do not have the liquid funds to mine crypto!", 2f);
            } // A bit of leeway in case the player has a stroke, just so they aren't spammed with messages. God knows they'll get enough of that already
            lastClicked = Time.realtimeSinceStartup;
            return;
        }

        RectTransform bonkTrans = bonkButton.GetComponent<RectTransform>();

        Vector2 min = new Vector2(jumpableArea.rect.xMin + bonkTrans.rect.width / 2, jumpableArea.rect.yMin + bonkTrans.rect.height / 2);
        Vector2 max = new Vector2(jumpableArea.rect.xMax - bonkTrans.rect.width / 2, jumpableArea.rect.yMax - bonkTrans.rect.height);

        float newX = RandomUtility.NextFloat(rand, min.x, max.x);
        float newY = RandomUtility.NextFloat(rand, min.y, max.y);

        GameObject lostMoney = Instantiate(textPrefab);
        lostMoney.transform.SetParent(transform);
        Vector2 transPos = bonkTrans.anchoredPosition;
        transPos.y += 20f;
        lostMoney.GetComponent<RectTransform>().anchoredPosition = transPos;

        CryptoBonusHandler monHdlr = lostMoney.GetComponent<CryptoBonusHandler>();

        monHdlr.text = "-$" + mineCost.ToString("N0");
        monHdlr.GetComponent<Text>().color = Color.red;

        GameObject cryp = Instantiate(textPrefab);
        cryp.transform.SetParent(transform);
        transPos = bonkTrans.anchoredPosition;
        transPos.y += 40f;
        cryp.GetComponent<RectTransform>().anchoredPosition = transPos;

        CryptoBonusHandler crypHdlr = cryp.GetComponent<CryptoBonusHandler>();

        StockBehaviour stonk = MarketController.instance.stonks[rand.Next(0, MarketController.instance.stonks.Count)];

        crypHdlr.text = "+" + mineValue + " " + stonk.code;

        stonk.stocks += mineValue;
        stonk.UpdateSocks("Init");

        bonkPressSound.Play();




        bonkTrans.anchoredPosition = new Vector2(newX, newY);
    }


}
