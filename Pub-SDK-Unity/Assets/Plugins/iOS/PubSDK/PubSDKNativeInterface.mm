//
//  PubSDKNativeInterface.m
//  PubSDKUnityBridge
//
//  Created by gamepub on 2022/11/09.
//

#import <Foundation/Foundation.h>
#import "PubSDKWrapper.h"

#define PUB_SDK_EXTERNC extern "C"

// MARK: - Helpers

NSString* PubSDKMakeNSString(const char* string)
{
    if(string) {
        return [NSString stringWithUTF8String:string];
    }else{
        return [NSString stringWithUTF8String:""];
    }
}

char* PubSDKMakeCString(NSString *str)
{
    const char* string = [str UTF8String];
    if(string == NULL) {
        return NULL;
    }
    
    char *buffer = (char*)malloc(strlen(string)+1);
    strcpy(buffer, string);
    return buffer;
}

PUB_SDK_EXTERNC void pub_sdk_UnitySendMessage(const char *name,
                                              const char *method,
                                              NSString *params)
{
    UnitySendMessage(name, method, PubSDKMakeCString(params));
}

// MARK: - Extern APIs

PUB_SDK_EXTERNC void pub_sdk_setup(const char* identifier,
                                   const char* projectId)
{
    NSString *nsIdentifier = PubSDKMakeNSString(identifier);
    NSString *nsProjectId = PubSDKMakeNSString(projectId);
    [[PubSDKWrapper sharedInstance] setupSDK:nsIdentifier
                                   projectId:nsProjectId];
}

PUB_SDK_EXTERNC void pub_sdk_login(const char* identifier,
                                   int loginType,
                                   int serviceType);
void pub_sdk_login(const char* identifier,
                   int loginType,
                   int serviceType)
{
    NSString *nsIdentifier = PubSDKMakeNSString(identifier);
    [[PubSDKWrapper sharedInstance] login:nsIdentifier
                                     type:loginType
                              serviceType:serviceType];
}

PUB_SDK_EXTERNC void pub_sdk_autoLogin(const char* identifier)
{
    NSString *nsIdentifier = PubSDKMakeNSString(identifier);
    [[PubSDKWrapper sharedInstance] autoLogin:nsIdentifier];
}

PUB_SDK_EXTERNC void pub_sdk_logout(const char* identifier)
{
    NSString *nsIdentifier = PubSDKMakeNSString(identifier);
    [[PubSDKWrapper sharedInstance] logout:nsIdentifier];
}

PUB_SDK_EXTERNC void pub_sdk_withdraw(const char* identifier)
{
    NSString *nsIdentifier = PubSDKMakeNSString(identifier);
    [[PubSDKWrapper sharedInstance] withdraw:nsIdentifier];
}

PUB_SDK_EXTERNC void pub_sdk_initBilling(const char* identifier) {
    NSString *nsIdentifier = PubSDKMakeNSString(identifier);
    [[PubSDKWrapper sharedInstance] initBilling:nsIdentifier];
}

PUB_SDK_EXTERNC void pub_sdk_purchase(const char* identifier,
                                      const char* productId,
                                      const char* channelId,
                                      const char* characterId);
void pub_sdk_purchase(const char* identifier,
                      const char* productId,
                      const char* channelId,
                      const char* characterId)
{
    NSString *nsIdentifier = PubSDKMakeNSString(identifier);
    NSString *nsProductId = PubSDKMakeNSString(productId);
    NSString *nsChannelId = PubSDKMakeNSString(channelId);
    NSString *nsCharacterId = PubSDKMakeNSString(characterId);
    [[PubSDKWrapper sharedInstance] purchase:nsIdentifier
                                   productId:nsProductId
                                   channelId:nsChannelId
                                 characterId:nsCharacterId];
}

PUB_SDK_EXTERNC void pub_sdk_openTerms(const char* identifier) {
    NSString *nsIdentifier = PubSDKMakeNSString(identifier);
    [[PubSDKWrapper sharedInstance] openTerms:nsIdentifier];
}

PUB_SDK_EXTERNC void pub_sdk_openImageBanner() {
    [[PubSDKWrapper sharedInstance] openImageBanner];
}

PUB_SDK_EXTERNC void pub_sdk_openHelp() {
    [[PubSDKWrapper sharedInstance] openHelp];
}

PUB_SDK_EXTERNC void pub_sdk_setPushToken(const char* pushToken) {
    NSString *nsPushToken = PubSDKMakeNSString(pushToken);
    [[PubSDKWrapper sharedInstance] setPushToken:nsPushToken];
}

PUB_SDK_EXTERNC void pub_sdk_setPushConfig(bool agreedPush,
                                           bool agreedNightPush) {
    [[PubSDKWrapper sharedInstance] setPushConfig:agreedPush
                                  agreedNightPush:agreedNightPush];
}
