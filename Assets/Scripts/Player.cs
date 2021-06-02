using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
* Represents the state of a player in the game
**********************************************/
public class Player : MonoBehaviour
{
    public int honor;
    public int runes;
    public int power;

    public List<GameObject> DrawPile;
    public List<GameObject> Hand;
    public List<GameObject> PlayArea;
    public List<GameObject> DiscardPile;

    // Start is called before the first frame update (when players first enter the game)
    void Awake()
    {
        honor = 0;
        runes = 0;
        power = 0;

        DrawPile = new List<GameObject>();
        Hand = new List<GameObject>();
        PlayArea = new List<GameObject>();
        DiscardPile = new List<GameObject>();

        // Add starting deck into player's deck and shuffle
        for (int i = 0; i < 8; i++) {
            GameObject startingCardPrefab = Resources.Load<GameObject>("Prefabs/Cards/Apprentice Variant");
            GameObject startingCard = Instantiate(startingCardPrefab);
            startingCard.SetActive(false);
            DrawPile.Add(startingCard);
        }
        for (int i = 0; i < 2; i++) {
            GameObject startingCardPrefab = Resources.Load<GameObject>("Prefabs/Cards/Militia Variant");
            GameObject startingCard = Instantiate(startingCardPrefab);
            startingCard.SetActive(false);
            DrawPile.Add(startingCard);
        }
        Container.Shuffle(DrawPile);
        PrepNextTurn();
    }

    // StartTurn is called when it is this player's turn
    public void PrepNextTurn() {
        // Draw 5 cards from deck (shuffle discard if not enough cards)
        for (int i = 0; i < 5; i++) {
            if (DrawPile.Count <= 0) {
                DrawPile.AddRange(DiscardPile);
                DiscardPile.Clear();
                Container.Shuffle(DrawPile);
            }
            GameObject card = DrawPile[0];
            DrawPile.RemoveAt(0);
            Hand.Add(card);
        }
    }

    public void EndTurn() {
        // Move all cards from play area and hand to discard pile
        DiscardPile.AddRange(PlayArea);
        DiscardPile.AddRange(Hand);
        PlayArea.Clear();
        Hand.Clear();

        PrepNextTurn();
    }
}
