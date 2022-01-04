﻿using System;
using System.Runtime.Caching;

namespace Fujitsu.eDoc.Organisation.FKIntegration
{
    public static class CacheHelper
    {
        public static void SaveTocache(string cacheKey, object savedItem, DateTime absoluteExpiration)
        {
            MemoryCache.Default.Add(cacheKey, savedItem, absoluteExpiration);
        }

        public static T GetFromCache<T>(string cacheKey) where T : class
        {
            return MemoryCache.Default[cacheKey] as T;
        }

        public static void RemoveFromCache(string cacheKey)
        {
            MemoryCache.Default.Remove(cacheKey);
        }

        public static bool IsIncache(string cacheKey)
        {
            return MemoryCache.Default[cacheKey] != null;
        }
    }
}
