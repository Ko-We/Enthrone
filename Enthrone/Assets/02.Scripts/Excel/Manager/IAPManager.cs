using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IAPManager m_instance;

    public static IAPManager Instance
    {
        get
        {
            if (m_instance != null) return m_instance;

            m_instance = FindObjectOfType<IAPManager>();

            if (m_instance == null) m_instance = new GameObject("IAP Manager").AddComponent<IAPManager>();
            return m_instance;
        }
    }
    MyObject myChar;
    private IStoreController storeController;       //구매 과정을 제어하는 함수 제공
    private IExtensionProvider storeExtensionProvider;      //여러 플랫폼을 위한 확장 처리 제공

    public const string ProductSpecialPack = "special_pack";         //Consumable;
    public const string ProductLuxuryPack = "luxury_pack";         //Consumable;
    public const string ProductStarterPack = "starter_pack";         //Consumable;
    public const string ProductGemId_1 = "dia_1";                       //Consumable;
    public const string ProductGemId_2 = "dia_2";                       //Consumable;
    public const string ProductGemId_3 = "dia_3";                       //Consumable;
    public const string ProductAD_Skip = "ad_skip";                       //Consumable;

    //스페셜팩
    private const string _iOS_SpecialPack_Id = "com.enthrone.app.special";
    private const string _android_SpecialPack_Id = "special_pack";
    //럭셔리팩
    private const string _iOS_LuxuryPack_Id = "com.enthrone.app.luxury";
    private const string _android_LuxuryPack_Id = "luxury_pack";
    //스타터팩
    private const string _iOS_StarterPack_Id = "com.enthrone.app.starter";
    private const string _android_StarterPack_Id = "starter_pack";
    //다이아 I
    private const string _iOS_Gem1_Id = "com.enthrone.app.gem1";
    private const string _android_Gem1_Id = "dia_1";
    //다이아 II
    private const string _iOS_Gem2_Id = "com.enthrone.app.gem2";
    private const string _android_Gem2_Id = "dia_2";
    //다이아 III
    private const string _iOS_Gem3_Id = "com.enthrone.app.gem3";
    private const string _android_Gem3_Id = "dia_3";
    //광고제거
    private const string _iOS_ADSkip_Id = "com.enthrone.app.adskip";
    private const string _android_ADSkip_Id = "ad_skip";

    public bool IsInitialized => storeController != null && storeExtensionProvider != null;
    //{ //위에 =>형식과 동일한 거임
    //    get
    //    {
    //        if (storeController != null && storeExtensionProvider != null)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //}
    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        InitUnityIAP();
        myChar = MyObject.MyChar;
    }

    void InitUnityIAP()
    {
        if (IsInitialized) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

#if UNITY_ANDROID
        builder.AddProduct(_android_SpecialPack_Id, ProductType.Consumable, new IDs()
        {
                { _iOS_SpecialPack_Id, AppleAppStore.Name },
                { _android_SpecialPack_Id, GooglePlay.Name }
        });

        builder.AddProduct(_android_LuxuryPack_Id, ProductType.Consumable, new IDs()
        {
                { _iOS_LuxuryPack_Id, AppleAppStore.Name },
                { _android_LuxuryPack_Id, GooglePlay.Name }
        });

        builder.AddProduct(_android_Gem1_Id, ProductType.Consumable, new IDs()
        {
                { _iOS_Gem1_Id, AppleAppStore.Name },
                { _android_Gem1_Id, GooglePlay.Name }
        });

        builder.AddProduct(_android_StarterPack_Id, ProductType.Consumable, new IDs()
        {
                { _iOS_StarterPack_Id, AppleAppStore.Name },
                { _android_StarterPack_Id, GooglePlay.Name }
        });

        builder.AddProduct(_android_Gem1_Id, ProductType.Consumable, new IDs(){
                { _iOS_Gem1_Id, AppleAppStore.Name },
                { _android_Gem1_Id, GooglePlay.Name }
        });

        builder.AddProduct(_android_Gem2_Id, ProductType.Consumable, new IDs()
        {
                { _iOS_Gem2_Id, AppleAppStore.Name },
                { _android_Gem2_Id, GooglePlay.Name }
        });

        builder.AddProduct(_android_Gem3_Id, ProductType.Consumable, new IDs()
        {
                { _iOS_Gem3_Id, AppleAppStore.Name },
                { _android_Gem3_Id, GooglePlay.Name }
        });

        builder.AddProduct(_android_ADSkip_Id, ProductType.Consumable, new IDs()
        {
                { _iOS_ADSkip_Id, AppleAppStore.Name },
                { _android_ADSkip_Id, GooglePlay.Name }
        });

        UnityPurchasing.Initialize(this, builder);
#endif
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("유니티 IAP 초기화 성공");
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"유니티 IAP 초기화 실패 {error}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"구매 성공 - ID{args.purchasedProduct.definition.id}");

        if (args.purchasedProduct.definition.id == ProductSpecialPack)
        {
            Debug.Log("스페셜팩 처리...");
            LobbiManager.instance.PurchaseCompleat(0);
            FirebaseManager.firebaseManager.ShopProductPurchase("special_pack");
        }
        else if (args.purchasedProduct.definition.id == ProductLuxuryPack)
        {
            Debug.Log("럭셔리팩 처리...");
            LobbiManager.instance.PurchaseCompleat(1);
            FirebaseManager.firebaseManager.ShopProductPurchase("luxury_pack");
        }
        else if (args.purchasedProduct.definition.id == ProductStarterPack)
        {
            Debug.Log("스타터팩 처리...");
            LobbiManager.instance.PurchaseCompleat(2);
            FirebaseManager.firebaseManager.ShopProductPurchase("starter_pack");
        }
        else if (args.purchasedProduct.definition.id == ProductGemId_1)
        {
            Debug.Log("다이아_1 처리...");
            LobbiManager.instance.GemPurchase();
            FirebaseManager.firebaseManager.ShopProductPurchase("dia_1");
        }
        else if (args.purchasedProduct.definition.id == ProductGemId_2)
        {
            Debug.Log("다이아_2 처리...");
            LobbiManager.instance.GemPurchase();
            FirebaseManager.firebaseManager.ShopProductPurchase("dia_2");
        }
        else if (args.purchasedProduct.definition.id == ProductGemId_3)
        {
            Debug.Log("다이아_3 처리...");
            LobbiManager.instance.GemPurchase();
            FirebaseManager.firebaseManager.ShopProductPurchase("dia_3");
        }
        else if (args.purchasedProduct.definition.id == ProductAD_Skip)
        {
            Debug.Log("광고스킵 처리...");
            myChar.ADClearCheck = true;
            myChar.SaveADClearCheck();
            FirebaseManager.firebaseManager.ShopProductPurchase("ad_skip");
        }

        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning($"구매 실패 - {product.definition.id}, {reason}");
    }

    public void Purchase(string productId)
    {
        Debug.Log("IAP구매창");
        if (!IsInitialized) return;

        var product = storeController.products.WithID(productId);

        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"구매 시도 - {product.definition.id}");
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log($"구매 시도 불가 - {product.definition.id}");
        }
    }

    //iOS는 구매복구 코드를 꼭 넣어줘야함
    public void RestorePurchase()
    {
        if (!IsInitialized) return;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("구매 복구 시도");

            var appleExt = storeExtensionProvider.GetExtension<IAppleExtensions>();

            appleExt.RestoreTransactions(result => Debug.Log($"구매 복구 시도 결과 - {result}"));
        }
    }

    //구매 시도한지 체크하는 부분
    public bool HadPurchased(string productId)
    {
        if (!IsInitialized) return false;

        var product = storeController.products.WithID(productId);

        if (product != null)
        {
            return product.hasReceipt;
        }
        return false;
    }

    public Product GetProduct(string _productId)
    {
        return storeController.products.WithID(_productId);
    }
}
