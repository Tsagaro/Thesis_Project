using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TorchCounter : MonoBehaviour
{
    public static TorchCounter instance;
    public TextMeshProUGUI text;
    int count=0;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance =this;
        }

    }
    public void AddTorch()
    {
        count++;      
        text.text = count.ToString();
    }
    
}
