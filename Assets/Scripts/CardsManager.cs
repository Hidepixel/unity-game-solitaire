using Core.Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
    
    public class CardsManager : MonoBehaviour
    {
        public GameObject cardPrefab;
        public Sprite[] Sprites;

        public Transform deck;

        public List<Card> cards;

        
        private void Start()
        {
            CreateCards();
        }


        public void CreateCards()
        {
            cards = new List<Card>();
            for (int s = 0; s < 4; s++) // suits (naipes)
            {
                for (int n = 0; n < Constants.Numbers.Length; n++) // numbers/letters
                {
                    int spriteIndex = (13 * s) + n;
                    Sprite sprite = Sprites[spriteIndex];
                    string number = Constants.Numbers[n];
                    Constants.Suit suit = (Constants.Suit)s;

                    // Create instance of cards with its data
                    Card card = new Card(sprite, number, suit);
                    cards.Add(card);
                }
            }
            Helper.Shuffle(cards);

            // Instantiate cards and assign the components to the list
            for (int i = 0; i < cards.Count; i++)
            {
                Card cardObj = Instantiate(cardPrefab, deck).GetComponent<Card>();
                cardObj.Initialize(cards[i].Sprite, cards[i].Number, cards[i].Suit);

                cards[i] = cardObj;
            }            
        }
    }
}