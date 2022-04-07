using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class popUpText : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float disappearTimer;
    private TextMeshPro textMP;
    private Color textColor;

    private void Start()
    {
        //moveSpeed = 20f;
        textMP = GetComponent<TextMeshPro>();
        textColor = textMP.color;
        disappearTimer = .3f;

    }
    void Update()
    {
        moveSpeed -= Time.deltaTime;
        if (moveSpeed <= 0)
            moveSpeed = 0;

        if (transform.position.y > 0)
            moveSpeed = -moveSpeed;
        //if (transform.position.y < 0)
        //    moveSpeed = -moveSpeed;

        transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if(disappearTimer <0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMP.color = textColor;
            if (textColor.a < 0)
                Destroy(gameObject);
        }

    }
}
