using System.Collections;
using System.Collections.Generic;

// Token: 0x020001CA RID: 458
public class SynchronizedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
{
    // Token: 0x040006A7 RID: 1703
    private readonly object _syncRoot = new object();

    // Token: 0x040006A6 RID: 1702
    private readonly Dictionary<TKey, TValue> _innerDict;

    // Token: 0x06000CD5 RID: 3285 RVA: 0x00049F98 File Offset: 0x00048198
    public SynchronizedDictionary()
    {
        _innerDict = new Dictionary<TKey, TValue>();
    }

    // Token: 0x170004E7 RID: 1255
    // (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00049BCC File Offset: 0x00047DCC
    public object SyncRoot
    {
        get { return _syncRoot; }
    }

    // Token: 0x06000CC4 RID: 3268 RVA: 0x00049BD4 File Offset: 0x00047DD4
    public void Add(TKey key, TValue value)
    {
        lock (_syncRoot)
        {
            _innerDict.Add(key, value);
        }
    }

    // Token: 0x06000CC5 RID: 3269 RVA: 0x00049C14 File Offset: 0x00047E14
    public bool ContainsKey(TKey key)
    {
        bool result;
        lock (_syncRoot)
        {
            result = _innerDict.ContainsKey(key);
        }

        return result;
    }

    // Token: 0x170004E8 RID: 1256
    // (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00049C58 File Offset: 0x00047E58
    public ICollection<TKey> Keys
    {
        get
        {
            ICollection<TKey> keys;
            lock (_syncRoot)
            {
                keys = _innerDict.Keys;
            }

            return keys;
        }
    }

    // Token: 0x06000CC7 RID: 3271 RVA: 0x00049C98 File Offset: 0x00047E98
    public bool Remove(TKey key)
    {
        bool result;
        lock (_syncRoot)
        {
            result = _innerDict.Remove(key);
        }

        return result;
    }

    // Token: 0x06000CC8 RID: 3272 RVA: 0x00049CDC File Offset: 0x00047EDC
    public bool TryGetValue(TKey key, out TValue value)
    {
        bool result;
        lock (_syncRoot)
        {
            result = _innerDict.TryGetValue(key, out value);
        }

        return result;
    }

    // Token: 0x170004E9 RID: 1257
    // (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00049D20 File Offset: 0x00047F20
    public ICollection<TValue> Values
    {
        get
        {
            ICollection<TValue> values;
            lock (_syncRoot)
            {
                values = _innerDict.Values;
            }

            return values;
        }
    }

    // Token: 0x170004EA RID: 1258
    public TValue this[TKey key]
    {
        get
        {
            TValue result;
            lock (_syncRoot)
            {
                result = _innerDict[key];
            }

            return result;
        }
        set
        {
            lock (_syncRoot)
            {
                _innerDict[key] = value;
            }
        }
    }

    // Token: 0x06000CCC RID: 3276 RVA: 0x00049DE4 File Offset: 0x00047FE4
    public void Add(KeyValuePair<TKey, TValue> item)
    {
        lock (_syncRoot)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>) _innerDict).Add(item);
        }
    }

    // Token: 0x06000CCD RID: 3277 RVA: 0x00049E24 File Offset: 0x00048024
    public void Clear()
    {
        lock (_syncRoot)
        {
            _innerDict.Clear();
        }
    }

    // Token: 0x06000CCE RID: 3278 RVA: 0x00049E64 File Offset: 0x00048064
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        bool result;
        lock (_syncRoot)
        {
            result = ((ICollection<KeyValuePair<TKey, TValue>>) _innerDict).Contains(item);
        }

        return result;
    }

    // Token: 0x06000CCF RID: 3279 RVA: 0x00049EA8 File Offset: 0x000480A8
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        lock (_syncRoot)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>) _innerDict).CopyTo(array, arrayIndex);
        }
    }

    // Token: 0x170004EB RID: 1259
    // (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00049EE8 File Offset: 0x000480E8
    public int Count
    {
        get
        {
            int count;
            lock (_syncRoot)
            {
                count = _innerDict.Count;
            }

            return count;
        }
    }

    // Token: 0x170004EC RID: 1260
    // (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00049F28 File Offset: 0x00048128
    public bool IsReadOnly
    {
        get { return false; }
    }

    // Token: 0x06000CD2 RID: 3282 RVA: 0x00049F2C File Offset: 0x0004812C
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        bool result;
        lock (_syncRoot)
        {
            result = ((ICollection<KeyValuePair<TKey, TValue>>) _innerDict).Remove(item);
        }

        return result;
    }

    // Token: 0x06000CD3 RID: 3283 RVA: 0x00049F70 File Offset: 0x00048170
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _innerDict.GetEnumerator();
    }

    // Token: 0x06000CD4 RID: 3284 RVA: 0x00049F84 File Offset: 0x00048184
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _innerDict.GetEnumerator();
    }
}