using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlazorWasmClient.Components.Auth;
using BlazorWasmClient.Shared;
using DashboardService.Api.Queries;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BlazorWasmClient.Components.Dashboard;

public class DashboardClient
{
    private readonly CustomAuthenticationStateProvider customAuthenticationStateProvider;
    private readonly HttpClient httpClient;
    private readonly string baseUrl;
    
    public DashboardClient
    (
        AuthenticationStateProvider customAuthenticationStateProvider, 
        HttpClient httpClient,
        IConfiguration configuration
    )
    {
        this.customAuthenticationStateProvider = (CustomAuthenticationStateProvider)customAuthenticationStateProvider;
        this.httpClient = httpClient;
        this.baseUrl = configuration["Services:ApiGateway"];
    }

    private async Task EnsureAuthHeader()
    {
        var token = await customAuthenticationStateProvider.GetToken();
        if (!string.IsNullOrWhiteSpace(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<ApiResponse<GetTotalSalesResult>> GetTotalSales(GetTotalSalesQuery query)
    {
        try
        {
            await EnsureAuthHeader();
            
            var calculationResult = await httpClient.PostAsync
            (
                $"{baseUrl}/api/Dashboard/total-sales", 
                new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json")
            );

            if (calculationResult.IsSuccessStatusCode)
            {
                var calculationResultJson = await calculationResult.Content.ReadAsStringAsync();
                return ApiResponse<GetTotalSalesResult>.Success(JsonConvert.DeserializeObject<GetTotalSalesResult>(calculationResultJson));
            }
            
            return ApiResponse<GetTotalSalesResult>.Error($"Request failed: {calculationResult.StatusCode}");
        }
        catch (Exception ex)
        {
            return ApiResponse<GetTotalSalesResult>.Error(ex.Message);
        }
    }
    
    public async Task<ApiResponse<GetAgentsSalesResult>> GetAgentsSales(GetAgentsSalesQuery query)
    {
        try
        {
            await EnsureAuthHeader();
            
            var calculationResult = await httpClient.PostAsync
            (
                $"{baseUrl}/api/Dashboard/agents-sales", 
                new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json")
            );

            if (calculationResult.IsSuccessStatusCode)
            {
                var calculationResultJson = await calculationResult.Content.ReadAsStringAsync();
                return ApiResponse<GetAgentsSalesResult>.Success(JsonConvert.DeserializeObject<GetAgentsSalesResult>(calculationResultJson));
            }
            
            return ApiResponse<GetAgentsSalesResult>.Error($"Request failed: {calculationResult.StatusCode}");
        }
        catch (Exception ex)
        {
            return ApiResponse<GetAgentsSalesResult>.Error(ex.Message);
        }
    }
    
    public async Task<ApiResponse<GetSalesTrendsResult>> GetSalesTrends(GetSalesTrendsQuery query)
    {
        try
        {
            await EnsureAuthHeader();
            
            var calculationResult = await httpClient.PostAsync
            (
                $"{baseUrl}/api/Dashboard/sales-trends", 
                new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json")
            );

            if (calculationResult.IsSuccessStatusCode)
            {
                var calculationResultJson = await calculationResult.Content.ReadAsStringAsync();
                return ApiResponse<GetSalesTrendsResult>.Success(JsonConvert.DeserializeObject<GetSalesTrendsResult>(calculationResultJson));
            }
            
            return ApiResponse<GetSalesTrendsResult>.Error($"Request failed: {calculationResult.StatusCode}");
        }
        catch (Exception ex)
        {
            return ApiResponse<GetSalesTrendsResult>.Error(ex.Message);
        }
    }
}
