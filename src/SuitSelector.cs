using System;
using System.Drawing;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Форма, позволяющая заказывать масть
	/// </summary>
	public partial class SuitSelector: Form
		{
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="InterfaceColors">Четыре цвета интерфейса:
		/// фон, кнопка, чёрные масти, красные масти</param>
		public SuitSelector (Color[] InterfaceColors)
			{
			// Инициализация
			InitializeComponent ();

			// Установка заголовка формы
			this.Text = Localization.GetText ("OrderSuit");
			this.BackColor = InterfaceColors[0];
			Peaks.BackColor = Clubs.BackColor = Hearts.BackColor =
				Diamonds.BackColor = InterfaceColors[1];
			Peaks.ForeColor = Clubs.ForeColor = InterfaceColors[2];
			Hearts.ForeColor = Diamonds.ForeColor = InterfaceColors[3];

			// Запуск
			this.ShowDialog ();
			}

		/// <summary>
		/// Возвращает выбранную масть
		/// </summary>
		public CardSuits SelectedSuit
			{
			get
				{
				return selectedSuit;
				}
			}
		private CardSuits selectedSuit;

		// Масти
		private void Peaks_Click (object sender, EventArgs e)
			{
			selectedSuit = CardSuits.Peaks;
			this.Close ();
			}

		private void Clubs_Click (object sender, EventArgs e)
			{
			selectedSuit = CardSuits.Clubs;
			this.Close ();
			}

		private void Hearts_Click (object sender, EventArgs e)
			{
			selectedSuit = CardSuits.Hearts;
			this.Close ();
			}

		private void Diamonds_Click (object sender, EventArgs e)
			{
			selectedSuit = CardSuits.Diamonds;
			this.Close ();
			}
		}
	}
