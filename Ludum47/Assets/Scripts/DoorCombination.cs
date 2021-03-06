﻿using System.Collections.Generic;
using UnityEngine;

public class DoorCombination : MonoBehaviour
{
    public List<int> CodeOpen;
    public List<int> CodeSwitchOpen;
    public bool NeedOrder;
    public bool NeedOtherAlive; // For no defined enemies on the combination
    public int DoorOpen;
    public bool FinalComb;
}
