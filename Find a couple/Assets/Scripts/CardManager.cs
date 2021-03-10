using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class CardManager : MonoBehaviour
{
    public int numberOfCards;

    GameObject[] cards;

    string[] number = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    string[] mast = new string[] { "Club", "Diamond", "Heart", "Spades" };
    Object prefab;
    //GameObject card;

    int scale;

    void Start()
    {
        numberOfCards = 50;
        scale = 7000 / numberOfCards;

        

        var playingCars = from n in number
                          from m in mast
                          select n + m;

        cards = new GameObject[numberOfCards];


        float otstup;
        for(int i = 0; i < numberOfCards / 2;++i)
        {
            prefab = Resources.Load("Free_Playing_Cards/PlayingCards_"+playingCars.ToArray()[Random.Range(0,playingCars.ToArray().Length)]);

            cards[i] = (GameObject)Instantiate(prefab, FindObjectOfType<Canvas>().transform);
            cards[i].transform.localScale = new Vector3(scale, scale, scale);
            cards[i].transform.rotation = new Quaternion(0, 180, 0, 0);
            cards[i].transform.localPosition = new Vector3(cards[i].GetComponent<MeshRenderer>().bounds.size.x * 2 + cards[i].GetComponentInParent<RectTransform>().rect.width*(-1 / 2f +  (1 * 2f / numberOfCards) * i), cards[i].GetComponentInParent<RectTransform>().rect.height * (-1 / 2f+ 4/6f), 0);


            Debug.Log(cards[i].GetComponent<MeshRenderer>().bounds.size.x);

            cards[i + 1] = (GameObject)Instantiate(prefab, FindObjectOfType<Canvas>().transform);
            cards[i + 1].transform.localScale = new Vector3(scale, scale, scale);
            cards[i + 1].transform.rotation = new Quaternion(0, 180, 0, 0);
            cards[i + 1].transform.localPosition = new Vector3(cards[i].GetComponent<MeshRenderer>().bounds.size.x * 2 + cards[i].GetComponentInParent<RectTransform>().rect.width * (-1 / 2f + (1 * 2f / numberOfCards) * i), cards[i].GetComponentInParent<RectTransform>().rect.height * (-1 / 2f + 1.5f / 6), 0);

        }

        //card = (GameObject)Instantiate(prefab, FindObjectOfType<Canvas>().transform);
        //card.transform.localScale = new Vector3(500, 500, 500);
        //card.transform.rotation = new Quaternion(0, 180, 0, 0);


        //GameObject ccc = Instantiate(card);
        //ccc.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Assets/Free_playing_cards/PlayingCards_4Diamond.fbx");
        //ccc.GetComponent<MeshFilter>().mesh.name = "PlayingCards_213Diamond";// + playingCars.ToArray()[Random.Range(0, playingCars.ToArray().Length) - 1] ;
        //Debug.Log(ccc.GetComponent<MeshFilter>().mesh.name);
        //GameObject cc = Instantiate(ccc);
    }

    void Update()
    {
        
    }
}
