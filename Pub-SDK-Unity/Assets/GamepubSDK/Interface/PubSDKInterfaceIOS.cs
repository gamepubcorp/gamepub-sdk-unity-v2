
#if UNITY_IOS

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Reflection;

namespace GamePub.PubSDK
{
    public class NativeInterface
    {
        static NativeInterface()
        {
            var _ = GamePubSDK.Ins;
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_setup(string identifier,                                                 
                                                 string projectId);
        public static void SetupSDK(string identifier,                                    
                                    string projectId)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            pub_sdk_setup(identifier, projectId);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_login(string identifier,
                                                 int loginType,
                                                 int serviceType);
        public static void Login(string identifier,
                                 PubLoginType loginType,
                                 PubAccountServiceType serviceType)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_login(identifier, (int)loginType, (int)serviceType);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_autoLogin(string identifier);
        public static void AutoLogin(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_autoLogin(identifier);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_logout(string identifier);
        public static void Logout(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_logout(identifier);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_withdraw(string identifier);
        public static void Withdraw(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_withdraw(identifier);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_initBilling(string identifier);
        public static void InitBilling(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_initBilling(identifier);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_purchase(string identifier,
                                                    string productId,
                                                    string channelId,
                                                    string characterId);
        public static void Purchase(string identifier,
                                    string productId,
                                    string channelId,
                                    string characterId)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_purchase(identifier, productId, channelId, characterId);
        }                

        [DllImport("__Internal")]
        private static extern void pub_sdk_openTerms(string identifier);
        public static void OpenTerms(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_openTerms(identifier);
        }        

        [DllImport("__Internal")]
        private static extern void pub_sdk_openImageBanner(string identifier);
        public static void OpenImageBanner(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_openImageBanner(identifier);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_openHelp(string identifier);
        public static void OpenHelp(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_openHelp(identifier);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_setPushToken(string identifier,
                                                        string pushToken);
        public static void SetPushToken(string identifier, string pushToken)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            pub_sdk_setPushToken(identifier, pushToken);
        }

        [DllImport("__Internal")]
        private static extern void pub_sdk_setPushConfig(string identifier,
                                                         bool agreedPush,
                                                         bool agreedNightPush);
        public static void SetPushConfig(string identifier,
                                         bool agreedPush,
                                         bool agreedNightPush)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            pub_sdk_setPushConfig(identifier, agreedPush, agreedNightPush);
        }        

        private static bool IsInvalidRuntime(string identifier)
        {
            return Helpers.IsInvalidRuntime(identifier, RuntimePlatform.IPhonePlayer);
        }
    }
}
#endif