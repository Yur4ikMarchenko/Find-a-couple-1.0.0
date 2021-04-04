using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;
    public int numberOfCardRows;

    float SpawnNewCards()
    {
        GameObject t = null;
        BackgroundCard card = null;

        for (int i = 0; i < numberOfCardRows; ++i)
        {
            if(i%2 == 0)
            {
                t = Instantiate(cardPrefab, canvasRect.transform);
                t.transform.localPosition = new Vector3(-canvasRect.rect.width * 0.6f, canvasRect.rect.height * 0.4f - cardHeight * i, 30);
                t.transform.rotation = new Quaternion(0, 180, 0, 0);
                t.GetComponent<BackgroundCard>().numberOfCardRows = numberOfCardRows;


                t = Instantiate(cardPrefab, canvasRect.transform);
                t.transform.localPosition = new Vector3(-canvasRect.rect.width * 0.6f, canvasRect.rect.height * 0.4f - cardHeight * i, 30);
                t.transform.rotation = new Quaternion(0, 0, 0, 0);
                t.GetComponent<BackgroundCard>().numberOfCardRows = numberOfCardRows;
            }
            else
            {
                t = Instantiate(cardPrefab, canvasRect.transform);
                t.transform.localPosition = new Vector3(canvasRect.rect.width * 0.6f, canvasRect.rect.height * 0.4f - cardHeight * i, 30);
                t.transform.rotation = new Quaternion(0, 180, 0, 0);
                card = t.GetComponent<BackgroundCard>();
                card.numberOfCardRows = numberOfCardRows;
                card.rotationSpeed *= -1;
                card.movingSpeed *= -1;

                t = Instantiate(cardPrefab, canvasRect.transform);
                t.transform.localPosition = new Vector3(canvasRect.rect.width * 0.6f, canvasRect.rect.height * 0.4f - cardHeight * i, 30);
                t.transform.rotation = new Quaternion(0, 0, 0, 0);
                card = t.GetComponent<BackgroundCard>();
                card.numberOfCardRows = numberOfCardRows;
                card.rotationSpeed *= -1;
                card.movingSpeed *= -1;
            }

        }
        timePassedAfterLastSpawn = 0;

        return t.GetComponent<MeshRenderer>().bounds.size.x / Mathf.Abs(card.movingSpeed) * 3.5f;
    }

    RectTransform canvasRect;
    float cardHeight;

    float pauseBetweenSpawns;
    float timePassedAfterLastSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        cardHeight = canvasRect.rect.height / numberOfCardRows;

        pauseBetweenSpawns = SpawnNewCards();

        Debug.Log(pauseBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        timePassedAfterLastSpawn += Time.deltaTime;
        if(timePassedAfterLastSpawn >= pauseBetweenSpawns)
        {
            timePassedAfterLastSpawn = 0;
            SpawnNewCards();
        }
    }
}
