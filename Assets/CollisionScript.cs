using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CollisionScript : MonoBehaviour
{
    public TextMeshProUGUI CountText;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        SetCountText();
    }

    // Update is called once per frame
    void SetCountText()
    {
        CountText.text = "Count: " + count.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    
}
