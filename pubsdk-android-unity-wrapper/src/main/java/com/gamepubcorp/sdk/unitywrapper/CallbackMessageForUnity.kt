package com.gamepubcorp.sdk.unitywrapper

import android.app.Activity
import com.google.gson.Gson
import com.unity3d.player.UnityPlayer
import io.github.gamepubcorp.code.PubSdkErrorCode
import io.github.gamepubcorp.result.PubSdkError

data class CallbackMessageForUnity(
    val identifier: String,
    val value: String
) {
    fun sendMessageOk() =
        UnityPlayer.UnitySendMessage(KEY_PUB_SDK, NAME_API_OK, generateMessageJson())

    fun sendMessageError() =
        UnityPlayer.UnitySendMessage(KEY_PUB_SDK, NAME_API_ERROR, generateMessageJson())

    private fun generateMessageJson(): String = gson.toJson(this)

    companion object {
        private val gson: Gson = Gson()

        private const val KEY_PUB_SDK: String  = "GamePubSDK"
        private const val NAME_API_OK: String  = "OnApiOk"
        private const val NAME_API_ERROR: String  = "OnApiError"

//        fun sendMessageError(
//            identifier: String,
//            loginResult: PubLoginResult,
//            errorString: String?)
//        {
//            val errorForUnity = ErrorForUnity(
//                loginResult.errorData.errCode.code,
//                errorString)
//            CallbackMessageForUnity(identifier, gson.toJson(errorForUnity)).sendMessageError()
//        }

        fun sendMessageError(
            identifier: String,
            apiError: PubSdkError
        ) {
            val jsonError = gson.toJson(apiError)
            CallbackMessageForUnity(identifier, jsonError).sendMessageError()
        }

        fun sendSdkNotInitializedError(
            activity: Activity,
            identifier: String
        ) {
            val apiError = PubSdkError(
                PubSdkErrorCode.SDK_NOT_INITIALIZED.code,
                activity.getString(R.string.msg_sdk_not_initialized)
            )
            val jsonError = gson.toJson(apiError)
            CallbackMessageForUnity(identifier, jsonError).sendMessageError()
        }
    }
}