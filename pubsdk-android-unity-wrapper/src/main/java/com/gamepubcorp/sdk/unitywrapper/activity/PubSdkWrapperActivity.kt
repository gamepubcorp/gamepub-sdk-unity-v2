package com.gamepubcorp.sdk.unitywrapper.activity

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import android.util.Log
import io.github.gamepubcorp.PubAccountServiceType
import io.github.gamepubcorp.PubApiResponseCode
import io.github.gamepubcorp.PubLoginType
import io.github.gamepubcorp.auth.PubLoginApi
import com.gamepubcorp.sdk.unitywrapper.CallbackMessageForUnity
import com.gamepubcorp.sdk.unitywrapper.CallbackMessageForUnity.Companion.sendMessageError
import com.gamepubcorp.sdk.unitywrapper.model.LoginResultForUnity
import com.google.gson.Gson
import io.github.gamepubcorp.auth.PubLoginResult

enum class AccountServiceType(val type: Int) {
    NONE(0),
    ACCOUNT_LOGIN(1),
    ACCOUNT_LINK(2);

    companion object {
        fun fromInt(value: Int) = AccountServiceType.values().first{
            it.type == value
        }
    }
}

enum class LoginType(val type: Int) {
    GOOGLE(1),
    FACEBOOK(2),
    APPLE(3),
    GUEST(4);

    companion object {
        fun fromInt(value: Int) = LoginType.values().first{
            it.type == value
        }
    }
}

enum class LanguageCode(val type: Int) {
    ko(0),
    en(1),
    ja(2),
    zhcn(3),
    zhtw(4),
    th(5),
    vi(6),
    es(7),
    pt(8),
    fr(9),
    de(10),
    ru(11);

    companion object {
        fun fromInt(value: Int) = LanguageCode.values().first{
            it.type == value
        }
    }
}

class PubSdkWrapperActivity : Activity() {
    private lateinit var identifier: String
    private var loginType: Int = 0
    private var accountServiceType: AccountServiceType = AccountServiceType.NONE

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        parseIntent();
        startPubSdkLoginActivity();
    }

    private fun parseIntent() {
        identifier = intent.getStringExtra(KEY_IDENTIFIER) ?: ""
        loginType = intent.getIntExtra(KEY_LOGIN_TYPE, 0)
        val type = intent.getIntExtra(KEY_ACCOUNT_SERVICE_TYPE, 0)

        accountServiceType = AccountServiceType.fromInt(type)
        //Log.d(TAG, accountServiceType.name)
        //Log.d(TAG, accountServiceType.toString())
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        //super.onActivityResult(requestCode, resultCode, data)
        if (requestCode != REQUEST_CODE_LOGIN) return

        val result = getLoginResultFromIntent(data)
        Log.d(TAG, "login result:$result")

        when(result?.responseCode) {
            PubApiResponseCode.SUCCESS -> {
                val resultJsonString = gson.toJson(LoginResultForUnity.convertPubResult(result))
                CallbackMessageForUnity(identifier, resultJsonString).sendMessageOk()
            }
            else -> {
                if (result != null) {
                    sendMessageError(identifier, result.errorData)
                }
            }
        }

        finish()
    }

    private fun getLoginIntent(): Intent? {
        return when(loginType) {
            PubLoginType.GOOGLE.ordinal -> {
                val type = PubAccountServiceType.valueOf(accountServiceType.name)
                PubLoginApi.getGoogleLoginIntent(this, type)
            }
            PubLoginType.FACEBOOK.ordinal -> {
                val type = PubAccountServiceType.valueOf(accountServiceType.name)
                PubLoginApi.getFacebookLoginIntent(this, type)
            }
            PubLoginType.GUEST.ordinal -> {
                val type = PubAccountServiceType.valueOf(accountServiceType.name)
                PubLoginApi.getGuestLoginIntent(this, type)
            }
            PubLoginType.APPLE.ordinal -> {
                val type = PubAccountServiceType.valueOf(accountServiceType.name)
                PubLoginApi.getAppleLoginIntent(this, type)
            }
            else -> {
                Log.d(TAG, "login error")
                null
            }
        }
    }

    private fun getLoginResultFromIntent(data: Intent?): PubLoginResult? {
        return when(loginType) {
            PubLoginType.GOOGLE.ordinal -> {
                PubLoginApi.getGoogleLoginResultFromIntent(data)
            }
            PubLoginType.FACEBOOK.ordinal -> {
                PubLoginApi.getFacebookLoginResultFromIntent(data)
            }
            PubLoginType.GUEST.ordinal -> {
                PubLoginApi.getGuestLoginResultFromIntent(data)
            }
            PubLoginType.APPLE.ordinal -> {
                PubLoginApi.getAppleLoginResultFromIntent(data)
            }
            else -> {
                Log.d(TAG, "data null")
                null
            }
        }
    }

    private fun startPubSdkLoginActivity() {

        val loginIntent = getLoginIntent()

        startActivityForResult(loginIntent, REQUEST_CODE_LOGIN)
    }

    companion object {
        private const val KEY_IDENTIFIER = "identifier"
        private const val KEY_LOGIN_TYPE = "loginType"
        private const val KEY_ACCOUNT_SERVICE_TYPE = "accountServiceType"
        private const val REQUEST_CODE_LOGIN: Int = 1234
        private const val TAG: String = "PubSdkWrapperActivity"
        private val gson: Gson = Gson()

        fun startActivity( activity: Activity,
                           identifier: String,
                           loginType: Int,
                           accountServiceType: Int) {
            val intent = Intent(activity, PubSdkWrapperActivity::class.java).apply {
                addFlags(Intent.FLAG_ACTIVITY_NO_ANIMATION)
                putExtra(KEY_IDENTIFIER, identifier)
                putExtra(KEY_LOGIN_TYPE, loginType)
                putExtra(KEY_ACCOUNT_SERVICE_TYPE, accountServiceType)
            }
            activity.startActivityForResult(intent, REQUEST_CODE_LOGIN)
        }
    }
}