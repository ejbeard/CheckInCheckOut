// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.SiteConfigurations.Caching;

public static class SiteConfigurationCacheKey
{
    public const string GetAllCacheKey = "all-SiteConfigurations";
    public static string GetPagtionCacheKey(string parameters) {
        return $"SiteConfigurationsWithPaginationQuery,{parameters}";
    }
    public static string GetBySiteIdCacheKey(int? siteId)
    {
        return $"GetBySiteIdConfigurationsQuery,{siteId}";
    }
    static SiteConfigurationCacheKey()
    {
        _tokensource = new CancellationTokenSource(new TimeSpan(3,0,0));
    }
    private static CancellationTokenSource _tokensource;
    public static CancellationTokenSource SharedExpiryTokenSource()
    {
        if (_tokensource.IsCancellationRequested)
        {
            _tokensource = new CancellationTokenSource(new TimeSpan(3, 0, 0));
        }
        return _tokensource;
    }
    public static void Refresh()
    {
        SharedExpiryTokenSource().Cancel();
    }
    public static MemoryCacheEntryOptions MemoryCacheEntryOptions => new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(SharedExpiryTokenSource().Token));
}

