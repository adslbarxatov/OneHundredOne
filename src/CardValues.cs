namespace RD_AAOW
	{
	/// <summary>
	/// Доступные номиналы
	/// </summary>
	public enum CardValues
		{
		/// <summary>
		/// 6
		/// </summary>
		Six = 0,

		/// <summary>
		/// 7
		/// </summary>
		Seven = 1,

		/// <summary>
		/// 8
		/// </summary>
		Eight = 2,

		/// <summary>
		/// 9
		/// </summary>
		Nine = 3,

		/// <summary>
		/// 10
		/// </summary>
		Ten = 4,

		/// <summary>
		/// Валет
		/// </summary>
		Jack = 5,

		/// <summary>
		/// Дама
		/// </summary>
		Queen = 6,

		/// <summary>
		/// Король
		/// </summary>
		King = 7,

		/// <summary>
		/// Туз
		/// </summary>
		Ace = 8,

		/// <summary>
		/// Состояние запроса карты по масти
		/// </summary>
		Request = 100
		}

	/// <summary>
	/// Класс описывает дополнительные параметры номиналов
	/// </summary>
	public static class CardValuesClass
		{
		/// <summary>
		/// Количество доступных номиналов
		/// </summary>
		public const uint Count = 9;

		/// <summary>
		/// Метод возвращает представление номинала в текстовом виде
		/// </summary>
		/// <param name="CValue">Номинал</param>
		/// <returns>Текстовое представление указанного номинала</returns>
		public static string ValueRepresentation (CardValues CValue)
			{
			switch (CValue)
				{
				case CardValues.Ace:
					return Localization.GetText ("CardValue_Ace");

				case CardValues.Eight:
					return "8";

				case CardValues.Jack:
					return Localization.GetText ("CardValue_Jack");

				case CardValues.King:
					return Localization.GetText ("CardValue_King");

				case CardValues.Nine:
					return "9";

				case CardValues.Queen:
					return Localization.GetText ("CardValue_Queen");

				case CardValues.Request:
					return "?";

				case CardValues.Seven:
					return "7";

				case CardValues.Six:
					return "6";

				case CardValues.Ten:
					return "10";
				}

			return "";  // Сомнительно, но всё же
			}
		}
	}
