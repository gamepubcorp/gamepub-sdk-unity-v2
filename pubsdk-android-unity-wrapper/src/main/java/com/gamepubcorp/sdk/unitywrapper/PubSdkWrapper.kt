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
import io.github.gamepubcorp.data.PubUnit
import io.github.gamepubcorp.iap.PubInAppProduct
import io.github.gamepubcorp.iap.PubPurchaseResult

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
        pubApiClient.setupSDK(PubCallback<PubUnit>().apply {
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

    fun logout()
    {
        val currentActivity = UnityPlayer.currentActivity
        pubApiClient.logout(currentActivity)
    }

    fun initBilling(identifier: String) {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.initBilling(currentActivity,
            PubCallback<List<PubInAppProduct>>().apply {
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

    fun purchaseLaunch(identifier: String,
                       pid: String) {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.purchase(currentActivity, pid, "", "",
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

    fun restorePurchase(identifier: String) {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.restorePurchase(currentActivity,
            PubCallback<PubPurchaseResult>().apply {
            success = { res ->
                val result = gson.toJson(res)
                CallbackMessageForUnity(identifier, result).sendMessageOk()
            }
            error = { err ->
                sendMessageError(identifier, err)
            }
        })
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

        pubApiClient.openImageBanner(currentActivity)
    }

    fun setPushToken(identifier: String) {

    }

    fun setPushConfig(identifier: String) {

    }
}