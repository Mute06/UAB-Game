using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtentions 
{
    public static bool isLayerInLayermask(LayerMask layermask, int layer)
    {
        return layermask == (layermask | (1 << layer));
    }
}
