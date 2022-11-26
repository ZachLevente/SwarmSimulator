using UnityEngine;
using Something;
using System;

public static class ExtensionMethods
{
    public static Field GetField(this Field[,,] env, Vector3Int coords ){
        return env[coords.x, coords.y, coords.z];
    }

    public static void ClampCoords(this Field[,,] env, ref Vector3Int coords ){
        coords.x = Math.Min(Math.Max(coords.x, 0), env.GetLength(0)-1);
        coords.y = Math.Min(Math.Max(coords.y, 0), env.GetLength(1)-1);
        coords.z = Math.Min(Math.Max(coords.z, 0), env.GetLength(2)-1);
    }
    
    public static Transform KillAllChildren(this Transform transform)
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        
        return transform;
    }
}