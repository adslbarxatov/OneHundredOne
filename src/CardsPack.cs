using System;
using System.Collections.Generic;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает карточную колоду
	/// </summary>
	public class CardsPack
		{
		// Набор карт колоды
		private List<Card> cards;
		private List<Card> handCards;
		private List<Card> removedCards;

		// ГПСЧ
		private Random rnd;

		///	<summary>
		/// Конструктор. Создаёт колоду, заполняя её всеми доступными картами
		/// </summary>
		public CardsPack ()
			{
			// Инициализация
			cards = new List<Card> ();
			handCards = new List<Card> ();
			removedCards = new List<Card> ();
			rnd = new Random ();

			// Заполнение
			for (uint s = 0; s < CardSuitsClass.Count; s++)
				for (uint v = 0; v < CardValuesClass.Count; v++)
					cards.Add (new Card ((CardSuits)s, (CardValues)v));
			}

		///	<summary>
		/// Метод получает случайную карту из колоды. Карта при этом изымается из колоды
		/// до следующего сброса. Сброс выполняется, когда колода заканчивается
		/// </summary>
		/// <returns>Случайная карта</returns>
		public Card GetRandomCard2 ()
			{
			if (cards.Count == 0)
				{
				cards.AddRange (removedCards);
				removedCards.Clear ();
				}

			int n = rnd.Next (0, cards.Count);
			Card card = new Card (cards[n].CardSuit, cards[n].CardValue);

			cards.Remove (card);
			handCards.Add (card);
			return card;
			}

		///	<summary>
		/// Метод сбрасывает карту в отбой, позволяя вернуть её в игру
		/// </summary>
		/// <param name="ThrownCard">Новая карта</param>
		public void ThrowCard (Card ThrownCard)
			{
			if (ThrownCard == null)
				throw new Exception ("Invalid method call, point 11");

			if (handCards.Contains (ThrownCard))
				{
				removedCards.Add (ThrownCard);
				handCards.Remove (ThrownCard);
				}
			else
				{
				throw new Exception ("Invalid cards processing chain, point 12");
				}
			}

		///	<summary>
		/// Возвращает количество карт в колоде
		/// </summary>
		public uint PackSize
			{
			get
				{
				return (uint)cards.Count;
				}
			}

		///	<summary>
		/// Возвращает количество карт на руках
		/// </summary>
		public uint HandsSize
			{
			get
				{
				return (uint)handCards.Count;
				}
			}

		///	<summary>
		/// Возвращает количество карт в отбое
		/// </summary>
		public uint RemovedSize
			{
			get
				{
				return (uint)removedCards.Count;
				}
			}
		}
	}
