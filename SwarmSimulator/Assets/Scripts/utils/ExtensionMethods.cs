using UnityEngine;
using Something;

public static class ExtensionMethods
{
    public static Field getField(this Field[,,] env, Vector3Int coords ){
        return env[coords.x, coords.y, coords.z];
    }
}