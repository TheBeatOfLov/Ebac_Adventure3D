using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotheChanger : MonoBehaviour
{
    public List<SkinnedMeshRenderer> meshList;

    public Color color;
    private Color _defaultColor = Color.white;
    public string shaderIdName = "_EmissionColor";

    [NaughtyAttributes.Button]
    public void ChangeColor()
    {
        foreach(var i in meshList)
        i.materials[0].SetColor(shaderIdName, color);
    }

    public void ChangeColor(ClotheSetup setup)
    {
       foreach (var i in meshList) i.materials[0].SetColor(shaderIdName, setup.color);
    }

    public void ResetColor()
    {
        foreach (var i in meshList) i.materials[0].SetColor(shaderIdName, _defaultColor);
    }
}
