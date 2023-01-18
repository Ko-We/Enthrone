using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
//using CodeStage.AntiCheat.ObscuredTypes;

public class InAppPurchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    public GameManager _gameManager;

    #region 상품ID
    // 상품ID는 구글 개발자 콘솔에 등록한 상품ID와 동일하게 해주세요.
    /*
#if UNITY_IOS
    public const string productId1 = "fillline_Noads";
#elif UNITY_ANDROID
    public const string productId1 = "fillline_noads";
#endif    
    */
    public const string productId1 = "cross_noads";
    public const string productId2 = "cross_hint_20";
    public const string productId3 = "cross_hint_70";
    public const string productId4 = "cross_hint_150";
    #endregion

    void Start()
    {
       // InitializePurchasing();
    }
    private bool IsInitialized()
    {
        return (storeController != null && extensionProvider != null);
    }
    
    public Text[] price;
    public GameObject refundText;
    public void GetPrice()
    {
        //if (_gameManager._languageManager.language == "KR")
        //{
        //    refundText.SetActive(true);
        //}
        //else
        //{
        //    refundText.SetActive(false);
        //}

        Product product = storeController.products.WithID(productId2);
        price[0].text = product.metadata.localizedPriceString;
        product = storeController.products.WithID(productId3);
        price[1].text = product.metadata.localizedPriceString;
        product = storeController.products.WithID(productId4);
        price[2].text = product.metadata.localizedPriceString;
    }

    
    

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;
        
        var module = StandardPurchasingModule.Instance();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct(productId1, ProductType.Consumable, new IDs
        {
            { productId1, AppleAppStore.Name },
            { productId1, GooglePlay.Name },
        });

        builder.AddProduct(productId2, ProductType.Consumable, new IDs
        {
            { productId2, AppleAppStore.Name },
            { productId2, GooglePlay.Name }, }
        );

        builder.AddProduct(productId3, ProductType.Consumable, new IDs
        {
            { productId3, AppleAppStore.Name },
            { productId3, GooglePlay.Name },
        });
        builder.AddProduct(productId4, ProductType.Consumable, new IDs
        {
            { productId4, AppleAppStore.Name },
            { productId4, GooglePlay.Name },
        });

        UnityPurchasing.Initialize(this, builder);
        
    }

    public void BuyProductID(string productId)
    {

        try
        {
            if (IsInitialized())
            {
                Product p = storeController.products.WithID(productId);

                if (p != null && p.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
                    storeController.InitiatePurchase(p);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }

    public void RestorePurchase()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = extensionProvider.GetExtension<IAppleExtensions>();
            
            apple.RestoreTransactions
                (
                    (result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
                );
              
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
    {
        Debug.Log("OnInitialized : PASS");

        storeController = sc;
        extensionProvider = ep;
    }

    public void OnInitializeFailed(InitializationFailureReason reason)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

        //ObscuredPrefs.SetBool("PAID", true);
        //switch (args.purchasedProduct.definition.id)
        //{
        //    case productId1:
        //        _gameManager.NoAds();
        //        _gameManager.HintCountUp(20);
        //        _gameManager._uiManager.OpenWindowSet(-1);
        //        ObscuredPrefs.SetInt("BUY_PRODUCT_0)", ObscuredPrefs.GetInt("BUY_PRODUCT_0)", 0) + 1);
        //        //_gameManager._kochavaManager.Purchase(0);

        //        break;
        //    case productId2:
        //        _gameManager.HintCountUp(20);
        //        ObscuredPrefs.SetInt("BUY_PRODUCT_1)", ObscuredPrefs.GetInt("BUY_PRODUCT_1)", 0) + 1);
        //        //_gameManager._kochavaManager.Purchase(1);

        //        break;
        //    case productId3:
        //        _gameManager.HintCountUp(70);
        //        ObscuredPrefs.SetInt("BUY_PRODUCT_2)", ObscuredPrefs.GetInt("BUY_PRODUCT_2)", 0) + 1);
        //       // _gameManager._kochavaManager.Purchase(2);

        //        break;

        //    case productId4:
        //        _gameManager.HintCountUp(150);
        //        ObscuredPrefs.SetInt("BUY_PRODUCT_3)", ObscuredPrefs.GetInt("BUY_PRODUCT_3)", 0) + 1);
        //       // _gameManager._kochavaManager.Purchase(3);

        //        break;
        //}

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
   
