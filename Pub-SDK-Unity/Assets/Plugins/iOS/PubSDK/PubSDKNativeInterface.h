//
//  PubSDKNativeInterface.h
//  PubSDKUnityBridge
//
//  Created by gamepub on 2022/11/09.
//

#ifndef PubSDKNativeInterface_h
#define PubSDKNativeInterface_h

#if __cplusplus
extern "C"
{
#endif /* __cplusplus */

    void pub_sdk_UnitySendMessage(const char *name, const char *method, NSString *params);

#if __cplusplus
}
#endif /* __cplusplus */

#endif /* PubSDKNativeInterface_h */
