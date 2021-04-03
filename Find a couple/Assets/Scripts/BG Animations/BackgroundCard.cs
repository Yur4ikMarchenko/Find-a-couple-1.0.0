using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCard : MonoBehaviour
{
    public float movingSpeed;
    public float rotationSpeed;
    public int numberOfCardRows;
    public float scaleMultiplier;

    RectTransform canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        float cardHeight = canvasRect.rect.height / numberOfCardRows;
        float scaleF = cardHeight / (GetComponent<MeshRenderer>().bounds.size.y / transform.localScale.y) * canvasRect.localScale.y * scaleMultiplier;
        transform.localScale = new Vector3(scaleF, scaleF, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 360 * Time.deltaTime * rotationSpeed, 0);
        transform.localPosition = transform.localPosition + new Vector3(movingSpeed * Time.deltaTime, 0, 0);

        if (Mathf.Abs(transform.localPosition.x) > canvasRect.rect.width * 0.7)
            GameObject.Destroy(this.gameObject);
    }
}
