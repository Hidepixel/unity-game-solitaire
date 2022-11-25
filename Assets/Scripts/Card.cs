using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solitaire
{
    public class Card : MonoBehaviour
    {
        public Image Image { get; private set; }

        public Sprite Sprite { get; private set; }
        public string Number;
        public Constants.Suit Suit { get; private set; }


        // Constructor
        public Card(Sprite sprite, string number, Constants.Suit suit)
        {
            Sprite = sprite;
            Number = number;
            Suit = suit;  
        }


        private void Awake()
        {
            Image = GetComponent<Image>();
            
        }
        public void Initialize(Sprite sprite, string number, Constants.Suit suit)
        {
            Image.sprite = sprite;
            Number = number;
            Suit = suit;

            gameObject.name = Number + SuitToString(Suit);
        }


        private string SuitToString(Constants.Suit suit)
        {
            switch (suit)
            {
                case Constants.Suit.SPADES:
                    return "Spades";
                case Constants.Suit.CLUBS:
                    return "Clubs";
                case Constants.Suit.HEARTHS:
                    return "Hearths";
                case Constants.Suit.DIAMONDS:
                    return "Clubs";
                default:
                    return "Error initializing card!";
            }
        }
    }
}