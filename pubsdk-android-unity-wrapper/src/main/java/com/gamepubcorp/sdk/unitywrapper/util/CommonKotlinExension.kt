package com.gamepubcorp.sdk.unitywrapper.util

import com.gamepubcorp.sdk.unitywrapper.BuildConfig

inline fun runIfDebugBuild(action: () -> Unit) {
    if (BuildConfig.DEBUG) action()
}