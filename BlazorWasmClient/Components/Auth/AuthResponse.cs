using System;
using System.Text.Json.Serialization;

namespace BlazorWasmClient.Components.Auth;

public class AuthResponse
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public string[] Roles { get; set; }
    public string Avatar { get; set; }
    public string UserType { get; set; }

    [JsonPropertyName("expiryTimeStamp")]
    public DateTime? ExpiryTimeStamp { get; set; }

    public DateTime ExpiresIn
    {
        get => ExpiryTimeStamp ?? (expiresIn != default ? expiresIn : DateTime.Now.AddDays(7));
        set => expiresIn = value;
    }
    private DateTime expiresIn;
}
