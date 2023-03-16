using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    public class GamePubSDK : MonoBehaviour
    {
        private static GamePubSDK instance;
        private bool isSetup = false;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public static GamePubSDK Ins
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("GamePubSDK");
                    instance = go.AddComponent<GamePubSDK>();                    
                }
                return instance;
            }            
        }        

        public void SetupSDK(Action<Result<PubUnit>> action)
        {           
            if (string.IsNullOrEmpty(GamePubSDKSettings.ProjectId))
            {
                throw new System.Exception("Gamepub SDK ProjectId is not set.");
            }
            GamePubAPI.SetupSDK(GamePubSDKSettings.ProjectId, action);
            isSetup = true;
        }

        public void Login(PubLoginType loginType,
                          PubAccountServiceType serviceType,
                          Action<Result<PubLoginResult>> action)
        {            
            GamePubAPI.Login(loginType, serviceType, action);
        }

		public void AutoLogin(Action<Result<PubUnit>> action)
		{
			GamePubAPI.AutoLogin(action);
		}

		public void Logout(Action<Result<PubUnit>> action)
        {            
            GamePubAPI.Logout(action);            
        }

        public void InitBilling(Action<Result<PubInAppListResult>> action)
        {
            GamePubAPI.InitBilling(action);
        }

        public void InAppPurchase(string pid,                                  
                                  Action<Result<PubPurchaseResult>> action)
        {
            GamePubAPI.InAppPurchase(pid, action);
        }

		public void RestorePurchase(Action<Result<PubPurchaseResult>> action)
		{
			GamePubAPI.RestorePurchase(action);
		}

		public void OpenTerms(Action<Result<PubTermsResult>> action)
		{
			GamePubAPI.OpenTerms(action);
		}

		public void OpenImageBanner(Action<Result<PubUnit>> action)
		{
			GamePubAPI.OpenImageBanner(action);
		}

		public void OnApiOk(string result)
        {
            result.SuccessLog();            
            GamePubAPI._OnApiOk(result);
        }

        public void OnApiError(string result)
        {
            result.ErrorLog();
            GamePubAPI._OnApiError(result);
        }        
    }
}