using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace BlazorWasmClient.Components.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ISessionStorageService sessionStorageService;
    private readonly ILogger<CustomAuthenticationStateProvider> logger;
    private readonly ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService, ILogger<CustomAuthenticationStateProvider> logger)
    {
        this.sessionStorageService = sessionStorageService;
        this.logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSession = await sessionStorageService.ReadEncryptedItem<UserSession>("UserSession");
            if (userSession == null || string.IsNullOrWhiteSpace(userSession.Token))
                return new AuthenticationState(anonymous);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.UserName ?? ""),
                new Claim(ClaimTypes.Role, userSession.Role ?? ""),
            }, "JwtAuth"));
            
            return new AuthenticationState(claimsPrincipal);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception in auth state provider");
            return new AuthenticationState(anonymous);
        }
    }

    public async Task UpdateAuthenticationState(UserSession userSession)
    {
        ClaimsPrincipal claimsPrincipal = anonymous;

        if (userSession != null && !string.IsNullOrWhiteSpace(userSession.Token))
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.UserName ?? ""),
                new Claim(ClaimTypes.Role, userSession.Role ?? ""),
            }, "JwtAuth"));
            await sessionStorageService.SaveItemAsEncrypted("UserSession", userSession);
        }
        else
        {
            await sessionStorageService.RemoveItemAsync("UserSession");
        }
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task<string> GetToken()
    {
        try
        {
            var userSession = await sessionStorageService.ReadEncryptedItem<UserSession>("UserSession");
            if (userSession != null && !string.IsNullOrWhiteSpace(userSession.Token))
            {
                if (userSession.ExpiryTimestamp == default || userSession.ExpiryTimestamp == DateTime.MinValue || DateTime.Now < userSession.ExpiryTimestamp)
                {
                    return userSession.Token;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception getting token from session storage");
        }
        
        return string.Empty;
    }
}
