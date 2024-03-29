package com.gamepubcorp.sdk.unitywrapper.auth

import io.github.gamepubcorp.result.PubLoginResult

data class LoginResultForUnity(
    val responseCode: Int,
    val accountId: String?,
    val accessToken: String?,
    val regMessage: String?,
    val startDate: String?,
    val endDate: String?,
    val clickLink: String?
) {
    companion object {
        fun convertPubResult(pubLoginResult: PubLoginResult): LoginResultForUnity {
            return LoginResultForUnity(
                pubLoginResult.code,
                pubLoginResult.accountId,
                pubLoginResult.accessToken,
                pubLoginResult.regMessage,
                pubLoginResult.startDate,
                pubLoginResult.endDate,
                pubLoginResult.clickLink
            )
        }
    }
}