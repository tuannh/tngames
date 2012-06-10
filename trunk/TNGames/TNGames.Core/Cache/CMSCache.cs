using System;
using System.Web.Caching;

namespace TNGames.Core.Cache
{
    /// <summary>
    /// Summary description for CMSCache.
    /// </summary>
    public class CMSCache
    {
        #region Constructors

        private CMSCache() { }
        
        #endregion
        
        //>> Based on Factor = 5 default value
        public static readonly int DayFactor = TCache.DayFactor;
        public static readonly int HourFactor = TCache.HourFactor;
        public static readonly int MinuteFactor = TCache.MinuteFactor;
        public static readonly double SecondFactor = TCache.SecondFactor;
		
        #region public static int CacheFactor
		
        public static int CacheFactor
        {
            get { return TCache.CacheFactor; }
        }
		
        #endregion

        #region public static void ReSetFactor(int cacheFactor)
        
        public static void ReSetFactor(int cacheFactor)
        {
            TCache.ReSetFactor(cacheFactor);
        }

        #endregion

        #region public static void Clear()
        
        /// <summary>
        /// Removes all items from the Cache
        /// </summary>
        public static void Clear()
        {
            TCache.Clear();
        }
        
        #endregion

        #region public static void RemoveByPattern(string pattern)

        public static void RemoveByPattern(string pattern)
        {
            TCache.RemoveByPattern(pattern);
        }
        
        #endregion

        #region public static void Remove(string key)
        
        /// <summary>
        /// Removes the specified key from the cache
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            TCache.Remove(key);
        }
        
        #endregion

        #region public static void Insert(...) overloads
        
        /// <summary>
        /// Insert the current "obj" into the cache. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Insert(string key, object obj)
        {
            TCache.Insert(key, obj, int.MaxValue);
        }

        public static void Insert(string key, object obj, CacheDependency dep)
        {
            TCache.Insert(key, obj, dep);
        }

        public static void Insert(string key, object obj, int seconds)
        {
            TCache.Insert(key, obj, seconds);
        }

        public static void Insert(string key, object obj, int seconds, CacheItemPriority priority)
        {
            TCache.Insert(key, obj, seconds, priority);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds)
        {
            TCache.Insert(key, obj, dep, seconds);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
        {
            TCache.Insert(key,obj,dep,seconds,priority);
        }
        
        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            TCache.Insert(key,obj,dep,seconds,priority,onRemoveCallback);
        }
        
        #endregion

        #region public static void MicroInsert (string key, object obj, int secondFactor)
        
        public static void MicroInsert (string key, object obj, int secondFactor) 
        {
            TCache.MicroInsert(key, obj, secondFactor);
        }
        
        #endregion

        #region public static void Max(...) overloads
        
        /// <summary>
        /// Insert an item into the cache for the Maximum allowed time
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Max(string key, object obj)
        {
            TCache.Max(key, obj);
        }

        public static void Max(string key, object obj, CacheDependency dep)
        {
            TCache.Max(key, obj, dep);
        }

        #endregion

        #region public static void Permanent(...) overloads
        
        /// <summary>
        /// Insert an item into the cache for the Maximum allowed time
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Permanent(string key, object obj)
        {
            TCache.Permanent(key, obj);
        }

        public static void Permanent(string key, object obj, CacheDependency dep)
        {
            TCache.Permanent(key, obj, dep);
        }
        
        #endregion

        #region public static object Get(string key)
        
        public static object Get(string key)
        {
            return TCache.Get(key);
        }
        
        #endregion

        #region public static int SecondFactorCalculate(int seconds)

        /// <summary>
        /// Return int of seconds * SecondFactor
        /// </summary>
        public static int SecondFactorCalculate(int seconds)
        {
            // Insert method below takes integer seconds, so we have to round any fractional values
            return TCache.SecondFactorCalculate(seconds);
        }

        #endregion

        #region public static void RefreshByPattern(string pattern)

        public static void RefreshByPattern(string pattern)
        {
            TCache.RefreshByPattern(pattern);
        }

        #endregion

        #region public static bool RefreshByCacheKey(string key)
        
        public static bool RefreshByCacheKey(string key)
        {
            return TCache.RefreshByCacheKey( key );
        }
		
        #endregion
		
        #region public static bool Update(string key)

        public static bool Update(string key)
        {
            return TCache.Update(key);
        }

        #endregion     
		
        #region public static object GetCacheEntryLock(...) overloads
		
        public static object GetCacheEntryLock(string key)
        {
            return TCache.GetCacheEntryLock(key);
        }
				
        #endregion

        #region public static bool ContainsCacheEntry(stirng key)
		
        public static bool ContainsCacheEntry(string key)
        {
            return TCache.ContainsCacheEntry(key);
        }
		
        #endregion
    }

    public class CMSCache<T> where T : class
    {
        #region Constructors
		
        private CMSCache()
        {}
		
        #endregion

        #region public static T Get(...) overloads

        public static T Get(string key)
        {
            return TCache<T>.Get(key);
        }

        public static T Get(string key, TimeSpan refreshIntervalSeconds, TimeSpan slidingExpirationSeconds, TCache.CacheLoaderDelegate loaderDelegate)
        {
            return TCache<T>.Get(key, refreshIntervalSeconds, slidingExpirationSeconds, loaderDelegate);
        }

        public static T Get(string key, int refreshIntervalSeconds, int slidingExpirationSeconds, TCache.CacheLoaderDelegate loaderDelegate)
        {
            return TCache<T>.Get(key, refreshIntervalSeconds, slidingExpirationSeconds, loaderDelegate);
        }
		
        #endregion
		
        #region public static bool Update(string key)

        //public static bool Update(string key)
        //{
        //    return TCache<T>.Update(key);
        //}

        #endregion
    }
}