using UnityEngine;
using Something;

public static class ExtensionMethods
{
    public static Field getField(this Field[,,] env, Vector3Int coords ){
        return env[coords.x, coords.y, coords.z];
    }
    
    public static Transform KillAllChildren(this Transform transform)
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        
        return transform;
    }
}