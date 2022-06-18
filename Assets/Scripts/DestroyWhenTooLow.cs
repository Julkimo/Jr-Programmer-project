using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenTooLow : MonoBehaviour
{
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
}
