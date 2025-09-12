using UnityEngine;

public static class BulletRoot
{
    private static Transform _cached;
    
    public static Transform GetOrCreate()
    {
        if (_cached) return _cached;

        var found = GameObject.Find("BulletParent");
        if (found) return _cached = found.transform;

        var go = new GameObject("BulletParent"); 
        return _cached = go.transform;
    }
}
