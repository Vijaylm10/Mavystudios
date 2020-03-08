using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> :ScriptableObject where T :ScriptableObject
{
    private static T instance = null;

    public static T _instance
    {
        get
        {
            if(instance==null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if(results.Length==0)
                {
                   
                    return null;
                }
                if(results.Length>1)
                {
                   
                    return null;
                }
                instance = results[0];
            }
            return instance;
        }
    }
    
}
