//* ===================================
//* 类名称：CacheManager
//* 类描述：
//* 创建人：Ryan Ren
//* 创建时间：2015/10/1 11:08:12
//* 修改人：
//* 修改时间：
//* 修改备注：
//* @version 1.0
//* ===================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focuswin.SP.Base.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheManager
    {
        private static CacheManager _default = new CacheManager();
        private Dictionary<string, ObjectWrapper> _cachedObjects = new Dictionary<string, ObjectWrapper>();
        private static Object _thisLock = new Object();
        private static Object _updateLock = new Object();
        private Timer _timer;
        public int DefaultTimerInterval = 1000 * 60; //1 mins as default

        private CacheManager()
        {
            TimerCallback tcb = this.UpdateCachedObjects;
            _timer = new Timer(tcb, null, DefaultTimerInterval, DefaultTimerInterval);
        }

        public static CacheManager Default { get { return _default; } }

        private void UpdateCachedObjects(Object stateInfo)
        {
            lock (_updateLock)
            {
                foreach (var ow in _cachedObjects.Values)
                {
                    if (ow.RequireUpdate())
                    {
                        Thread t = new Thread(ow.Update);
                        t.IsBackground = true;
                        t.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Register object and update it immediatelly
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void RegisterObject(string key, ICachable obj)
        {
            RegisterObject(key, obj, 0, true);
        }
        public void RegisterObject(string key, ICachable obj, int checkInterval, bool updateImmediatelly)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (!HasCached(key))
            {
                lock (_thisLock)
                {
                    if (!HasCached(key))
                    {
                        var ow = new ObjectWrapper { CachedObject = obj, CheckInterval = checkInterval, LastUpdatedTimeTicks = 0, Key = key };
                        _cachedObjects[key] = ow;
                        if (updateImmediatelly)
                        {
                            ow.Update();
                        }
                    }
                }
            }
        }

        public bool HasCached(string key)
        {
            return _cachedObjects.ContainsKey(key);
        }

        public object GetCached(string key)
        {
            var obj = GetWrapperObject(key);
            if (obj != null)
            {
                return obj.CachedObject;
            }
            else
            {
                return null;
            }
        }

        public int GetCachedInterval(string key)
        {
            var obj = GetWrapperObject(key);
            if (obj != null)
            {
                return obj.CheckInterval;
            }
            else
            {
                return -1;
            }
        }

        public void ClearCachedByKey(string key)
        {
            lock (_updateLock)
            {
                if (_cachedObjects.ContainsKey(key))
                {
                    _cachedObjects.Remove(key);
                }
            }
        }

        private ObjectWrapper GetWrapperObject(string key)
        {
            if (_cachedObjects.ContainsKey(key))
            {
                return _cachedObjects[key];
            }
            return null;
        }

        public void SetCheckInterval(string key, int checkinterval)
        {
            var ob = GetWrapperObject(key);
            if (ob != null)
            {
                ob.CheckInterval = checkinterval;
            }
        }

        public void ForceUpdate(string key)
        {
            if (HasCached(key))
            {
                ObjectWrapper ow = (ObjectWrapper)_cachedObjects[key];
                ow.Update();
            }
        }

        public Dictionary<string, object> GetAllCached()
        {
            Dictionary<string, object> items = new Dictionary<string, object>();
            foreach (string key in _cachedObjects.Keys)
            {
                items.Add(key, _cachedObjects[key].CheckInterval);
            }
            return items;
        }

        private class ObjectWrapper
        {
            public ICachable CachedObject { get; set; }
            public long LastUpdatedTimeTicks { get; set; }
            public long LastCheckedTimeTicks { get; set; }
            public int CheckInterval { get; set; }
            public string Key { get; set; }

            public void Update()
            {
                try
                {
                    LastUpdatedTimeTicks = CachedObject.UpdateCachedObject();
                }
                catch (Exception ex)
                {
                }
            }

            public bool RequireUpdate()
            {
                bool checkResult = false;
                try
                {
                    long currentTimeStick = DateTime.Now.Ticks;
                    long checkInterval = CacheManager.Default.DefaultTimerInterval;
                    LastCheckedTimeTicks = currentTimeStick;
                    if (CheckInterval > 0)
                    {
                        checkInterval = (long)CheckInterval;
                    }
                    if ((currentTimeStick - LastUpdatedTimeTicks) >= (checkInterval * 10000))
                    {
                        checkResult = true;
                    }
                }
                catch (Exception ex)
                {
                }
                return checkResult;
            }
        }
    }
}
