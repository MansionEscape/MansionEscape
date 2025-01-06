using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundScript : MonoBehaviour
{
    public static BackgroundSoundScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
