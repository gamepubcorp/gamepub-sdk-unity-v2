using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePub.PubSDK
{
    public class GamePubAPI
    {
        public static Dictionary<String, FlattenAction> actions =
            new Dictionary<string, FlattenAction>();

        public static void SetupSDK(string projectId,
                                    Action<Result<PubSetupResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PubSetupResult>(action));
            NativeInterface.SetupSDK(identifier, projectId);
        }

        public static void Login(PubLoginType loginType,
                                 PubAccountServiceType serviceType,
                                 Action<Result<PubLoginResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PubLoginResult>(action));
            NativeInterface.Login(identifier, loginType, serviceType);
        }

        public static void AutoLogin(Action<Result<PubLoginResult>> action)
        {
			var identifier = AddAction(FlattenAction.JsonFlatten<PubLoginResult>(action));
			NativeInterface.AutoLogin(identifier);
		}

		public static void Logout(Action<Result<PubUnit>> action)
        {
			var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
			NativeInterface.Logout(identifier);
        }

		public static void Withdraw(Action<Result<PubUnit>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
			NativeInterface.Withdraw(identifier);
		}

		public static void InitBilling(Action<Result<PubInitBillingResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PubInitBillingResult>(action));
            NativeInterface.InitBilling(identifier);
        }

        public static void Purchase(string productId,
                                    string channelId,
                                    string characterId,
                                    Action<Result<PubPurchaseResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PubPurchaseResult>(action));
            NativeInterface.Purchase(identifier, productId, channelId, characterId);
        }

		public static void RetryPurchase(string channelId,
                                         string characterId,
                                         Action<Result<PubRetryPurchaseResult>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubRetryPurchaseResult>(action));

#if UNITY_ANDROID
			NativeInterface.RetryPurchase(identifier, channelId, characterId);
#endif
		}

		public static void OpenVoided(string channelId,
									  string characterId,
									  Action<Result<PubVoidedResult>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubVoidedResult>(action));

#if UNITY_ANDROID
			NativeInterface.OpenVoided(identifier, channelId, characterId);
#endif
		}

		public static void OpenTerms(Action<Result<PubUnit>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
			NativeInterface.OpenTerms(identifier);
		}

		static string AddAction(FlattenAction action)
        {
            var identifier = Guid.NewGuid().ToString();
            actions.Add(identifier, action);
            return identifier;
        }

        static FlattenAction PopActionFromPayload(CallbackMessageForUnity payload)
        {
            var identifier = payload.Identifier;
            if (identifier == null)
            {
                return null;
            }
            FlattenAction action = null;
            if (actions.TryGetValue(identifier, out action))
            {
                actions.Remove(identifier);
                return action;
            }
            return null;
        }        

        public static void _OnApiOk(string result)
        {
            var payload = CallbackMessageForUnity.FromJson(result);
            var action = PopActionFromPayload(payload);
            if (action != null)
            {
                action.CallOk(payload.Value);
            }
        }

        public static void _OnApiError(string result)
        {
            var payload = CallbackMessageForUnity.FromJson(result);
            var action = PopActionFromPayload(payload);
            if (action != null)
            {
                action.CallError(payload.Value);
            }
        }        
    }

}