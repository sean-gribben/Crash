using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopItemBehaviour : MonoBehaviour
{
    public int id;

    public int price;
    public string description;

    public System.Action itemFunc;

    Text titleText;
    Text descriptionText;
    Button button;

    float lastClicked;
    // Start is called before the first frame update

    void Start()
    {
        titleText = transform.Find("Title").GetComponent<Text>();
        descriptionText = transform.Find("Description").GetComponent<Text>();

        titleText.text = "$" + price.ToString("N0");
        descriptionText.text = description;

        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
        lastClicked = Time.realtimeSinceStartup - 5f;
    }

    void Clicked() {
        if (!MoneyController.instace.UpdateLiquid(-price, false)) {
            ShopController.instance.ErrorSound.Play();
            if(Time.realtimeSinceStartup - lastClicked > 5f) {
                NotificationController.instance.ShowNotification("You do not have the liquid funds to purchase this item!");
            } // A bit of leeway in case the player has a stroke, just so they aren't spammed with messages. God knows they'll get enough of that already
            lastClicked = Time.realtimeSinceStartup;
            return;
        }

        itemFunc?.Invoke();

        ShopController.instance.ItemBoughtSound.Play();

        ShopController.instance.RemoveShopItem(id);

        Destroy(gameObject);
    }

}
