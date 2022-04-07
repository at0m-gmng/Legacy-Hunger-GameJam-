using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldenBuff : MonoBehaviour
{
    [SerializeField] private float goldenAppleScore = 15f;
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.layer == 3)
        {
            gameController.Instance.popUpTextSpawner(collider.gameObject.transform, false, goldenAppleScore);
            gameController.Instance.addScore(goldenAppleScore, false, collider.gameObject);
            Destroy(gameObject, .1f);
        }
    }
}
