using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class CardManager : MonoBehaviour
{
    int CalculateNumberOfRows(int n)
    {
        int i = 0;
        while(n > 0)
        {
            for (int j = 0; j<=i; ++j)
                n -= 2;
            ++i;
        }
        return i;
    }

    public int numberOfCards;

    GameObject[] cards;

    string[] number = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    string[] mast = new string[] { "Club", "Diamond", "Heart", "Spades" };
    Object prefab;
    object animation;
    //GameObject card;

    void Start()
    {
        numberOfCards = 46;


        int numberOfRows = CalculateNumberOfRows(numberOfCards);
        int numberOfColumns = numberOfCards / numberOfRows;
        if (numberOfColumns * numberOfRows < numberOfCards) ++numberOfColumns;


        var playingCars = from n in number
                          from m in mast
                          select n + m;

        cards = new GameObject[numberOfCards];

        //init random pairs of cards
        for (int i = 0; i < numberOfCards - 1; i += 2) 
        {
            prefab = Resources.Load("Free_Playing_Cards/PlayingCards_"+playingCars.ToArray()[Random.Range(0,playingCars.ToArray().Length)]);


            //cards[i].AddComponent<Card>();
            //cards[i + 1].AddComponent<Card>();
            cards[i] = (GameObject)Instantiate(prefab, FindObjectOfType<Canvas>().transform);
            cards[i + 1] = (GameObject)Instantiate(prefab, FindObjectOfType<Canvas>().transform);

            //cards[i].AddComponent<Animation>();
            //cards[i + 1].AddComponent<Animation>();
            //animation = Resources.Load("Animations/flip");

            //cards[i].GetComponent<Animation>().AddClip((AnimationClip)animation, "flip");
            //cards[i + 1].GetComponent<Animation>().AddClip((AnimationClip)animation, "flip");
            //cards[i].GetComponent<Animation>().Play("flip");
            //cards[i + 1].GetComponent<Animation>().Play("flip");


            cards[i].transform.rotation = new Quaternion(0, 180, 0, 0);
            cards[i + 1].transform.rotation = new Quaternion(0, 180, 0, 0);
            }

        //shuffle cards
        for (int i = 0; i < numberOfCards; ++i)
        {
            int j = Random.Range(0, numberOfCards);
            var temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }


        Rect screen = cards[0].GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect;

        float otstupY = 35;
        float otstupX = 0;

        float cardHeight = (screen.height - otstupY) / numberOfRows;
        float cardWidth = (screen.width) / numberOfColumns;

        
        Vector3 scale = new Vector3(cardWidth / cards[0].GetComponent<MeshRenderer>().bounds.size.x / 2,
            cardHeight / cards[0].GetComponent<MeshRenderer>().bounds.size.y / 2 , 1);

        //set card positions
        for (int row = 0; row < numberOfRows; ++row) 
        {
            for (int column = 0; column < numberOfColumns && row * numberOfColumns + column < numberOfCards; ++column) 
            {
                cards[row * numberOfColumns + column].transform.localScale = scale;
                cards[row * numberOfColumns + column].transform.localPosition = new Vector3(- screen.width / 2 + cardWidth / 2 + cardWidth * column,
                    screen.height / 2 - otstupY - cardHeight / 2 - cardHeight * row, 0) ;
            }
        }
    }

    void Update()
    {

    }
}
