//
//  PubSDKWrapper.m
//  PubSDKUnityBridge
//
//  Created by gamepub on 2022/05/16.
//

#import "PubSDKWrapper.h"
#import "PubSDKCallbackPayload.h"

@import PubSDKSwift;

@interface PubSDKWrapper()
@property (nonatomic, assign) BOOL setup;
@end

@implementation PubSDKWrapper

+ (instancetype)sharedInstance
{
    static PubSDKWrapper *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[PubSDKWrapper alloc] init];
    });
    return sharedInstance;
}

- (void)setupSDK:(NSString *)identifier
       projectId:(NSString *)projectId
{
    if(self.setup) {
        return;
    }
    self.setup = YES;
    
    NSLog(@"setupSDK");
    
    NSInteger projId = [projectId intValue];
        
    [[PubAPIClient shared] setupWithProjectId:projId
                            completionHandler:^(PubSDKSetupResult *result,
                                                NSError *error)
     {
        if(error) {
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)login:(NSString *)identifier
         type:(int)loginType
  serviceType:(int)accountServiceType
{
    NSLog(@"login");
    
    [[PubAPIClient shared] loginWithLoginType:loginType
                                  serviceType:accountServiceType
                             inViewController:UnityGetGLViewController()
                            completionHandler:^(PubSDKLoginResult *result,
                                                NSError *error)
     {
        if(error) {
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)autoLogin:(NSString *)identifier
{
    NSLog(@"autoLogin");
    
    [[PubAPIClient shared] autoLoginWithCompletionHandler:^(PubSDKLoginResult *result,
                                                            NSError *error)
     {
        if(error) {
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)logout:(NSString *)identifier
{
    NSLog(@"logout");
}

- (void)withdraw:(NSString *)identifier
{
    NSLog(@"withdraw");
    
    [[PubAPIClient shared] withdrawWithCompletionHandler:^(PubSDKLoginResult *result,
                                                           NSError *error)
     {
        if(error) {
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)initBilling:(NSString *)identifier
{
    NSLog(@"initBilling");
    
    [[PubAPIClient shared] initBillingWithCompletionHandler:^(PubSDKProductList *result,
                                                              NSError *error)
     {
        if(error) {
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)purchase:(NSString *)identifier
       productId:(NSString *)productId
       channelId:(NSString *)channelId
     characterId:(NSString *)characterId
{
    NSLog(@"purchase");
    
    [[PubAPIClient shared] purchaseWithProductId:productId
                                       channelId:channelId
                                     characterId:characterId
                               completionHandler:^(PubSDKPurchaseResult *result,
                                                   NSError *error)
     {
        if(error) {
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            PubSDKCallbackPayload *payload = [PubSDKCallbackPayload callbackMessage:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void)openTerms:(NSString *)identifier
{
    NSLog(@"openTerms");
}

- (void)openImageBanner
{
    NSLog(@"openImageBanner");
}

- (void)openHelp
{
    NSLog(@"openHelp");
}

- (void)setPushToken:(NSString *)pushToken
{
    NSLog(@"setPushToken");
    [[PubAPIClient shared] setPushTokenWithPushToken:pushToken];
}

- (void)setPushConfig:(BOOL)agreedPush
      agreedNightPush:(BOOL)agreedNightPush
{
    NSLog(@"setPushConfig");
}

- (NSString *)wrapError:(NSError *)error {
    NSDictionary *dic = @{@"code": @(error.code), @"message": error.localizedDescription};
    NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:kNilOptions error:nil];
    if (!data) { return nil; }
    return [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
}

@end
