package com.gamepubcorp.sdk.unitywrapper.auth

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import android.util.Log
import com.gamepubcorp.sdk.unitywrapper.CallbackMessageForUnity
import io.github.gamepubcorp.auth.PubLoginApi
import com.gamepubcorp.sdk.unitywrapper.CallbackMessageForUnity.Companion.sendMessageError
import com.google.gson.Gson
import io.github.gamepubcorp.code.PubResponseCode
import io.github.gamepubcorp.result.PubApiError

//enum class AccountServiceType(val type: Int) {
//    ACCOUNT_LOGIN(1),
//    ACCOUNT_LINK(2);
//
//    companion object {
//        fun fromInt(value: Int) = values().first{
//            it.type == value
//        }
//    }
//}
//
//enum class LoginType(val type: String) {
//    GOOGLE(1),
//    FACEBOOK(2),
//    APPLE(3),
//    GUEST(4);
//}
//
//enum class LanguageCode(val type: Int) {
//    ko(0),
//    en(1),
//    ja(2),
//    zhcn(3),
//    zhtw(4),
//    th(5),
//    vi(6),
//    es(7),
//    pt(8),
//    fr(9),
//    de(10),
//    ru(11);
//}

class PubSdkWrapperActivity : Activity() {

    private val TAG: String = "bridge"
    private var identifier = ""
    private val gson: Gson = Gson()
    private val DEFAULT_VALUE = -1

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val loginIntent = parseIntent(intent)
        startLoginActivity(loginIntent)
    }

    private fun parseIntent(intent: Intent): Intent? {
        identifier = intent.getStringExtra(KEY_IDENTIFIER) ?: ""

        val loginType = intent.getIntExtra(KEY_LOGIN_TYPE, DEFAULT_VALUE)
        val serviceType = intent.getIntExtra(KEY_ACCOUNT_SERVICE_TYPE, DEFAULT_VALUE)

        return PubLoginApi.getLoginIntent(this, loginType, serviceType)
    }

    private fun startLoginActivity(loginIntent: Intent?) {
        if (loginIntent != null) {
            startActivityForResult(loginIntent, REQUEST_CODE_LOGIN)
        }
        else {
            sendMessageError(identifier, PubApiError(
                PubResponseCode.AUTH_WRONG_TYPE_INPUT.code,
                "wrong login type input."
            ))
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        //super.onActivityResult(requestCode, resultCode, data)
        if (requestCode != REQUEST_CODE_LOGIN) return

        if (data != null) {
            val result = PubLoginApi.getLoginResultFromIntent(resultCode, data)
            Log.d(TAG, "login result:$result")

            when(result.responseCode) {
                PubResponseCode.SUCCESS -> {
                    val resultForUnity = LoginResultForUnity.convertPubResult(result)
                    val resultJsonString = gson.toJson(resultForUnity)
                    CallbackMessageForUnity(identifier, resultJsonString).sendMessageOk()
                }
                else -> {
                    result.errorData?.let { sendMessageError(identifier, it) }
                }
            }
        }
        finish()
    }

//    private fun getLoginIntent(
//        loginType: PubLoginType,
//        accountServiceType: PubAccountServiceType
//    ): Intent {
//        return when(loginType) {
//            PubLoginType.GOOGLE -> {
//                PubLoginApi.getGoogleLoginIntent(this, accountServiceType)
//            }
//            PubLoginType.FACEBOOK -> {
//                PubLoginApi.getFacebookLoginIntent(this, accountServiceType)
//            }
//            PubLoginType.APPLE -> {
//                PubLoginApi.getAppleLoginIntent(this, accountServiceType)
//            }
//            PubLoginType.GUEST -> {
//                PubLoginApi.getGuestLoginIntent(this, accountServiceType)
//            }
//        }
//    }
//
//    private fun getLoginResultFromIntent(
//        resultCode: Int,
//        data: Intent
//    ): PubLoginResult {
//        return when(resultCode) {
//            PubLoginType.GOOGLE.ordinal -> {
//                PubLoginApi.getGoogleLoginResultFromIntent(data)
//            }
//            PubLoginType.FACEBOOK.ordinal -> {
//                PubLoginApi.getFacebookLoginResultFromIntent(data)
//            }
//            PubLoginType.APPLE.ordinal -> {
//                PubLoginApi.getAppleLoginResultFromIntent(data)
//            }
//            PubLoginType.GUEST.ordinal -> {
//                PubLoginApi.getGuestLoginResultFromIntent(data)
//            }
//            else -> {
//                PubLoginResult.internalError("login result is null")
//            }
//        }
//    }

    companion object {
        private const val KEY_IDENTIFIER = "identifier"
        private const val KEY_LOGIN_TYPE = "loginType"
        private const val KEY_ACCOUNT_SERVICE_TYPE = "accountServiceType"
        private const val REQUEST_CODE_LOGIN: Int = 1234

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