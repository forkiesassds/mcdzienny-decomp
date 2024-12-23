using System.Collections;
using System.Collections.Generic;

public class SynchronizedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
{
    readonly Dictionary<TKey, TValue> _innerDict;

    readonly object _syncRoot = new object();

    public SynchronizedDictionary()
    {
        _innerDict = new Dictionary<TKey, TValue>();
    }

    public object SyncRoot { get { return _syncRoot; } }

    public ICollection<TKey> Keys
    {
        get
        {
            lock (_syncRoot)
            {
                return _innerDict.Keys;
            }
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            lock (_syncRoot)
            {
                return _innerDict.Values;
            }
        }
    }

    public TValue this[TKey key]
    {
        get
        {
            lock (_syncRoot)
            {
                return _innerDict[key];
            }
        }
        set
        {
            lock (_syncRoot)
            {
                _innerDict[key] = value;
            }
        }
    }

    public int Count
    {
        get
        {
            lock (_syncRoot)
            {
                return _innerDict.Count;
            }
        }
    }

    public bool IsReadOnly { get { return false; } }

    public void Add(TKey key, TValue value)
    {
        lock (_syncRoot)
        {
            _innerDict.Add(key, value);
        }
    }

    public bool ContainsKey(TKey key)
    {
        lock (_syncRoot)
        {
            return _innerDict.ContainsKey(key);
        }
    }

    public bool Remove(TKey key)
    {
        lock (_syncRoot)
        {
            return _innerDict.Remove(key);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        lock (_syncRoot)
        {
            return _innerDict.TryGetValue(key, out value);
        }
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        lock (_syncRoot)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)_innerDict).Add(item);
        }
    }

    public void Clear()
    {
        lock (_syncRoot)
        {
            _innerDict.Clear();
        }
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        lock (_syncRoot)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)_innerDict).Contains(item);
        }
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        lock (_syncRoot)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)_innerDict).CopyTo(array, arrayIndex);
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        lock (_syncRoot)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)_innerDict).Remove(item);
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _innerDict.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _innerDict.GetEnumerator();
    }
}