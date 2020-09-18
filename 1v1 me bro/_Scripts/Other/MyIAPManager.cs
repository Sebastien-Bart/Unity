using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class MyIAPManager : MonoBehaviour, IStoreListener
{
    public static string fiftyBrocoinsID = "com.SebMakesGames.OneVOneMeBro.FiftyBrocoins";
    public static string fullAccessID = "com.SebMakesGames.OneVOneMeBro.FullAccess";

    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    private void Start()
    {
        InitializePurchasing();
    }

    private void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (storeController != null && storeExtensionProvider != null)
            return;

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(fiftyBrocoinsID, ProductType.Consumable);
        builder.AddProduct(fullAccessID, ProductType.NonConsumable);

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
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
        // If Purchasing has been initialized ...
        if (storeController != null && storeExtensionProvider != null)
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = storeController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                storeController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");
        // Overall Purchasing system, configured with products for this application.
        storeController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (string.Equals(args.purchasedProduct.definition.id, fiftyBrocoinsID, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // The consumable item has been successfully purchased
            PlayerData.nbBrocoins += 50;
            PlayerData.SaveBrocoinsAndAccess();
            AudioManagerForOneGame.am.PlaySound("OneBrocoin");
        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (string.Equals(args.purchasedProduct.definition.id, fullAccessID, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            PlayerData.hasFullAccess = true;
            PlayerData.SaveBrocoinsAndAccess();
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

}
