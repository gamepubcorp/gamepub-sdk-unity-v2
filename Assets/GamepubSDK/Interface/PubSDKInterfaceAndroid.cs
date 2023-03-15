
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

        public static void Logout()
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }            

            if (pubSdkWrapper != null)
                pubSdkWrapper.Call("logout");
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

        private static bool IsInvalidRuntime(string identifier)
        {
            return Helpers.IsInvalidRuntime(identifier, RuntimePlatform.Android);
        }
    }
}

#endif