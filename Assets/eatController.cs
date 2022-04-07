using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eatController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Image image;
    [SerializeField] private Sprite[] imageBuild;
    public bool onPoison; 
    private Color color;

    private float destroyOnPoisonTime = 3f;
    private float eatUpdateTime = 2.5f;
    public Vector3 startPosition;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        image.sprite = imageBuild[Random.Range(0, imageBuild.Length)];
        onPoison = System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
        startPosition = transform.localPosition;
        if (image.sprite.name == "Fruits_0" || image.sprite.name == "Fruits_1" || image.sprite.name == "Fruits_3" || image.sprite.name == "Fruits_4"
            || image.sprite.name == "Fruits_5" || image.sprite.name == "Fruits_6" || image.sprite.name == "Fruits_7" || image.sprite.name == "Fruits_8")
        {
            transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 350f, gameObject.transform.localPosition.z);
            rb.gravityScale = 0;
            Invoke("gravityON", .5f);
        }

        if (onPoison)
        {
            if (ColorUtility.TryParseHtmlString("#934A64", out color))
            { 
                image.color = color; 
            }
            Invoke("eatDestroy", destroyOnPoisonTime);
        }

        //gameController.Instance.popUpTextSpawner(transform);
    }

    private void gravityON()
    {
        rb.gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.gameObject.layer);
        gameController.Instance.addScore(0, onPoison, collider.gameObject);
        if (collider.gameObject.layer == 3 || collider.gameObject.layer == 10)
        {
            //всплывающий текст
            //Instantiate(popUpText, gameObject.transform.localPosition, Quaternion.identity);

            if (collider.gameObject.layer == 3 && 
                (image.sprite.name == "Fruits_0" || image.sprite.name == "Fruits_1" || image.sprite.name == "Fruits_3" || image.sprite.name == "Fruits_4"
                || image.sprite.name == "Fruits_5" || image.sprite.name == "Fruits_6" || image.sprite.name == "Fruits_7" || image.sprite.name == "Fruits_8"))
            {
                transform.localPosition = startPosition; // Для избежания бага, когда объест ловят в воздухе и стартовая позиция по Y сдвигается вверх
            }
            gameController.Instance.popUpTextSpawner(collider.gameObject.transform, onPoison, 0);
            eatDestroy();
        }
    }
    private void eatDestroy()
    {
        gameObject.SetActive(false);
        //Instantiate(popUpText, transform);
        //вызываем метод спавна текста
        Invoke("eatSpawner", eatUpdateTime);
        Destroy(gameObject, eatUpdateTime);
    }

    private void eatSpawner()
    {
        gameController.Instance.eatSpawner(new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z));
    }
}
