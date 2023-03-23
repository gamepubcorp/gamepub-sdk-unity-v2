﻿
#if UNITY_ANDROID
using UnityEngine;

namespace GamePub.PubSDK
{
    public class NativeInterface
    {
#if UNITY_EDITOR
        static AndroidJavaObject pubSdkWrapper = null;
#else
        static AndroidJavaObject pubSdkWrapper = new AndroidJavaObject("com.gamepubcorp.sdk.unitywrapper.PubSdkWrapper");
#endif
        static NativeInterface()
        {
            var _ = GamePubSDK.Ins;
        }

        public static void SetupSDK(string identifier,
                                    string projectId)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            object[] param = new object[2];
            param[0] = identifier;            
            param[1] = projectId;

            if (pubSdkWrapper != null)
                pubSdkWrapper.Call("setupSDK", param);
        }

        public static void Login(string identifier,
                                 PubLoginType loginType,
                                 PubAccountServiceType serviceType)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[3];
            param[0] = identifier;
            param[1] = (int)loginType;
            param[2] = (int)serviceType;

            if (pubSdkWrapper != null)
                pubSdkWrapper.Call("login", param);
        }

        public static void AutoLogin(string identifier)
        {
			if (!Application.isPlaying) { return; }
			if (IsInvalidRuntime(identifier)) { return; }

			object[] param = new object[1];
			param[0] = identifier;

			if (pubSdkWrapper != null)
				pubSdkWrapper.Call("autoLogin", param);
		}

		public static void SetPushToken(string identifier, 
                                        string pushToken)
		{
			if (!Application.isPlaying) { return; }
			if (IsInvalidRuntime(null)) { return; }

			object[] param = new object[2];
			param[0] = identifier;
            param[1] = pushToken;

			if (pubSdkWrapper != null)
				pubSdkWrapper.Call("setPushToken", param);
		}

		public static void SetPushConfig(string identifier,
										 PubPushConfig pushConfig)
		{
			if (!Application.isPlaying) { return; }
			if (IsInvalidRuntime(null)) { return; }

			object[] param = new object[4];
			param[0] = identifier;
			param[1] = pushConfig.AgreedPush;
			param[2] = pushConfig.AgreedNightPush;
			param[3] = pushConfig.AgreedAdPush;

			if (pubSdkWrapper != null)
				pubSdkWrapper.Call("setPushConfig", param);
		}

		public static void Logout(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

			object[] param = new object[1];
			param[0] = identifier;

			if (pubSdkWrapper != null)
                pubSdkWrapper.Call("logout", param);
        }

        public static void Withdraw(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

			object[] param = new object[1];
			param[0] = identifier;

			if (pubSdkWrapper != null)
				pubSdkWrapper.Call("withdraw", param);
		}

		public static void InitBilling(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[1];
            param[0] = identifier;

            if (pubSdkWrapper != null)
                pubSdkWrapper.Call("initBilling", param);
        }
        
        public static void InAppPurchase(string identifier,
                                         string pid)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[2];
            param[0] = identifier;
            param[1] = pid;            

            if (pubSdkWrapper != null)
                pubSdkWrapper.Call("purchaseLaunch", param);
        }

		public static void RetryPurchase(string identifier)
		{
			if (!Application.isPlaying) { return; }
			if (IsInvalidRuntime(identifier)) { return; }

			object[] param = new object[1];
			param[0] = identifier;

			if (pubSdkWrapper != null)
				pubSdkWrapper.Call("retryPurchase", param);
		}

		public static void RestoreRefund(string identifier)
		{
			if (!Application.isPlaying) { return; }
			if (IsInvalidRuntime(identifier)) { return; }

			object[] param = new object[1];
			param[0] = identifier;

			if (pubSdkWrapper != null)
				pubSdkWrapper.Call("restoreRefund", param);
		}

		public static void OpenTerms(string identifier)
		{
			if (!Application.isPlaying) { return; }
			if (IsInvalidRuntime(identifier)) { return; }

			object[] param = new object[1];
			param[0] = identifier;

			if (pubSdkWrapper != null)
				pubSdkWrapper.Call("openTerms", param);
		}

		public static void OpenImageBanner(string identifier)
		{
			if (!Application.isPlaying) { return; }
			if (IsInvalidRuntime(identifier)) { return; }

			object[] param = new object[1];
			param[0] = identifier;

			if (pubSdkWrapper != null)
				pubSdkWrapper.Call("openImageBanner", param);
		}

		private static bool IsInvalidRuntime(string identifier)
        {
            return Helpers.IsInvalidRuntime(identifier, RuntimePlatform.Android);
        }
    }
}

#endif