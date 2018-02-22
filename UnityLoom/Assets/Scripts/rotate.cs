using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.back;
    }
}