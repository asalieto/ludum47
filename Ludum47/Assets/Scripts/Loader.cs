﻿using UnityEngine;

public class Loader : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
