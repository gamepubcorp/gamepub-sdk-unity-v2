package com.gamepubcorp.sdk.unitywrapper.model

import io.github.gamepubcorp.data.PubLoginInfo

data class UserLoginInfo(
    val accountId: String,
    val loginToken: String,
    val regMessage: String,
    val startDate: String,
    val endDate: String
)
{
    companion object {
        fun convertPubLoginInfo(loginInfo: PubLoginInfo): UserLoginInfo =
            UserLoginInfo(
                loginInfo.accountId,
                loginInfo.accessToken,
                loginInfo.regMessage,
                loginInfo.startDate,
                loginInfo.endDate
            )
    }
}