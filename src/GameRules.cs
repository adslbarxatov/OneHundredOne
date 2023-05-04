namespace RD_AAOW
	{
	/// <summary>
	/// Класс обеспечивает доступ к правилам игры
	/// </summary>
	public static class GameRules
		{
		/// <summary>
		/// Метод определяет, требуется ли крыть указанную карту
		/// </summary>
		/// <param name="SomeCard">Карта для проверки</param>
		public static bool CoveringNotNeeded (Card SomeCard)
			{
			if ((SomeCard.CardValue == CardValues.Ace) ||
				(SomeCard.CardValue == CardValues.Eight) ||
				(SomeCard.CardValue == CardValues.Seven) ||
				(SomeCard.CardValue == CardValues.Six) ||
				(SomeCard.CardValue == CardValues.King) && (SomeCard.CardSuit == CardSuits.Peaks))
				{
				return false;
				}

			return true;
			}

		/// <summary>
		/// Метод определяет, может ли карта CoverCard покрыть карту OldCard
		/// </summary>
		/// <param name="OldCard">Карта для покрытия</param>
		/// <param name="CoverCard">Потенциально кроющая карта</param>
		public static bool CanCover (Card OldCard, Card CoverCard)
			{
			if ((OldCard == null) ||
				(OldCard.CardSuit == CoverCard.CardSuit) ||
				(OldCard.CardValue == CoverCard.CardValue) ||
				(CoverCard.CardValue == CardValues.Queen) ||
				(OldCard.CardValue == CardValues.Request) && (OldCard.CardSuit == CoverCard.CardSuit))
				{
				return true;
				}

			return false;
			}

		/// <summary>
		/// Метод определяет, сколько карт нужно взять игроку в ответ на указанную карту
		/// </summary>
		/// <param name="SomeCard">Карта для проверки</param>
		/// <returns>Количество карт</returns>
		public static uint CardsToTake (Card SomeCard)
			{
			if (SomeCard.CardValue == CardValues.Six)
				return 2;

			if (SomeCard.CardValue == CardValues.Seven)
				return 1;

			if ((SomeCard.CardValue == CardValues.King) && (SomeCard.CardSuit == CardSuits.Peaks))
				return 5;

			return 0;
			}

		/// <summary>
		/// Метод определяет, сколько штрафных очков получает игрок при проигрыше
		/// </summary>
		/// <param name="Hand">Рука игрока</param>
		/// <returns>Число штрафных очков</returns>
		public static uint HandCost (CardsHand Hand)
			{
			uint points = 0;

			CardsHand hand = new CardsHand (Hand);
			for (uint i = 0; i < Hand.HandSize; i++)
				{
				Card card = hand.MakeMove (0);

				switch (card.CardValue)
					{
					case CardValues.Ace:
						points += 11;
						break;

					case CardValues.Eight:
						points += 8;
						break;

					case CardValues.Jack:
						points += 2;
						break;

					case CardValues.King:
						points += 4;
						break;

					case CardValues.Request:    // Вообще не должно быть. Но, на всякий случай
					case CardValues.Nine:
						break;

					case CardValues.Queen:
						points += 3;
						break;

					case CardValues.Seven:
						points += 7;
						break;

					case CardValues.Six:
						points += 6;
						break;

					case CardValues.Ten:
						points += 10;
						break;
					}
				}

			return points;
			}

		/// <summary>
		/// Метод определяет число призовых (вычитаемых) очков при выигрыше игрока
		/// </summary>
		/// <param name="SomeCard">Карта для проверки</param>
		/// <returns>Число призовых очков</returns>
		public static uint CardBonus (Card SomeCard)
			{
			if (SomeCard.CardValue == CardValues.Request)   // Вместо дам
				{
				if (SomeCard.CardSuit == CardSuits.Peaks)
					return 40;

				return 20;
				}

			return 0;
			}

		/// <summary>
		/// Метод определяет рейтинг карты, указывающий, насколько предпочтительно было бы
		/// её оставить, по возможности, не используя сразу
		/// </summary>
		/// <param name="SomeCard">Карта для проверки</param>
		/// <returns>Рейтинг карты</returns>
		public static uint CardRating (Card SomeCard)
			{
			// Дам предпочтительно оставлять до конца
			if (SomeCard.CardValue == CardValues.Queen)
				{
				if (SomeCard.CardSuit == CardSuits.Peaks)
					return 4;

				return 3;
				}

			// Девятки тоже
			if (SomeCard.CardValue == CardValues.Nine)
				return 2;

			// Так же имеет смысл оставлять более мелкие карты
			if ((SomeCard.CardValue == CardValues.Jack) ||
				(SomeCard.CardValue == CardValues.King))
				{
				return 1;
				}

			// Остальное - как получится
			return 0;
			}

		/// <summary>
		/// Возвращает верхнюю границу шкалы рейтинга
		/// </summary>
		public const uint RatingLimit = 5;
		}
	}
