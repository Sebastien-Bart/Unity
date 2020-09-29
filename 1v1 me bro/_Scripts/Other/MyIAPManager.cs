using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;

public class MyIAPManager : MonoBehaviour, IStoreListener
{
    public MainMenu mainMenu;

    public static string fiftyBrocoinsID = "com.sebmakesgames.onevonemebro.fifty_brocoins";
    public static string fullAccessID = "com.sebmakesgames.onevonemebro.full_access";

    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    private void Start()
    {
        InitializePurchasing();
    }

    private void InitializePurchasing()
    {
        if (storeController != null && storeExtensionProvider != null)
            return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(fiftyBrocoinsID, ProductType.Consumable);
        builder.AddProduct(fullAccessID, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyFiftyBrocoins()
    {
        BuyProductID(fiftyBrocoinsID);
    }

    public void BuyFullAccess()
    {
        BuyProductID(fullAccessID);
    }

    private void BuyProductID(string productId)
    {
        if (storeController != null && storeExtensionProvider != null)
        {
            Product product = storeController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                storeController.InitiatePurchase(product);
            }
            else
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
        }
        else
            Debug.Log("BuyProductID FAIL. Not initialized.");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (string.Equals(args.purchasedProduct.definition.id, fiftyBrocoinsID, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerData.nbBrocoins += 50;
            PlayerData.SaveBrocoinsAndAccess();
            mainMenu.StartCoroutine(MakeNSoundsOfBrocoins(5)); 
            return PurchaseProcessingResult.Complete;
        }
        else if (string.Equals(args.purchasedProduct.definition.id, fullAccessID, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerData.hasFullAccess = true;
            mainMenu.UpdateToFullAccessDisplay();
            PlayerData.SaveBrocoinsAndAccess();
            mainMenu.StartCoroutine(MakeNSoundsOfBrocoins(10));
            GetComponent<BroCoinMenu>().CheckIfFullAccessAndAdjustButtons();
            return PurchaseProcessingResult.Complete;
        }
        else
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));

        return PurchaseProcessingResult.Complete;
    }

    private IEnumerator MakeNSoundsOfBrocoins(int n)
    {
        for (int i = 0; i < n; i++)
        {
            AudioManagerForOneGame.am.PlaySound("OneBrocoin");
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

}
