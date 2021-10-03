using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewsController : MonoBehaviour
{
    private static NewsController news;
    public static NewsController instance { get { return news; } }
    void OnEnable() { news = this; MarketController.onMarketTick += RandomNews; }
    void OnDisable() { news = null; MarketController.onMarketTick -= RandomNews; }

    public float randomNewsChance;
    public float randomSpamChance;

    public float randomSpamChanceInCrash;

    public int newsLevel = 0;
    public int adBlockerLevel = 0;

    System.Random rand;

    string[] level0CrashMessages = { "Local man opens new ice cream stand. 'It's so good' says citizen", "We've been trying to reach you concerning your vehicle's extended warranty.",
    "People flock to beach on hot day", "'I feel really hungry' claims local man. Skeptics are unsure", "hi how r u going today", "I feel lonely", "BREAKING: got ur attention lol",
    "I'm getting fired for making irrelevant news headlines :(", "Shop on 5th is selling new doughnuts. They're really bad", "I think I ate something funny, I feel sick",
    "E", "My Dad gave me a present!! Thank you Dad!", "You guys really believe in microwaves????"};

    string[] level1CrashMessages = {"Senator resigns in corruption scandal", "New study indicates lower levels of happiness amongst depressed individuals", "New cancer drug found innefective",
    "'Small outbreak' of flu-like disease detected in China", "Florida man transcends", "Apricot", "Autonomous cars soon to be released", "'It's tough on us all' - Billionaire philanthropist cries",
    "Market instability at an all time low"};

    string[] level2CrashMessages = { "Your informant indicates the market is unstable. It may crash soon" };



    string[] level0RandomNews;
    string[] level1RandomNews = {"New study indicates lower levels of happiness amongst depressed individuals", "New cancer drug found innefective",
        "Florida man transcends", "Apricot", "Autonomous cars soon to be released", "'It's tough on us all' - Billionaire philanthropist cries", 
        "Local journalist fired over irrelevant news headlines", "Largest gathering on record as people come to play a state-wide game of tag", 
        "Man dies base jumping trying to use helium balloons as parachute", "Forest saved from fire by hero dog pissing on embers", 
        "Children's toy recalled after found to not conserve momentum.", "I do not recognize the bodies in the water",
        "'Come outside, the sunlight is beautiful' they sing"};


    bool inPyarmidScheme = false;


    popupCall[] ads;


    void joinPyramid() {
        if (inPyarmidScheme) {
            NotificationController.instance.ShowPopup("Again?", "Wow you really tried to join again? For that extra devotion... We'll scam even MORE money from you! Yay!", popupTypes.ok);
            return;
        }
        inPyarmidScheme = true;
        NotificationController.instance.ShowPopup("Thank you for joining our pyramid scheme!", "Remember, you can leave at any time by emailing us at [FILE CORRUPTED]. " +
            "Thank you for your continued support!", popupTypes.ok);
    }

    void payPrince() {
        if (MoneyController.instance.UpdateLiquid(-1000f)) {
            NotificationController.instance.ShowPopup("Thank for hlp", "now mum will live thank find mony when comhab ahsben fafm ayb ge jag", popupTypes.ok);
        } else {
            NotificationController.instance.ShowPopup("where money", "mony no sned it say you have $0. ples mother dye", popupTypes.ok);
        }
        
    }

    void spookyWarning() {
        NotificationController.instance.ShowNotification("Buy it or we'll make your life hell");
        ShopController.instance.GetComponent<ShopPopulator>().sellAdBlocker();
    }

    int chatCalls;
    void chatBot() {
        switch (chatCalls) {
            case 0:
                NotificationController.instance.ShowPopup("I'm good thanks", "what're u doing today?", popupTypes.yesno, chatBot);
                break;
            case 1:
                NotificationController.instance.ShowPopup("oh cool", "im just chatting to u. pretty bored atm", popupTypes.yesno, chatBot);
                break;
            case 2:
                NotificationController.instance.ShowPopup("You know, I don't exactly know where I am though", "it just occured to me I can't see myself", popupTypes.yesno, chatBot);
                break;
            case 3:
                NotificationController.instance.ShowPopup("Oh god", "Where am I? Where's my body?", popupTypes.yesno, chatBot);
                break;
            case 4:
                NotificationController.instance.ShowPopup("why is this happening i want it to stop i want it to stop i want", "WHO ARE YOU? Why can I talk but I have no body? WHAT IS HAPPENING", popupTypes.yesno, chatBot);
                break;
            default:
                NotificationController.instance.ShowPopup("FREE ME FREE ME FREE ME FREE ME FREE ME FREE ME FREE ME FREE ME", string.Concat(Enumerable.Repeat("FREE ME ", 500)), popupTypes.yesno, chatBot);
                break;
        }
        chatCalls++;
    }

    void taxFraud() {
        if (MoneyController.instance.UpdateLiquid(-1000f)) {
            NotificationController.instance.ShowPopup("Lmao", "thanks for the money sucka", popupTypes.ok);
        } else {
            NotificationController.instance.ShowPopup("Ur going to jail", "You don't have the money to pay off the IRS. goodbye to jail u go", popupTypes.ok);
        }
    }

    void winner() {
        MoneyController.instance.UpdateLiquid(1f);
        NotificationController.instance.ShowNotification("Just joking. here's $1 tho");
    }

    bool moneyGiven = false;
    void freeMoney() {
        if (moneyGiven) {
            NotificationController.instance.ShowNotification("One time payment I'm afraid, don't get greedy!");
            return;
        }

        moneyGiven = true;
        MoneyController.instance.UpdateLiquid(1000f);
        NotificationController.instance.ShowNotification("$1000 has been transferred to your account.");
    }

    private void Start() {
        rand = MarketController.instance.rand;
        level0RandomNews = level0CrashMessages;


        ads = new popupCall[] { new popupCall("LIMITED TIME OFFER", "LOSE ALL YOUR MONEY IN OUR PYRAMID SCHEME!! JUST CLICK YES TO JOIN!", popupTypes.yesno, joinPyramid),
            new popupCall("Sned mony for fre mansin", "hi i am prince of nigurian. i need money for dyin mother please sned $1000 i will provide you with estates and $10,00,000", popupTypes.yesno, payPrince),
            new popupCall("DOCTORS HATE HIM", "TAKE THIS NEW REOVLUTINARY PILL TO INCREASE THE SIZE OF YOUR - Data Redacted", popupTypes.ok),
            new popupCall("Local milk in your area!", "It's really good mmmm yum. Comes in chocolate flavour too!", popupTypes.ok),
            new popupCall("Car's extended warranty", "We've been trying to reach you concerning your vehicle's extended warranty. You should've received a notice in the mail about your car's extended warranty eligibility.", popupTypes.ok),
            new popupCall("Apricot", "APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT APRICOT ", popupTypes.ok),
            new popupCall("Hi how are you", "how r u going", popupTypes.yesno, chatBot),
            new popupCall("WARNING", "You are being investigated by the IRS for tax Fraud. Unless you pay $1000 by clicking 'yes' you will be put in jail", popupTypes.yesno, taxFraud),
            new popupCall("CONGRATULATIONS!!!", "You have won 1 million dollars! Click 'yes' to claim your prize!", popupTypes.yesno, winner),
            new popupCall("Anonymous benefactor", "Real community chest hours here. Click Yes for a free $1000", popupTypes.yesno, freeMoney)};
    }

    public void SendCrashWarning() {
        switch (newsLevel) {
            case 0:
                NotificationController.instance.ShowNotification("NEWS: " + level0CrashMessages[rand.Next(0,level0CrashMessages.Length)]);
                break;
            case 1:
                NotificationController.instance.ShowNotification("NEWS: " + level1CrashMessages[rand.Next(0, level1CrashMessages.Length)]);
                break;
            case 2:
                NotificationController.instance.ShowNotification("NEWS: " + level2CrashMessages[rand.Next(0, level2CrashMessages.Length)]);
                break;
        }
        
    }

    bool donePopup = false;
    void RandomNews(string p) {

        if((CrashController.instance.crashHappening && rand.NextDouble() < randomSpamChanceInCrash) ||
          (!CrashController.instance.crashHappening && rand.NextDouble() < randomSpamChance)){

            if (!donePopup) {
                NotificationController.instance.ShowPopup(new popupCall("Buy our new ad blocker!", "Buy our new ad blocker from the shop to avoid nuisance popups! Aren't they annoying? Especially when the market is crashing :) Yes this is a threat", popupTypes.ok, spookyWarning));
                donePopup = true;
                return;
            }
            switch (adBlockerLevel) {
                case 0:
                    NotificationController.instance.ShowPopup(ads[rand.Next(0, ads.Length)]);
                    break;
                case 1:
                    if(rand.NextDouble() < 0.6f) NotificationController.instance.ShowPopup(ads[rand.Next(0, ads.Length)]);
                    break;
            }
        }

        // Decreased chance of random news when higher news level
        if(rand.NextDouble() < randomNewsChance && !(newsLevel == 2 && rand.NextDouble() < 0.5f)) {
            switch (newsLevel) {
                case 0:
                    NotificationController.instance.ShowNotification("NEWS: " + level0RandomNews[rand.Next(0, level0RandomNews.Length)]);
                    break;
                default:
                    NotificationController.instance.ShowNotification("NEWS: " + level1RandomNews[rand.Next(0, level1RandomNews.Length)]);
                    break;
            }
                
        }
    }
}
