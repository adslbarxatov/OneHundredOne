using System;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает отдельную игральную карту (из колоды в 36 карт)
	/// </summary>
	public class Card: IEquatable<Card>
		{
		/// <summary>
		/// Масть карты
		/// </summary>
		public CardSuits CardSuit
			{
			get
				{
				return cardSuit;
				}
			}
		private CardSuits cardSuit;

		/// <summary>
		/// Номинал карты
		/// </summary>
		public CardValues CardValue
			{
			get
				{
				return cardValue;
				}
			}
		private CardValues cardValue;

		/// <summary>
		/// Конструктор. Создаёт объект-карту
		/// </summary>
		/// <param name="CSuit">Масть карты</param>
		/// <param name="CValue">Номинал карты</param>
		public Card (CardSuits CSuit, CardValues CValue)
			{
			cardSuit = CSuit;
			cardValue = CValue;
			chainLevel = 0;
			answered = false;   // По умолчанию - не покрыта
			}

		/// <summary>
		/// Конструктор. Создаёт объект-карту с возможностью её последующего идентифицирования в цепочке
		/// </summary>
		/// <param name="CSuit">Масть карты</param>
		/// <param name="CValue">Номинал карты</param>
		/// <param name="CLevel">Уровень карты в дереве цепочек стратегий</param>
		public Card (CardSuits CSuit, CardValues CValue, uint CLevel)
			{
			cardSuit = CSuit;
			cardValue = CValue;
			chainLevel = CLevel;
			answered = false;   // По умолчанию - не покрыта
			}

		/// <summary>
		/// Свойство указывает, была ли эта карта покрыта
		/// </summary>
		public bool Answered
			{
			get
				{
				return answered;
				}
			}
		private bool answered;

		/// <summary>
		/// Метод делает карту покрытой
		/// </summary>
		public void AnswerCard ()
			{
			answered = true;
			}

		/// <summary>
		/// Метод отвечает за поддержку сравнения двух объектов класса Card
		/// </summary>
		/// <param name="other">Объект для сравнения</param>
		public bool Equals (Card other)
			{
			if (other == null)
				return false;

			if ((cardSuit == other.cardSuit) && (cardValue == other.cardValue))
				return true;

			return false;
			}

		/// <summary>
		/// Возвращает уровень карты в дереве цепочек стратегий (используется для сборки цепочек)
		/// </summary>
		public uint ChainLevel
			{
			get
				{
				return chainLevel;
				}
			}
		private uint chainLevel;
		}
	}
