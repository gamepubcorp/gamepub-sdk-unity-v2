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
                                    Action<Result<PubUnit>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PubUnit>(action));
            NativeInterface.SetupSDK(identifier, projectId);
        }

        public static void Login(PubLoginType loginType,
                                 PubAccountServiceType serviceType,
                                 Action<Result<PubLoginResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PubLoginResult>(action));

            NativeInterface.Login(identifier, loginType, serviceType);
        }

        public static void Logout()
        {            
            NativeInterface.Logout();
        }

        public static void InitBilling(Action<Result<PubInAppListResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PubInAppListResult>(action));
            NativeInterface.InitBilling(identifier);
        }

        public static void InAppPurchase(string pid,                                         
                                         Action<Result<PubPurchaseResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PubPurchaseResult>(action));
            NativeInterface.InAppPurchase(identifier, pid);
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