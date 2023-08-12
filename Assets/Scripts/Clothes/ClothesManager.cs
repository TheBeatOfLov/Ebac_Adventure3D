using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public enum ClotheType
{
    SPEED,
    STRENGHT
}
public class ClothesManager : Singleton<ClothesManager>
{
    public List<ClotheSetup> clotheSetups;

    public ClotheSetup GetClotheSetupByType(ClotheType type)
    {
        return clotheSetups.Find(i => i.clotheType == type);
    }
}
[System.Serializable]
public class ClotheSetup
{
    public ClotheType clotheType;
    public Color color;
}
