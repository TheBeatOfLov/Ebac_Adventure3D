using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOIntUIUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uiTextValue.text = soInt.value.ToString();
    }
}
