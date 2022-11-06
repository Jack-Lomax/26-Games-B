using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBall : MonoBehaviour
{
    float speed = 1;
    float maxHeight;

    void Start()
    {
        speed = Random.Range(0.1f, 0.3f);
        maxHeight = Random.Range(0, .1f);
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * Mathf.PI * 2 * speed) * maxHeight;
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.y);
    }
}
