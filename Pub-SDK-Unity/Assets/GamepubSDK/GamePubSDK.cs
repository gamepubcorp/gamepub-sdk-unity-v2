using System;
using UnityEngine;

namespace GamePub.PubSDK
{
    public class GamePubSDK : MonoBehaviour
    {
        private static GamePubSDK instance;
        //private bool isSetup = false;

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

        public void SetupSDK(Action<Result<PubSetupResult>> action)
        {           
            if (string.IsNullOrEmpty(GamePubSDKSettings.ProjectId))
            {
                throw new System.Exception("Gamepub SDK ProjectId is not set.");
            }
            GamePubAPI.SetupSDK(GamePubSDKSettings.ProjectId, action);
            //isSetup = true;
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

		public void Withdraw(Action<Result<PubUnit>> action)
		{
			GamePubAPI.Withdraw(action);
		}

		public void InitBilling(Action<Result<PubInitBillingResult>> action)
        {
            GamePubAPI.InitBilling(action);
        }

        public void Purchase(string productId,
                             string channelId,
                             string characterId,
                             Action<Result<PubPurchaseResult>> action)
        {
            GamePubAPI.Purchase(productId, channelId, characterId, action);
        }

		public void RetryPurchase(string channelId,
                                  string characterId,
                                  Action<Result<PubRetryPurchaseResult>> action)
		{
			GamePubAPI.RetryPurchase(channelId, characterId, action);
		}

		public void OpenVoided(string channelId,
							   string characterId,
							   Action<Result<PubVoidedResult>> action)
		{
			GamePubAPI.OpenVoided(channelId, characterId, action);
		}

		public void OpenTerms(Action<Result<PubUnit>> action)
		{
			GamePubAPI.OpenTerms(action);
		}

		public void OpenImageBanner(Action<Result<PubUnit>> action)
		{
			GamePubAPI.OpenImageBanner(action);
		}

		public void OpenHelp(Action<Result<PubUnit>> action)
		{
			GamePubAPI.OpenHelp(action);
		}

		public void SetPushToken(string pushToken,
								Action<Result<PubUnit>> action)
		{
			GamePubAPI.SetPushToken(pushToken, action);
		}

		public void SetPushConfig(bool agreedPush,
								  bool agreedNightPush,
								  Action<Result<PubUnit>> action)
		{
			GamePubAPI.SetPushConfig(agreedPush, agreedNightPush, action);
		}

		public void OnApiOk(string result)
        {
            //result.SuccessLog();            
            GamePubAPI._OnApiOk(result);
        }

        public void OnApiError(string result)
        {
            //result.ErrorLog();
            GamePubAPI._OnApiError(result);
        }        
    }
}