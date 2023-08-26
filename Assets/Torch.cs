using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Torch : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TorchCounter.instance.AddTorch();
        }

    }




}
