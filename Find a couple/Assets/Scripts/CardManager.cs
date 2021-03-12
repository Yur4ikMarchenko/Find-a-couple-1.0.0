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

    GameObject GetPickedCard()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            for (int i = 0; i < numberOfCards; ++i)
                if (hit.collider == cards[i].GetComponent<Collider>())
                    return cards[i];
        }
        return null;
    }

    void ResizeField()
    {
        canvas = FindObjectOfType<Canvas>();
        menu = FindObjectOfType<MenuButton>().GetComponent<RectTransform>();

        screen = canvas.GetComponent<RectTransform>();

        otstupY = screen.rect.height * 0.2f;

        menu.transform.localPosition = new Vector3(-screen.rect.width / 2 + screen.rect.width * 0.13f, screen.rect.height / 2 - otstupY / 2, 0);
        menu.transform.localScale = new Vector3(screen.rect.width * 0.2f / menu.rect.width, screen.rect.height * 0.15f / menu.rect.height, 1);
        

        float cardHeight = (screen.rect.height - otstupY) / numberOfRows;
        float cardWidth = (screen.rect.width) / numberOfColumns;

        Vector3 scale = new Vector3(cardWidth / (cards[0].GetComponent<MeshRenderer>().bounds.size.x / cards[0].transform.parent.localScale.x) * screen.transform.localScale.x * scaleMulX,
            cardHeight / (cards[0].GetComponent<MeshRenderer>().bounds.size.y / cards[0].transform.parent.localScale.y) * screen.transform.localScale.y * scaleMulY, 1);


        //set card positions
        for (int row = 0; row < numberOfRows; ++row)
        {
            for (int column = 0; column < numberOfColumns && row * numberOfColumns + column < numberOfCards; ++column)
            {
                cards[row * numberOfColumns + column].transform.parent.localScale = scale;
                cards[row * numberOfColumns + column].transform.parent.localPosition = new Vector3(-screen.rect.width / 2 + cardWidth / 2 + cardWidth * column,
                    screen.rect.height / 2 - otstupY - cardHeight / 2 - cardHeight * row, 0);
            }
        }
    }


    public float scaleMulY = 0.9f;
    public float scaleMulX = 0.6f;

    public int numberOfCards;

    float otstupY;

    int numberOfRows;
    int numberOfColumns;
    int flippedPairs;

    GameObject[] cards;

    GameObject FlippedCard;
    GameObject CardToUnFlip;

    Canvas canvas;
    RectTransform screen;
    RectTransform menu;

    string[] number = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    string[] mast = new string[] { "Club", "Diamond", "Heart", "Spades" };

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        menu = FindObjectOfType<MenuButton>().GetComponent<RectTransform>();

        //numberOfCards = 46;
        FlippedCard = null;
        CardToUnFlip = null;

        flippedPairs = 0;

        numberOfRows = CalculateNumberOfRows(numberOfCards);
        numberOfColumns = numberOfCards / numberOfRows;
        if (numberOfColumns * numberOfRows < numberOfCards) ++numberOfColumns;


        var playingCars = from n in number
                          from m in mast
                          select n + m;

        GameObject t = new GameObject();
        t.name = "Card";

        Object prefab;

        //initialize cards array
        cards = new GameObject[numberOfCards];
        object animationFlip = Resources.Load("Animations/CardFlip");
        object animationUnFlip = Resources.Load("Animations/CardUnFlip");
        //init random pairs of cards
        for (int i = 0; i < numberOfCards - 1; i += 2) 
        {
            //load random card
            prefab = Resources.Load("Free_Playing_Cards/PlayingCards_"+playingCars.ToArray()[Random.Range(0,playingCars.ToArray().Length)]);


            //create empty pair of "Card" objects
            cards[i] = Instantiate(t, canvas.transform);
            cards[i + 1] = Instantiate(t, canvas.transform);

            //create actual pair of cards inside empty object and add an animation
            cards[i] = (GameObject)Instantiate(prefab, cards[i].transform);
            cards[i + 1] = (GameObject)Instantiate(prefab, cards[i + 1].transform);

            cards[i].AddComponent<Card>();
            cards[i + 1].AddComponent<Card>();

            cards[i].AddComponent<Animation>();
            cards[i].GetComponent<Animation>().AddClip((AnimationClip)animationFlip, "Flip");
            cards[i].GetComponent<Animation>().AddClip((AnimationClip)animationUnFlip, "UnFlip");
            cards[i + 1].AddComponent<Animation>();
            cards[i + 1].GetComponent<Animation>().AddClip((AnimationClip)animationFlip, "Flip");
            cards[i + 1].GetComponent<Animation>().AddClip((AnimationClip)animationUnFlip, "UnFlip");

            cards[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            cards[i + 1].transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        GameObject.Destroy(t);

        //shuffle cards
        for (int i = 0; i < numberOfCards; ++i)
        {
            int j = Random.Range(0, numberOfCards);
            var temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }

        ResizeField();
    }




    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CardToUnFlip == null)
        {
            var pick = GetPickedCard();
            if (pick != null && !pick.GetComponent<Card>().IsFlipped && !pick.GetComponent<Animation>().isPlaying)
            {
                var card = pick.GetComponent<Card>();
                
                if (FlippedCard == null)
                {
                    if(CardToUnFlip == null)
                    {
                        card.Flip();
                        FlippedCard = pick;
                    }
                    
                }
                else
                {
                    card.Flip();
                    if (pick.GetComponent<MeshRenderer>().name != FlippedCard.GetComponent<MeshRenderer>().name)
                        CardToUnFlip = pick;
                    else
                    {
                        FlippedCard = null;
                        ++flippedPairs;
                        Debug.Log("Flipped " + flippedPairs + " paids");
                    }
                }
            }
        }
        if (CardToUnFlip != null && !CardToUnFlip.GetComponent<Animation>().isPlaying)
        {
            CardToUnFlip.GetComponent<Card>().UnFlip();
            FlippedCard.GetComponent<Card>().UnFlip();
            CardToUnFlip = null;
            FlippedCard = null;
        }
        ResizeField();
    }
}
