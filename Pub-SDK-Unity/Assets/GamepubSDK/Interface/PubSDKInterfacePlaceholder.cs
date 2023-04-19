//  Copyright (c) 2020-present, GamePub Corporation. All rights reserved.

#if !UNITY_IOS && !UNITY_ANDROID
namespace GamePub.PubSDK
{
    public class NativeInterface
    {
        public static void SetupSDK(string identifier, string sdkAppId) { }
        public static void Login(string identifier, PubLoginType loginType, PubAccountServiceType serviceType) { }
        public static void AutoLogin(string identifier) { }
        public static void Logout(string identifier) { }
        public static void Withdraw(string identifier) { }
        public static void InitBilling(string identifier) { }
        public static void Purchase(string identifier, string productId, string channelId, string characterId) { }
        public static void RetryPurchase(string identifier, string channelId, string characterId) { }
        public static void OpenVoided(string identifier, string channelId, string characterId) { }
        public static void OpenTerms(string identifier) { }
        public static void OpenImageBanner(string identifier) { }
        public static void OpenCustomerCenter(string identifier) { }
        public static void SetPushToken(string identifier, string pushToken) { }
        public static void SetPushConfig(string identifier, PubPushConfig pushConfig) { }
    }
}
#endif