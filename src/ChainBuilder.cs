using System;
using System.Collections.Generic;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс отвечает за построение стратегических цепочек
	/// </summary>
	public class ChainBuilder
		{
		// Набор цепочек
		private List<CardsChain> chains;

		/// <summary>
		/// Конструктор. Выполняет построение цепочек на основе руки игрока
		/// </summary>
		/// <param name="PlayersHand">Рука игрока</param>
		public ChainBuilder (CardsHand PlayersHand)
			{
			// Инициализация
			chains = new List<CardsChain> ();

			if ((PlayersHand == null) || (PlayersHand.HandSize == 0))
				return;

			// Запуск рекурсии
			CardsHand upHand = new CardsHand (PlayersHand);
			CardsChain genericChain = new CardsChain ();
			CreateGenericChain (upHand, null, genericChain, 0); // Получена цепочка в виде дерева

			// Сборка цепочек
			AssembleChains (genericChain, chains);
			}

		/// <summary>
		/// Рекурсивная функция сборки дерева цепочек
		/// </summary>
		/// <param name="ChainLevel">Текущий уровень рекурсии. Он же - уровень дерева цепочек</param>
		/// <param name="GenericChain">Собираемая цепочка-дерево</param>
		/// <param name="LastCard">Выбранная карта из набора, с которой на данном шаге рекурсии
		/// выполняется поиск вариантов покрытия</param>
		/// <param name="UpHand">Текущий вариант набора</param>
		private void CreateGenericChain (CardsHand UpHand, Card LastCard, CardsChain GenericChain, uint ChainLevel)
			{
			// Проверка на завершённость цепи
			if (LastCard != null)
				{
				if (GameRules.CoveringNotNeeded (LastCard) || (UpHand.HandSize == 0))
					{
					GenericChain.AddCard (LastCard);
					return;
					}
				}

			// Поиск допустимого покрытия для карты
			bool cardAdded = false;
			for (uint i = 0; i < UpHand.HandSize; i++)
				{
				// Берётся первая доступная карта
				CardsHand newHand = new CardsHand (UpHand);
				Card newCard = newHand.MakeMove (i);    // Карта изымается из набора
				Card chainedCard = new Card (newCard.CardSuit, newCard.CardValue, ChainLevel);

				// Проверяется возможность покрыть ею имеющуюся
				if (GameRules.CanCover (LastCard, chainedCard))
					{
					// Если есть, карта добавляется, и запускается поиск следующей покрывающей карты
					if (LastCard != null)
						{
						GenericChain.AddCard (LastCard);
						cardAdded = true;
						}
					CreateGenericChain (newHand, chainedCard, GenericChain, ChainLevel + 1);
					}
				}

			// Если покрытия не было
			if ((LastCard != null) && !cardAdded)
				GenericChain.AddCard (LastCard);
			}

		/// <summary>
		/// Функция разборки дерева цепочек на отдельные цепочки стратегий
		/// </summary>
		/// <param name="GenericChain">Анализируемая цепочка-дерево</param>
		/// <param name="ChainsArray">Получаемый массив цепочек стартегий</param>
		private void AssembleChains (CardsChain GenericChain, List<CardsChain> ChainsArray)
			{
			// Переменные
			bool completed = false;

			// Извлечение цепочек до тех пор, пока не найдутся все
			while (!completed)
				{
				// Подготовка
				completed = true;

				// Сборка одной цепочки
				ChainsArray.Add (new CardsChain ());

				for (uint i = GenericChain.ChainLength; i > 0; i--)
					{
					Card card = GenericChain.GetCard (i - 1);

					// Если карта не помечена как использованная в других цепочках,
					if (!card.Answered)
						{
						// Подготовка
						int currentLevel = (int)card.ChainLevel;
						completed = false;

						// начинается сборка отдельной цепочки
						for (uint j = i; j > 0; j--)
							{
							card = GenericChain.GetCard (j - 1);

							// Если карта имеет нужный уровень, она добавляется в цепочку, и выполняется
							// переход к следующему уровню
							if (card.ChainLevel == (uint)currentLevel)
								{
								ChainsArray[ChainsArray.Count - 1].AddCard (card);
								GenericChain.AnswerCard (j - 1);
								currentLevel--;
								}
							}

						// Так как второй цикл является продолжением первого, прервать нужно и первый
						break;
						}
					}
				}

			// Удаление пустых цепочек
			int count = ChainsArray.Count;
			for (int i = 0; i < ChainsArray.Count; i++)
				{
				if (ChainsArray[i].ChainLength == 0)
					{
					ChainsArray.RemoveAt (i);
					i--;
					count--;
					}
				}
			}

		/// <summary>
		/// Метод получает случайную из наилучших цепочек стратегий
		/// </summary>
		/// <param name="LastCard">Карта, которую нужно покрыть</param>
		/// <returns>Полученная цепочка</returns>
		public CardsChain GetRandomBestChain (Card LastCard)
			{
			if (chains.Count == 0)
				return null;

			// Определение максимальной длины цепочки и количества таких цепочек
			uint maxLength = 0, maxLengthCount = 0;
			for (int i = 0; i < chains.Count; i++)
				{
				// При условии совпадения по первой карте
				Card card = chains[i].GetCard (chains[i].ChainLength - 1);
				if (GameRules.CanCover (LastCard, card))
					{
					if (chains[i].ChainLength > maxLength)
						{
						maxLength = chains[i].ChainLength;
						maxLengthCount = 0;
						}

					if (chains[i].ChainLength == maxLength)
						{
						maxLengthCount++;
						}
					}
				}

			// Метод не должен возвращать неподходящие цепочки
			if (maxLengthCount == 0)
				return null;

			// Выбор цепочки
			uint minRating = GameRules.RatingLimit;
			int minRatingPos = 0;

			for (int i = 0; i < chains.Count; i++)
				{
				Card card = chains[i].GetCard (chains[i].ChainLength - 1);
				if ((chains[i].ChainLength == maxLength) && (GameRules.CanCover (LastCard, card)))
					{
					card = chains[i].GetCard (0);   // Определение рейтинга цепочки по последней карте

					if (GameRules.CardRating (card) < minRating)
						{
						minRating = GameRules.CardRating (card);
						minRatingPos = i;
						}
					}
				}

			// Возврат цепочки с минимальным рейтингом (не нуждающейся в задержке)
			return chains[minRatingPos];
			}
		}
	}
