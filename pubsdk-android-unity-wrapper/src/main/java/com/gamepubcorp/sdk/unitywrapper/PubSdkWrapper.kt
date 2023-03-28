package com.gamepubcorp.sdk.unitywrapper

import io.github.gamepubcorp.api.PubApiClient
import io.github.gamepubcorp.api.PubApiClientBuilder
import com.gamepubcorp.sdk.unitywrapper.CallbackMessageForUnity.Companion.sendMessageError
import com.gamepubcorp.sdk.unitywrapper.activity.PubSdkWrapperActivity
import com.gamepubcorp.sdk.unitywrapper.util.Log
import com.google.gson.Gson
import com.unity3d.player.UnityPlayer
import io.github.gamepubcorp.PubCallback
import io.github.gamepubcorp.cs.PubTermsResult
import io.github.gamepubcorp.data.PubSetupResult
import io.github.gamepubcorp.data.PubUnit
import io.github.gamepubcorp.iap.PubInitBillingResult
import io.github.gamepubcorp.iap.PubPurchaseResult
import io.github.gamepubcorp.iap.PubRetryPurchaseResult

class PubSdkWrapper {

    private val TAG = "PubSdkWrapper"
    private lateinit var pubApiClient: PubApiClient
    private val gson = Gson()

    fun setupSDK(identifier: String,
                 projectId: String)
    {
        val currentActivity = UnityPlayer.currentActivity
        Log.d(TAG, "setup sdk: $projectId")

        pubApiClient = PubApiClientBuilder(currentActivity, projectId.toInt()).build()
        pubApiClient.setupSDK(PubCallback<PubSetupResult>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun login(identifier: String,
              loginType: Int,
              accountServiceType: Int)
    {
        Log.d(TAG, "login type: $loginType")
        Log.d(TAG, "service type: $accountServiceType")
        //type check

        val currentActivity = UnityPlayer.currentActivity
        PubSdkWrapperActivity.startActivity(
            currentActivity,
            identifier,
            loginType,
            accountServiceType
        )
    }

    fun autoLogin(identifier: String) {
        pubApiClient.autologin(PubCallback<PubUnit>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun logout(identifier: String) {
        val currentActivity = UnityPlayer.currentActivity
        pubApiClient.logout(currentActivity, PubCallback<PubUnit>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun withdraw(identifier: String) {
        pubApiClient.withdraw(PubCallback<PubUnit>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun initBilling(identifier: String) {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.initBilling(currentActivity,
            PubCallback<PubInitBillingResult>().apply {
                success = { res ->
                    val result = gson.toJson(res)
                    CallbackMessageForUnity(identifier, result).sendMessageOk()
                }
                error = { err ->
                    sendMessageError(identifier, err)
                }
            }
        )
    }

    fun purchase(identifier: String,
                 productId: String,
                 channelId: String,
                 characterId: String)
    {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.purchase(
            currentActivity,
            productId,
            channelId,
            characterId,
            PubCallback<PubPurchaseResult>().apply {
                success = { res ->
                    val result = gson.toJson(res)
                    CallbackMessageForUnity(identifier, result).sendMessageOk()
                }
                error = { err ->
                    sendMessageError(identifier, err)
                }
            }
        )
    }

    fun retryPurchase(identifier: String,
                      channelId: String,
                      characterId: String)
    {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.retryPurchase(
            currentActivity,
            channelId,
            characterId,
            PubCallback<PubRetryPurchaseResult>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun restoreRefund(identifier: String){

    }

    fun openTerms(identifier: String){
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.openTerms(currentActivity, PubCallback<PubTermsResult>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun openImageBanner(identifier: String) {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.openImageBanner(currentActivity, PubCallback<PubUnit>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun openCustomerCenter(identifier: String) {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.openCustomerCenter(currentActivity, PubCallback<PubUnit>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun setPushToken(identifier: String,
                     pushToken: String
    ) {
        pubApiClient.setPushToken(pushToken, PubCallback<PubUnit>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
    }

    fun setPushConfig(identifier: String,
                      agreedPush: Boolean,
                      agreedNightPush: Boolean)
    {
        pubApiClient.setPushConfig(
            agreedPush,
            agreedNightPush,
            PubCallback<PubUnit>().apply {
                success = { res ->
                    val result = gson.toJson(res)
                    CallbackMessageForUnity(identifier, result).sendMessageOk()
                }
                error = { err ->
                    sendMessageError(identifier, err)
                }
            }
        )
    }
}