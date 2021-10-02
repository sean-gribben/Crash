using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopulator : MonoBehaviour
{

    void endGame() {
        Application.Quit();
    }

    void PayMobBack() {
        NotificationController.instance.ShowPopup("Congratulations!", "You won!", popupTypes.ok, endGame);
    }



    int startingQuantity;
    void IncreaseBS() {
        QuantityController.instance.maxQuantity *= 2;

        int newMoney = QuantityController.instance.maxQuantity/startingQuantity * 2500;

        shop.AddShopItem(new ShopItem(newMoney, "Increase max buy/sell quantity by 2x", IncreaseBS));
    }

    ShopController shop;

    private void Start() {
        // Initial Shop items
        shop = ShopController.instance;

        shop.AddShopItem(new ShopItem(1000000, "Pay back the mob and avoid getting shanked", PayMobBack));
        startingQuantity = QuantityController.instance.maxQuantity;
        startingQuantity = QuantityController.instance.maxQuantity;
        shop.AddShopItem(new ShopItem(2500, "Increase max buy/sell quantity by 2x", IncreaseBS));
        
    }
}
