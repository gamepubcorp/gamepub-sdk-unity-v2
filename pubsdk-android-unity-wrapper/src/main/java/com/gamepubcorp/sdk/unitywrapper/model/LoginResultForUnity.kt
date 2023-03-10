package com.gamepubcorp.sdk.unitywrapper.model

import io.github.gamepubcorp.PubApiResponseCode
import io.github.gamepubcorp.auth.PubLoginResult

data class LoginResultForUnity(
    val responseCode: Int?,
    val loginInfo: UserLoginInfo?
) {
    companion object {
        fun convertPubResult(pubLoginResult: PubLoginResult?): LoginResultForUnity? {
            val pubLoginInfo = pubLoginResult?.pubLoginInfo?.let {
                UserLoginInfo.convertPubLoginInfo(it)
            }
            val respCode = pubLoginResult?.responseCode?.code
            return LoginResultForUnity(
                respCode,
                pubLoginInfo
            )
        }
    }
}