package com.gamepubcorp.sdk.unitywrapper

import com.google.gson.Gson
import com.unity3d.player.UnityPlayer
import io.github.gamepubcorp.result.PubApiError

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
            apiError: PubApiError
        ) {
            val jsonError = gson.toJson(apiError)
            CallbackMessageForUnity(identifier, jsonError).sendMessageError();
        }
    }
}