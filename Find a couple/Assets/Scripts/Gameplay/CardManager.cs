using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    bool UpdateTimer()
    {
        string rez = "";

        timeLimit -= Time.deltaTime;
        if (timeLimit <= 0)
        {
            timer.text = "00:00";
            return true;
        }
        float secs = timeLimit % 60;
        float mins = (timeLimit - secs) / 60;
        secs -= secs % 1 - 1;

        if (mins < 10) rez += '0';
        rez += mins.ToString() + ':';
        if (secs < 10) rez += '0';
        rez += secs.ToString();

        timer.text = rez;
        return false;
    }

    bool UpdateTries()
    {
        --triesLimit;
        tries.text = "Tries left: " + triesLimit.ToString();
        if (triesLimit == 0)
            return true;
        return false;
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.gameObject.SetActive(true);
    }

    void Victory()
    {
        if (LevelManager.casual)
            casualVictoryPanel.SetActive(true);
        else
        {
            LevelManager.Clear();
            victoryPanel.gameObject.SetActive(true);
        }
        victory = true;
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
        screen = canvas.GetComponent<RectTransform>();

        otstupY = screen.rect.height * 0.2f;

        float cardHeight = (screen.rect.height - otstupY) / numberOfRows;
        float cardWidth = (screen.rect.width) / numberOfColumns;

        float scaleF = cardHeight / (cards[0].GetComponent<MeshRenderer>().bounds.size.y / cards[0].transform.parent.localScale.y) * screen.transform.localScale.y * scaleMulY;

        Vector3 scale = new Vector3(scaleF,scaleF,1);

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

    public GameObject menu;
    public GameObject options;


    Text timer;
    Text tries;

    bool gameOver;
    bool victory;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject casualVictoryPanel;

    public float timeLimit;
    public int triesLimit;

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

    string[] number = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    string[] mast = new string[] { "Club", "Diamond", "Heart", "Spades" };

    float spawnAnimationLength = 59 / 60f;     //105 frames of 60fps animation
    float unFlipAnimationLength = 45 / 60f;      
    float initialCardOpeningDuration;      //for how long cards will be shown at the begining, seconds
    float timeFromStart = 0;
    float startGameAfter;

    void Start()
    {

        //load level data
        GameObject.FindObjectOfType<AudioSource>().volume= Options.volume;
        numberOfCards = LevelManager.pairs * 2;
        timeLimit = LevelManager.limit;
        triesLimit = LevelManager.tries;

        //get hint duration from level settings or use a simple formula if game mode is set to casual;
        initialCardOpeningDuration = LevelManager.casual ? Mathf.Sqrt(LevelManager.pairs * 0.7f) : LevelManager.hintDuration;
        startGameAfter = spawnAnimationLength + unFlipAnimationLength + initialCardOpeningDuration;

        gameOver = false;
        victory = false;

        timer = GameObject.Find("Timer").GetComponent<Text>();
        tries = GameObject.Find("Tries").GetComponent<Text>();
        tries.text += triesLimit;

        canvas = FindObjectOfType<Canvas>();

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
        object animationSpawn = Resources.Load("Animations/Spawn");
        bool[] usedCards = new bool[playingCars.ToArray().Length];
        for(int i = 0; i< usedCards.Length;++i)
        {
            usedCards[i] = false;
        }
        //init random pairs of cards    
        for (int i = 0; i < numberOfCards - 1; i += 2) 
        {
            //find card that hasn't been used yet
            int temp;
            do
            {
                temp = Random.Range(0, usedCards.Length);
            }
            while (usedCards[temp]);
            usedCards[temp] = true;
            //load random card
            prefab = Resources.Load("Free_Playing_Cards/PlayingCards_"+playingCars.ToArray()[temp]);


            //create empty pair of "Card" objects
            cards[i] = Instantiate(t, canvas.transform);
            cards[i + 1] = Instantiate(t, canvas.transform);

            //create actual pair of cards inside empty object and add some animations
            cards[i] = (GameObject)Instantiate(prefab, cards[i].transform);
            cards[i + 1] = (GameObject)Instantiate(prefab, cards[i + 1].transform);

            cards[i].AddComponent<Card>();
            cards[i + 1].AddComponent<Card>();

            cards[i].AddComponent<Animation>();
            cards[i].GetComponent<Animation>().AddClip((AnimationClip)animationFlip, "Flip");
            cards[i].GetComponent<Animation>().AddClip((AnimationClip)animationUnFlip, "UnFlip");
            cards[i].GetComponent<Animation>().AddClip((AnimationClip)animationSpawn, "Spawn");
            cards[i + 1].AddComponent<Animation>();
            cards[i + 1].GetComponent<Animation>().AddClip((AnimationClip)animationFlip, "Flip");
            cards[i + 1].GetComponent<Animation>().AddClip((AnimationClip)animationUnFlip, "UnFlip");
            cards[i + 1].GetComponent<Animation>().AddClip((AnimationClip)animationSpawn, "Spawn");

            cards[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            cards[i + 1].transform.rotation = new Quaternion(0, 0, 0, 0);

            //playing spawn animation
            cards[i].GetComponent<Animation>().Play("Spawn");
            cards[i + 1].GetComponent<Animation>().Play("Spawn");
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

        if(LevelManager.casual)
        {
            timer.gameObject.SetActive(false);
            tries.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //wait some time after begining of the level and hide cards
        if(!cards[0].GetComponent<Animation>().IsPlaying("Unflip") && timeFromStart < startGameAfter && timeFromStart > spawnAnimationLength + initialCardOpeningDuration)
            for(int i = 0; i < numberOfCards;++i)
                cards[i].GetComponent<Card>().UnFlip();
        timeFromStart += Time.deltaTime;


        if (timeFromStart > startGameAfter && !menu.gameObject.activeSelf && !options.gameObject.activeSelf && Input.GetMouseButtonDown(0) && CardToUnFlip == null && !gameOver)
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
                    }
                }
            }
        }
        if (timeFromStart > startGameAfter && !menu.gameObject.activeSelf && !options.gameObject.activeSelf && CardToUnFlip != null && !CardToUnFlip.GetComponent<Animation>().isPlaying)
        {
            if (!LevelManager.casual && UpdateTries())
                GameOver();
            CardToUnFlip.GetComponent<Card>().UnFlip();
            FlippedCard.GetComponent<Card>().UnFlip();
            CardToUnFlip = null;
            FlippedCard = null;
        }


        if (timeFromStart > startGameAfter && !menu.gameObject.activeSelf && !options.gameObject.activeSelf && !gameOver && !victory)
        {
            if(!LevelManager.casual && UpdateTimer())
               GameOver();
            if (flippedPairs == numberOfCards / 2)
               Victory();
        }
    }
}
