using UnityEngine;
using Something;
using System;
using Random = UnityEngine.Random;

public static class ExtensionMethods
{
    public static void ClampCoords(this Entity[,,] env, ref Vector3Int coords ){
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

    public static Vector3 ScaleDownRandomly(this Vector3 original)
    {
        return new Vector3(Random.Range(0, original.x-1), Random.Range(0, original.y-1), Random.Range(0, original.z-1));
    }

    public static Vector3Int ScaleDownRandomly(this Vector3Int original)
    {
        return new Vector3Int(Random.Range(0, original.x-1), Random.Range(0, original.y-1), Random.Range(0, original.z-1));
    }
}