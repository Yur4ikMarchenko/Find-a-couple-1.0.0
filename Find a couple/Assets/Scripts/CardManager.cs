using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;


public class CardManager : MonoBehaviour
{
    string[] number = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    string[] mast = new string[] { "Club", "Diamond", "Heart", "Spades" };
    Object prefab;
    GameObject card;

    void Start()
    {
        var playingCars = from n in number
                          from m in mast
                          select n + m;


        prefab = Resources.Load("Free_Playing_Cards/PlayingCards_"+playingCars.ToArray()[Random.Range(0,playingCars.ToArray().Length - 1)]); 
        card = (GameObject)Instantiate(prefab, new Vector3(-8, 2.6f, 76), Quaternion.identity);
        card.transform.localScale = new Vector3(500, 500, 500);




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
