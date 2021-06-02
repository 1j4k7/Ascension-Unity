using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Buttons
    public Toggle DrawPileButton;
    public Toggle DiscardPileButton;

    // Containers
    public GameObject Menu;
    public GameObject CenterDeckContainer;
    public GameObject DrawPileContainer;
    public GameObject VoidPileContainer;
    public GameObject DiscardPileContainer;
    public GameObject CenterRowContainer;
    public GameObject StaticCardsContainer; // Mystic, Heavy Infantry, and Cultist
    public GameObject PlayArea;
    public GameObject Hand;

    // Counters
    public TextMeshProUGUI HonorPoolCounter;
    public TextMeshProUGUI HonorCounter;
    public TextMeshProUGUI RunesCounter;
    public TextMeshProUGUI PowerCounter;
    public TextMeshProUGUI PlayerCounter;

    // Prefabs
    public Player PlayerPrefab;

    // Logical
    private Player[] players;
    private int totalHonor;
    private int playerIdx; // whose turn it is

    // Start is called before the first frame update
    void Start()
    {
        /*
        * Setup game
        */
        // Initialize players
        players = new Player[4];
        for (int i = 0; i < players.Length; i++) {
            players[i] = Instantiate<Player>(PlayerPrefab);
        }

        // Initialize total honor
        if (players.Length == 2) {
            totalHonor = 60;
        } else if (players.Length == 3) {
            totalHonor = 75;
        } else if (players.Length == 4) {
            totalHonor = 90;
        } else {
            Debug.Log($"Bad number of players: {players.Length}");
        }

        // Initialize static cards
        GameObject staticCardPrefab = Resources.Load<GameObject>("Prefabs/Cards/Mystic Variant");
        StaticCardsContainer.GetComponent<StaticCardsContainer>().Add(Instantiate(staticCardPrefab));
        staticCardPrefab = Resources.Load<GameObject>("Prefabs/Cards/HeavyInfantry Variant");
        StaticCardsContainer.GetComponent<StaticCardsContainer>().Add(Instantiate(staticCardPrefab));
        staticCardPrefab = Resources.Load<GameObject>("Prefabs/Cards/Cultist Variant");
        StaticCardsContainer.GetComponent<StaticCardsContainer>().Add(Instantiate(staticCardPrefab));


        // Initialize center deck and verify it equals 100 cards
        var cards = Resources.LoadAll<GameObject>("Prefabs/Cards").Where(card => card.tag == "InitialDeck").ToList();
        foreach (GameObject cardPrefab in cards) {
            Card cardScriptComponent = cardPrefab.GetComponent<Card>();
            for (int i = 0; i < cardScriptComponent.initNum; i++) {
                GameObject card = Instantiate(cardPrefab);
                CenterDeckContainer.GetComponent<Container>().Add(card);
            }
        }
        Debug.Log($"Final Deck Count: {CenterDeckContainer.GetComponent<Container>().cards.Count}");

        // Shuffle center deck
        CenterDeckContainer.GetComponent<CenterDeckContainer>().Shuffle();

        // Deal first six cards into the center row
        for (int i = 0; i < 6; i++) {
            GameObject card = CenterDeckContainer.GetComponent<Container>().RemoveAt(0);
            CenterRowContainer.GetComponent<Container>().Add(card);
        }

        // Randomly choose which player goes first
        playerIdx = Random.Range(0, players.Length);

        // Display draw pile, hand, and discard pile for that player
        DrawPileContainer.GetComponent<PlayerContainer>().UpdateSelf(players[playerIdx].DrawPile);
        Hand.GetComponent<PlayerContainer>().UpdateSelf(players[playerIdx].Hand);
        DiscardPileContainer.GetComponent<PlayerContainer>().UpdateSelf(players[playerIdx].DiscardPile);

        /*
        * Random settings
        */
        // Match fullscreen toggle
        Menu.GetComponentInChildren<Toggle>(true).isOn = Screen.fullScreen; // TODO: replace this with an actual reference
    }

    // Update is called once per frame
    void Update()
    {
        // Update all counters according to what player it is (Consider only updating this when it changes as well)
        HonorPoolCounter.SetText($"{totalHonor}");
        RunesCounter.SetText($"{players[playerIdx].runes}");
        HonorCounter.SetText($"{players[playerIdx].honor}");
        PowerCounter.SetText($"{players[playerIdx].power}");
        PlayerCounter.SetText($"Player {playerIdx+1}");

        // Update all containers to match their logical counterparts ONLY when they are changed 
    }

    /***************************
    * Button/Toggle Events
    ***************************/

    public void ToggleMenu(bool value) {
        Menu.SetActive(value);
    }
    public void FullScreenToggle(bool value) {
        Screen.fullScreen = value;
    }
    public void Exit2Menu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
    public void ToggleVoidPile(bool value) {
        VoidPileContainer.SetActive(value);
    }
    public void ToggleDrawPile(bool value) {
        if (value && DiscardPileContainer.activeSelf) {
            DiscardPileButton.isOn = false;
        }
        DrawPileContainer.SetActive(value);
    }
    public void ToggleDiscardPile(bool value) {
        if (value && DrawPileContainer.activeSelf) {
            DrawPileButton.isOn = false;
        }
        DiscardPileContainer.SetActive(value);
    }
    public void ToggleConstructsPile(bool value) {
        Debug.Log($"Show Constructs Pile: {value}");
        
        /*
        for (int i = 0; i < 6; i++) {
            GameObject card = Instantiate(CenterDeck[0], CenterRowContainer.transform);
            float resizeFactor = CenterRowContainer.GetComponent<BoxCollider2D>().size.y / card.GetComponent<BoxCollider2D>().size.y;
            card.transform.localScale = new Vector3(resizeFactor, resizeFactor, 1);
            card.SetActive(true);
        }
        for (int i = 0; i < 3; i++) {
            GameObject card = Instantiate(CenterDeck[0], StaticCardsContainer.transform);
            float resizeFactor = StaticCardsContainer.GetComponent<BoxCollider2D>().size.y / card.GetComponent<BoxCollider2D>().size.y;
            card.transform.localScale = new Vector3(resizeFactor, resizeFactor, 1);
            card.SetActive(true);
        }
        */
    }

    public void EndTurn() {
        // Update logical containers
        players[playerIdx].PlayArea.AddRange(PlayArea.GetComponent<PlayerContainer>().cards);
        PlayArea.GetComponent<PlayerContainer>().Clear();
        players[playerIdx].EndTurn();

        // Move to the next player
        playerIdx = (playerIdx + 1) % players.Length;

        // Update containers
        DrawPileContainer.GetComponent<PlayerContainer>().UpdateSelf(players[playerIdx].DrawPile);
        Hand.GetComponent<PlayerContainer>().UpdateSelf(players[playerIdx].Hand);
        DiscardPileContainer.GetComponent<PlayerContainer>().UpdateSelf(players[playerIdx].DiscardPile);

        /*
        for (int i = 0; i < 3; i++) {
            GameObject card = Instantiate(CenterDeck[0], PlayArea.transform);
            float resizeFactor = PlayArea.GetComponent<BoxCollider2D>().size.y / card.GetComponent<BoxCollider2D>().size.y * 0.8f;
            card.transform.localScale = new Vector3(resizeFactor, resizeFactor, 1);
            card.SetActive(true);
            card = Instantiate(CenterDeck[0], DrawPileContainer.transform);
            resizeFactor = DrawPileContainer.GetComponent<BoxCollider2D>().size.y / card.GetComponent<BoxCollider2D>().size.y * 0.8f;
            card.transform.localScale = new Vector3(resizeFactor, resizeFactor, 1);
            card.SetActive(true);
            card = Instantiate(CenterDeck[0], DiscardPileContainer.transform);
            resizeFactor = DiscardPileContainer.GetComponent<BoxCollider2D>().size.y / card.GetComponent<BoxCollider2D>().size.y * 0.8f;
            card.transform.localScale = new Vector3(resizeFactor, resizeFactor, 1);
            card.SetActive(true);
            card = Instantiate(CenterDeck[0], VoidPileContainer.transform);
            resizeFactor = VoidPileContainer.GetComponent<BoxCollider2D>().size.y / card.GetComponent<BoxCollider2D>().size.y * 0.8f;
            card.transform.localScale = new Vector3(resizeFactor, resizeFactor, 1);
            card.SetActive(true);
        }
        */
        /*
        Destroy(CenterRow[2]);
        int idx = Random.Range(0,CenterDeck.Count);
        CenterRow[2] = CenterDeck[idx];
        CenterDeck.RemoveAt(idx);
        UpdateCenterRowContainer();
        */
    }
}
