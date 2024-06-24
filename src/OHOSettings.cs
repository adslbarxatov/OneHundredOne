namespace RD_AAOW
	{
	/// <summary>
	/// Класс предоставляет доступ к параметрам работы приложения
	/// </summary>
	public static class OHOSettings
		{
		/// <summary>
		/// Возвращает или задаёт количество выигрышей компьютера
		/// </summary>
		public static uint PCWins
			{
			get
				{
				return RDGenerics.GetSettings (pcWinsPar, 0);
				}
			set
				{
				RDGenerics.SetSettings (pcWinsPar, value);
				}
			}
		private const string pcWinsPar = "PCWins";

		/// <summary>
		/// Возвращает или задаёт количество выигрышей игрока
		/// </summary>
		public static uint PlayerWins
			{
			get
				{
				return RDGenerics.GetSettings (playerWinsPar, 0);
				}
			set
				{
				RDGenerics.SetSettings (playerWinsPar, value);
				}
			}
		private const string playerWinsPar = "PlWins";
		}
	}
