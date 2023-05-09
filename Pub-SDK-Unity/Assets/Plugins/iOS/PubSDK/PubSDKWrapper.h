//
//  PubSDKWrapper.h
//  PubSDKUnityBridge
//
//  Created by gamepub on 2022/05/16.
//

#import <Foundation/Foundation.h>

@interface PubSDKWrapper : NSObject

+ (instancetype)sharedInstance;

- (void)setupSDK:(NSString *)identifier
       projectId:(NSString *)projectId;

- (void)login:(NSString *)identifier
         type:(int)loginType
  serviceType:(int)accountServiceType;

- (void)autoLogin:(NSString *)identifier;

- (void)logout:(NSString *)identifier;

- (void)withdraw:(NSString *)identifier;

- (void)initBilling:(NSString *)identifier;

- (void)purchase:(NSString *)identifier
       productId:(NSString *)productId
       channelId:(NSString *)channelId
     characterId:(NSString *)characterId;

- (void)openTerms:(NSString *)identifier;

- (void)openImageBanner;

- (void)openHelp;

- (void)setPushToken:(NSString *)pushToken;

- (void)setPushConfig:(BOOL)agreedPush
      agreedNightPush:(BOOL)agreedNightPush;

@end
