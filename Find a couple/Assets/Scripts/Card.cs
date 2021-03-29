using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public void Flip()
    {
        GetComponent<Animation>().Play("Flip");
        IsFlipped = true;
    }

    public void UnFlip()
    {
        GetComponent<Animation>().Play("UnFlip");
        IsFlipped = false;
    }

    public bool IsFlipped { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        IsFlipped = false;
        gameObject.AddComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Destroy(GetComponent<BoxCollider>());
        gameObject.AddComponent<BoxCollider>();
        Debug.Log(GetComponent<BoxCollider>().size.x);
    }
}
