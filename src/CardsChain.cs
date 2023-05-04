using System.Collections.Generic;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает цепочку карт, которая может быть принята в качестве стратегии следующего хода
	/// </summary>
	public class CardsChain
		{
		// Цепочка карт
		private List<Card> chain;

		/// <summary>
		/// Конструктор. Создаёт объект-цепочку
		/// </summary>
		public CardsChain ()
			{
			chain = new List<Card> ();
			}

		/// <summary>
		/// Метод добавляет в цепочку с конца одну карту
		/// </summary>
		/// <param name="NewCard">Новая карта</param>
		public void AddCard (Card NewCard)
			{
			if (NewCard == null)
				return;

			chain.Add (new Card (NewCard.CardSuit, NewCard.CardValue, NewCard.ChainLevel));
			if (NewCard.Answered)
				chain[chain.Count - 1].AnswerCard ();
			}

		/// <summary>
		/// Возвращает длину цепочки
		/// </summary>
		public uint ChainLength
			{
			get
				{
				return (uint)chain.Count;
				}
			}

		/// <summary>
		/// Метод получает указанную карту из цепочки
		/// </summary>
		/// <param name="CardNumber">Номер карты в цепочке</param>
		public Card GetCard (uint CardNumber)
			{
			if (CardNumber >= (uint)chain.Count)
				return null;

			Card card = new Card (chain[(int)CardNumber].CardSuit, chain[(int)CardNumber].CardValue,
				chain[(int)CardNumber].ChainLevel);

			if (chain[(int)CardNumber].Answered)
				card.AnswerCard ();

			return card;
			}

		/// <summary>
		/// Метод помечает указанную карту как использованную
		/// </summary>
		/// <param name="CardNumber">Номер карты в цепочке</param>
		public void AnswerCard (uint CardNumber)
			{
			if (CardNumber < (uint)chain.Count)
				chain[(int)CardNumber].AnswerCard ();
			}
		}
	}
