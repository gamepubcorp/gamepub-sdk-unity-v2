package com.gamepubcorp.sdk.unitywrapper.model

import io.github.gamepubcorp.PubProfile

data class UserProfile(
    val loginType: String,
    val channelId: String,
    val displayName: String,
    val photoURL: String,
    val email: String
)
{
    companion object {
        fun convertPubProfile(profile: PubProfile): UserProfile =
            UserProfile(
                profile.loginType.toString(),
                profile.socialId ?: "",
                profile.displayName ?: "",
                profile.photoURL ?: "",
                profile.email ?: ""
            )

    }
}