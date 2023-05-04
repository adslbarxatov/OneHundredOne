namespace RD_AAOW
	{
	/// <summary>
	/// Доступные масти
	/// </summary>
	public enum CardSuits
		{
		/// <summary>
		/// Пики
		/// </summary>
		Peaks = 0,

		/// <summary>
		/// Трефы
		/// </summary>
		Clubs = 1,

		/// <summary>
		/// Червы
		/// </summary>
		Hearts = 2,

		/// <summary>
		/// Бубны
		/// </summary>
		Diamonds = 3
		}

	/// <summary>
	/// Класс описывает дополнительные параметры мастей
	/// </summary>
	public static class CardSuitsClass
		{
		/// <summary>
		/// Количество доступных мастей
		/// </summary>
		public const uint Count = 4;

		/// <summary>
		/// Метод возвращает представление масти в текстовом виде
		/// </summary>
		/// <param name="Suit">Масть</param>
		/// <returns>Текстовое представление указанной масти</returns>
		public static string SuitRepresentation (CardSuits Suit)
			{
			switch (Suit)
				{
				case CardSuits.Clubs:
					return "♣";

				case CardSuits.Diamonds:
					return "♦";

				case CardSuits.Hearts:
					return "♥";

				case CardSuits.Peaks:
					return "♠";
				}

			return "";  // Сомнительно, но всё же
			}
		}
	}
