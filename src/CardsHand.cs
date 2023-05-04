using System;
using System.Collections.Generic;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает руку игрока
	/// </summary>
	public class CardsHand
		{
		// Набор карт игрока
		private List<Card> hand;

		/// <summary>
		/// Конструктор. Создаёт руку игрока c правом первого хода
		/// </summary>
		/// <param name="Card1">Первая карта</param>
		/// <param name="Card2">Вторая карта</param>
		/// <param name="Card3">Третья карта</param>
		public CardsHand (Card Card1, Card Card2, Card Card3)
			{
			// Предохранитель
			if ((Card1 == null) || (Card2 == null) || (Card3 == null))
				throw new Exception ("Critical: undefined card in player's hand, point 1");

			// Инициализация
			hand = new List<Card>
				{
				new Card (Card1.CardSuit, Card1.CardValue),
				new Card (Card2.CardSuit, Card2.CardValue),
				new Card (Card3.CardSuit, Card3.CardValue)
				};
			}

		/// <summary>
		/// Конструктор. Создаёт руку без права первого хода
		/// </summary>
		/// <param name="Card1">Первая карта</param>
		/// <param name="Card2">Вторая карта</param>
		/// <param name="Card3">Третья карта</param>
		/// <param name="Card4">Четвёртая карта</param>
		public CardsHand (Card Card1, Card Card2, Card Card3, Card Card4)
			{
			// Предохранитель
			if ((Card1 == null) || (Card2 == null) || (Card3 == null) || (Card4 == null))
				throw new Exception ("Critical: undefined card in player's hand, point 2");

			// Инициализация
			hand = new List<Card>
				{
				new Card (Card1.CardSuit, Card1.CardValue),
				new Card (Card2.CardSuit, Card2.CardValue),
				new Card (Card3.CardSuit, Card3.CardValue),
				new Card (Card4.CardSuit, Card4.CardValue)
				};
			}

		/// <summary>
		/// Конструктор. Дублирует ранее созданную руку
		/// </summary>
		/// <param name="OldHand">Ранее созданная рука</param>
		public CardsHand (CardsHand OldHand)
			{
			// Предохранитель
			if (OldHand == null)
				throw new Exception ("Critical: undefined card in player's hand, point 3");

			// Инициализация
			hand = new List<Card> ();

			for (int i = 0; i < OldHand.hand.Count; i++)
				hand.Add (OldHand.hand[i]);
			}

		/// <summary>
		/// Метод соответствует добору карты из колоды
		/// </summary>
		/// <param name="NewCard">Новая карта</param>
		public void AddCard (Card NewCard)
			{
			if (NewCard == null)
				return;

			hand.Add (new Card (NewCard.CardSuit, NewCard.CardValue));
			}

		/// <summary>
		/// Возвращает количество карт в руке игрока
		/// </summary>
		public uint HandSize
			{
			get
				{
				return (uint)hand.Count;
				}
			}

		/// <summary>
		/// Метод соответствует ходу игрока. При этом карта из руки удаляется
		/// </summary>
		/// <param name="CardNumber">Номер карты в руке</param>
		/// <returns>Выбранная карта</returns>
		public Card MakeMove (uint CardNumber)
			{
			if (CardNumber >= (uint)hand.Count)
				return null;

			Card card = new Card (hand[(int)CardNumber].CardSuit, hand[(int)CardNumber].CardValue);
			hand.RemoveAt ((int)CardNumber);
			return card;
			}

		/// <summary>
		/// Метод соответствует ходу игрока. Выполняет только удаление карты
		/// </summary>
		/// <param name="UsedCard">Выбранная карта</param>
		/// <returns>Выбранная карта без признаков покрытости и уровня в цепочке</returns>
		public Card MakeMove (Card UsedCard)
			{
			if (UsedCard == null)
				return null;

			hand.Remove (UsedCard);
			return new Card (UsedCard.CardSuit, UsedCard.CardValue);
			}

		/// <summary>
		/// Метод сортирует карты в руке по порядку мастей и номиналов
		/// </summary>
		public void Sort ()
			{
			// Добавление отсортированной последовательности в руку
			for (uint v = 0; v < CardValuesClass.Count; v++)
				{
				for (uint s = 0; s < CardSuitsClass.Count; s++)
					{
					Card card = new Card ((CardSuits)s, (CardValues)v);
					if (hand.Contains (card))
						{
						hand.Add (card);
						hand.Remove (card);
						}
					}
				}

			// Завершено
			}
		}
	}
