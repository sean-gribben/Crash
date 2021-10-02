using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopulator : MonoBehaviour
{

    void PayMobBack() {
        NotificationController.instance.ShowPopup("Congratulations!", "You won!", popupTypes.ok);
    }

    void IncreaseBS() {
        QuantityController.instance.maxQuantity *= 2;

        int newMoney = QuantityController.instance.maxQuantity * 2500;

        shop.AddShopItem(new ShopItem(newMoney, "Increase max buy/sell quantity by 2x", IncreaseBS));
    }

    ShopController shop;

    private void Start() {
        // Initial Shop items
        shop = ShopController.instance;

        shop.AddShopItem(new ShopItem(1000000, "Pay back the mob and avoid getting shanked", PayMobBack));
        shop.AddShopItem(new ShopItem(2500, "Increase max buy/sell quantity by 2x", IncreaseBS));
        
    }
}
