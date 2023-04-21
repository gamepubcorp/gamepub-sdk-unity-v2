package com.gamepubcorp.sdk.unitywrapper

import io.github.gamepubcorp.api.PubApiClient
import io.github.gamepubcorp.api.PubApiClientBuilder
import com.gamepubcorp.sdk.unitywrapper.CallbackMessageForUnity.Companion.sendMessageError
import com.gamepubcorp.sdk.unitywrapper.auth.PubSdkWrapperActivity
import com.gamepubcorp.sdk.unitywrapper.util.Log
import com.google.gson.Gson
import com.unity3d.player.UnityPlayer
import io.github.gamepubcorp.result.*
import io.github.gamepubcorp.utils.PubCallback

class PubSdkWrapper {

    private val TAG = "bridge"
    private lateinit var pubApiClient: PubApiClient
    private val gson = Gson()

    fun setupSDK(identifier: String,
                 projectId: String) {
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
              accountServiceType: Int) {
        val currentActivity = UnityPlayer.currentActivity

        Log.d(TAG, "login type: $loginType")
        Log.d(TAG, "service type: $accountServiceType")
        //type check

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
                 characterId: String) {
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
                      characterId: String) {
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
            }
        )
    }

    fun openVoided(identifier: String,
               channelId: String,
               characterId: String){
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.openVoided(
            currentActivity,
            channelId,
            characterId, PubCallback<PubVoidedResult>().apply {
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

    fun openTerms(identifier: String){
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.openTerms(currentActivity, PubCallback<PubUnit>().apply {
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

    fun openHelp(identifier: String) {
        val currentActivity = UnityPlayer.currentActivity

        pubApiClient.openHelp(currentActivity, PubCallback<PubUnit>().apply {
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
                     pushToken: String) {

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
                      agreedNightPush: Boolean) {

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