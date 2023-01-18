using Firebase;
using Firebase.Analytics;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private static FirebaseManager d_fire = null;

    public static FirebaseManager firebaseManager
    {
        get
        {
            if (d_fire == null)
            {
                d_fire = FindObjectOfType(typeof(FirebaseManager)) as FirebaseManager;
                if (d_fire == null)
                {
                    GameObject obj = new GameObject("Firebase");
                    d_fire = obj.AddComponent(typeof(FirebaseManager)) as FirebaseManager;
                }
            }
            return d_fire;
        }
    }

    public FirebaseManager() { }

    private void Awake()
    {        
        DontDestroyOnLoad(this);

        //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp, 
                // FirebaseApp에 대한 참조를 작성하고 유지하십시오.
                // where app is a Firebase.FirebaseApp property of your application class.
                // 여기서 app은 애플리케이션 클래스의 Firebase.FirebaseApp 속성입니다.

                //   app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                // Firebase를 앱에서 사용할 준비가되었는지 표시하려면 여기에 플래그를 설정하십시오.
                Debug.Log("Firebase 초기화 완료");
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    //public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    //{
    //    UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    //}

    //public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    //{
    //    UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    //}

    public void FirstOpen()
    {
        FirebaseAnalytics.LogEvent("first_open");        
    }

    //public void LobbyHeroPurchaseUpgrade(string Hero_Name, int Pay_Jewel, int Pay_Token)
    //{
    //    FirebaseAnalytics.LogEvent("Lobby_Hero_Purchase/Upgrade_" + Hero_Name + Pay_Jewel + Pay_Token);
    //}
    public void LobbyHeroPurchaseUpgrade(string Hero_Name, int Pay_Jewel, int Pay_Token)
    {
        FirebaseAnalytics.LogEvent("Lobby_Hero_Purchase_Upgrade", new Parameter("Hero_Name", Hero_Name), new Parameter("Pay_Jewel", Pay_Jewel), new Parameter("Pay_Token", Pay_Token));
    }

public void LobbySkinPurchase(string Skin_Name)
    {
        FirebaseAnalytics.LogEvent("Lobby_Skin_Purchase", new Parameter("Skin_Name", Skin_Name));
    }

    public void LobbyRoulettePay(string Purchase_Method, int Pay_RoulletteCoin)
    {
        FirebaseAnalytics.LogEvent("Lobby_Roulette_Pay", new Parameter("Purchase_Method", Purchase_Method), new Parameter("Pay_RoulletteCoin", Pay_RoulletteCoin));
    }

    public void ForgeItemShorten(string Item_Name, string Purchase_Method, int Pay_Jewel)
    {
        FirebaseAnalytics.LogEvent("Forge_Item_Shorten", new Parameter("Item_Name", Item_Name), new Parameter("Purchase_Method", Purchase_Method), new Parameter("Pay_Jewel", Pay_Jewel));
    }

    public void ForgeItemUpgrade(string Item_Name, int Item_Lv, int Pay_Token)
    {
        FirebaseAnalytics.LogEvent("Forge_Item_Upgrade", new Parameter("Item_Name", Item_Name), new Parameter("Item_Lv", Item_Lv), new Parameter("Pay_Token", Pay_Token));
    }

    public void IngameRetryPurchase(string Purchase_Method, int Pay_Jewel)
    {
        FirebaseAnalytics.LogEvent("Ingame_Retry_Purchase", new Parameter("Purchase_Method", Purchase_Method), new Parameter("Pay_Jewel", Pay_Jewel));
    }

    public void IngameTreasureChestOpen(string Purchase_Method, int Pay_Jewel)
    {
        FirebaseAnalytics.LogEvent("Ingame_TreasureChest_Open", new Parameter("Purchase_Method", Purchase_Method), new Parameter("Pay_Jewel", Pay_Jewel));
    }

    public void ResultWatchAD(string Purchase_Method)
    {
        FirebaseAnalytics.LogEvent("Result_Watch_AD", new Parameter("Purchase_Method", Purchase_Method));
    }

    public void ShopProductPurchase(string Product_Name)
    { 
        FirebaseAnalytics.LogEvent("Shop_Product_Purchase", new Parameter("Product_Name", Product_Name));
    }
}
