using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    const float DELAY = 0.2f;
    void Start()
    {
        Destroy(gameObject, DELAY);
    }
}
