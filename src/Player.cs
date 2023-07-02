using System;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает отдельного игрока
	/// </summary>
	public class Player
		{
		// Переменные
		private CardsHand hand;         // Рука игрока
		private CardsChain strategy;    // Текущая стратегия игрока
		private uint strategyCursor;    // Текущая позиция реализации стратегии
		private int scores;             // Очки игрока
		private ChainBuilder cb;        // Объект-генератор стратегий

		/// <summary>
		/// Конструктор. Создаёт игрока
		/// </summary>
		/// <param name="Pack">Колода карт</param>
		/// <param name="WillStart">Указывает, обладает ли этот игрок правом первого хода</param>
		public Player (CardsPack Pack, bool WillStart)
			{
			// Предохранитель
			if (Pack == null)
				throw new Exception ("Critical: initialization with empty pack, point 5");

			// Инициализация
			scores = 0;
			if (WillStart)
				ReInitializeFirstPlayer (Pack);
			else
				ReInitializeSecondPlayer (Pack);
			}

		/// <summary>
		/// Метод реинициализирует игрока без права первого хода
		/// </summary>
		/// <param name="Pack">Колода карт</param>
		public void ReInitializeSecondPlayer (CardsPack Pack)
			{
			hand = new CardsHand (Pack.GetRandomCard2 (), Pack.GetRandomCard2 (), Pack.GetRandomCard2 (),
				Pack.GetRandomCard2 ());
			strategy = new CardsChain ();
			strategyCursor = 0;
			}

		/// <summary>
		/// Метод реинициализирует игрока с правом первого хода
		/// </summary>
		/// <param name="Pack">Колода карт</param>
		public void ReInitializeFirstPlayer (CardsPack Pack)
			{
			hand = new CardsHand (Pack.GetRandomCard2 (), Pack.GetRandomCard2 (), Pack.GetRandomCard2 ());
			strategy = new CardsChain ();
			strategyCursor = 0;
			}

		/// <summary>
		/// Метод реализует ход игрока. Метод не должен вызываться после определения победителя
		/// </summary>
		/// <param name="LastCard">Последняя выложенная карта</param>
		/// <param name="Pack">Колода карт</param>
		/// <returns>Возвращает новую выкладываемую карту</returns>
		public Card MakeMove (CardsPack Pack, Card LastCard)
			{
			// Если другой игрок реализует стратегию
			if (!LastCard.Answered)
				{
				// Добор карт, если требуется
				for (int i = 0; i < GameRules.CardsToTake (LastCard); i++)
					hand.AddCard (Pack.GetRandomCard2 ());

				// Пропуск хода, если требуется
				if (!GameRules.CoveringNotNeeded (LastCard))
					{
					LastCard.AnswerCard ();
					return LastCard;
					}
				}

			// Определение дальнейших действий
			if (strategyCursor == 0)
				{
				cb = new ChainBuilder (hand);
				strategy = cb.GetRandomBestChain (LastCard);

				// Если без новой карты из колоды никак, нужно её взять
				if (strategy == null)
					{
					// Требуется перекрыть восьмёрку обязательно
					if (LastCard.CardValue == CardValues.Eight)
						{
						do
							{
							hand.AddCard (Pack.GetRandomCard2 ());
							cb = new ChainBuilder (hand);
							strategy = cb.GetRandomBestChain (LastCard);
							} while (strategy == null);
						}

					// В остальных случаях - по возможности
					else
						{
						hand.AddCard (Pack.GetRandomCard2 ());
						cb = new ChainBuilder (hand);
						strategy = cb.GetRandomBestChain (LastCard);

						if (strategy == null)
							{
							LastCard.AnswerCard ();
							return LastCard;
							}
						}
					}

				strategyCursor = strategy.ChainLength;
				}

			// Реализация стратегии (ход)
			if (LastCard.CardValue != CardValues.Request)
				Pack.ThrowCard (LastCard);

			Card card = strategy.GetCard (strategyCursor - 1);
			card = hand.MakeMove (card);
			strategyCursor--;

			// Реализация дам
			if (card.CardValue == CardValues.Queen) // Возможно окончание дамой
				{
				if (hand.HandSize != 0)
					{
					// Получение любой оптимальной стратегии
					cb = new ChainBuilder (hand);
					strategy = cb.GetRandomBestChain (null);

					// Замена дамы на request нужной масти
					Pack.ThrowCard (card);
					card = strategy.GetCard (strategy.ChainLength - 1);
					}

				return new Card (card.CardSuit, CardValues.Request);    // Если рука кончилась, вернёт масть дамы
				}

			return card;
			}

		/// <summary>
		/// Метод реализует управляемый ход игрока. Метод не должен вызываться после определения победителя
		/// Метод не проверяет выполняемые действия
		/// </summary>
		/// <param name="LastCard">Последняя выложенная карта</param>
		/// <param name="Pack">Колода карт</param>
		/// <param name="NewCard">Карта, определяющая ход игрока. Если указано значение null,
		/// выполняется добор карт</param>
		/// <param name="RequestedSuit">Запрашиваемая масть (необходима, если выкладывается дама)</param>
		/// <returns>Возвращает новую выкладываемую карту</returns>
		public Card MakeManualMove (CardsPack Pack, Card LastCard, Card NewCard, CardSuits RequestedSuit)
			{
			// Если другой игрок реализует стратегию
			if (!LastCard.Answered)
				{
				// Добор карт, если требуется
				for (int i = 0; i < GameRules.CardsToTake (LastCard); i++)
					hand.AddCard (Pack.GetRandomCard2 ());

				// Пропуск хода, если требуется
				if (!GameRules.CoveringNotNeeded (LastCard))
					{
					LastCard.AnswerCard ();
					return LastCard;
					}
				}

			// Определение дальнейших действий
			Card card;

			// Если крыть нечем
			if (NewCard == null)
				{
				// Нужно найти ближайшую карту, которая сможет покрыть восьмёрку
				if (LastCard.CardValue == CardValues.Eight)
					{
					do
						{
						card = Pack.GetRandomCard2 ();
						hand.AddCard (card);
						} while (!GameRules.CanCover (LastCard, card));

					return LastCard;
					}

				// Иначе выполняется простой добор карты
				hand.AddCard (Pack.GetRandomCard2 ());
				LastCard.AnswerCard ();
				return LastCard;
				}

			// Если вариант есть
			if (LastCard.CardValue != CardValues.Request)
				Pack.ThrowCard (LastCard);

			card = hand.MakeMove (NewCard);

			// Обработка дамы
			if (NewCard.CardValue == CardValues.Queen)
				card = new Card (RequestedSuit, CardValues.Request);

			return card;
			}

		/// <summary>
		/// Возвращает очки, полученные игроком
		/// </summary>
		public int Scores
			{
			get
				{
				return scores;
				}
			}

		/// <summary>
		/// Обновляет очки в случае завершения игры. Должен вызываться перед методами ReInitializePlayer
		/// </summary>
		/// <param name="LastCard">Последняя выложенная карта</param>
		public void UpdateScores (Card LastCard)
			{
			// Победа
			if (hand.HandSize == 0)
				scores -= (int)GameRules.CardBonus (LastCard);

			// Поражение
			else
				scores += (int)GameRules.HandCost (hand);
			}

		/// <summary>
		/// Сбрасывает очки в случае получения 101 очка
		/// </summary>
		public void ZeroScores ()
			{
			scores = 0;
			}

		/// <summary>
		/// Возвращает текущую руку игрока
		/// </summary>
		public CardsHand Hand
			{
			get
				{
				return hand;
				}
			}
		}
	}
