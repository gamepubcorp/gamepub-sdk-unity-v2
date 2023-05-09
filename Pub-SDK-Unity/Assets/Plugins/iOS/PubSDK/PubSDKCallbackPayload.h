//
//  PubSDKCallbackPayload.h
//  PubSDKUnityBridge
//
//  Created by gamepub on 2022/11/09.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface PubSDKCallbackPayload : NSObject

+ (instancetype)callbackMessage:(NSString *)identifier value:(NSString *)value;

- (instancetype)initWithIdentifier:(NSString *)identifier value:(NSString *)value;
- (void)sendMessageOK;
- (void)sendMessageError;

@end

NS_ASSUME_NONNULL_END
