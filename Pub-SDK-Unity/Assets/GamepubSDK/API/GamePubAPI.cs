﻿using System;
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

        public static void AutoLogin(Action<Result<PubUnit>> action)
        {
			var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
			NativeInterface.AutoLogin(identifier);
		}

		public static void SetPushToken(string pushToken,
                                        Action<Result<PubUnit>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
			NativeInterface.SetPushToken(identifier, pushToken);
		}

		public static void SetPushConfig(PubPushConfig pushConfig, 
                                         Action<Result<PubUnit>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
			NativeInterface.SetPushConfig(identifier, pushConfig);
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
			NativeInterface.RetryPurchase(identifier, channelId, characterId);
		}

		public static void OpenVoided(string channelId,
									  string characterId,
									  Action<Result<PubVoidedResult>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubVoidedResult>(action));
			NativeInterface.OpenVoided(identifier, channelId, characterId);
		}

		public static void OpenTerms(Action<Result<PubTermsResult>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubTermsResult>(action));
			NativeInterface.OpenTerms(identifier);
		}

		public static void OpenImageBanner(Action<Result<PubUnit>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
			NativeInterface.OpenImageBanner(identifier);
		}

		public static void OpenCustomerCenter(Action<Result<PubUnit>> action)
		{
			var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
			NativeInterface.OpenCustomerCenter(identifier);
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