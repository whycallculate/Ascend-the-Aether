using CardTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic Create SO", menuName = "Relic Create SO", order = 0)]
public class RelicScriptableObject : ScriptableObject
{
    public StatRelics statRelic;
    public StatusRelics statusRelic;

}
