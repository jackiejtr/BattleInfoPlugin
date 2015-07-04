﻿using System;
using BattleInfoPlugin.Models;
using BattleInfoPlugin.Models.Notifiers;
using Livet;
using Livet.EventListeners;
using Livet.Messaging;

namespace BattleInfoPlugin.ViewModels
{
	public class ToolViewModel : ViewModel
	{
		private readonly BattleEndNotifier notifier = new BattleEndNotifier();

		private readonly BattleData battleData = new BattleData();

		public string UpdatedTime
		{
			get
			{
				return this.battleData != null && this.battleData.UpdatedTime != default(DateTimeOffset)
					? this.battleData.UpdatedTime.ToString("yyyy/MM/dd HH:mm:ss")
					: "No Data";
			}
		}

		public string BattleSituation
		{
			get
			{
				return this.battleData != null && this.battleData.BattleSituation != Models.BattleSituation.없음
					? this.battleData.BattleSituation.ToString()
					: "";
			}
		}

		public string FriendAirSupremacy
		{
			get
			{
				return this.battleData != null && this.battleData.FriendAirSupremacy != AirSupremacy.항공전없음
					? this.battleData.FriendAirSupremacy.ToString()
					: "";
			}
		}

		public string Cell
		{
			get
			{
				return this.battleData.Cell.ToString();
			}
		}

		public string RankResult
		{
			get
			{
				return this.battleData.RankResult.ToString();
			}
		}

		#region FirstFleet変更通知プロパティ
		private FleetViewModel _FirstFleet;

		public FleetViewModel FirstFleet
		{
			get
			{ return this._FirstFleet; }
			set
			{
				if (this._FirstFleet == value)
					return;
				this._FirstFleet = value;
				this.RaisePropertyChanged();
			}
		}
		#endregion


		#region SecondFleet変更通知プロパティ
		private FleetViewModel _SecondFleet;

		public FleetViewModel SecondFleet
		{
			get
			{ return this._SecondFleet; }
			set
			{
				if (this._SecondFleet == value)
					return;
				this._SecondFleet = value;
				this.RaisePropertyChanged();
			}
		}
		#endregion


		#region Enemies変更通知プロパティ
		private FleetViewModel _Enemies;

		public FleetViewModel Enemies
		{
			get
			{ return this._Enemies; }
			set
			{
				if (this._Enemies == value)
					return;
				this._Enemies = value;
				this.RaisePropertyChanged();
			}
		}
		#endregion


		#region IsNotifierEnabled変更通知プロパティ
		// ここ以外で変更しないのでModel変更通知は受け取らない雑対応
		public bool IsNotifierEnabled
		{
			get
			{ return this.notifier.IsEnabled; }
			set
			{
				if (this.notifier.IsEnabled == value)
					return;
				this.notifier.IsEnabled = value;
				this.RaisePropertyChanged();
			}
		}
		#endregion


		public ToolViewModel()
		{
			this.FirstFleet = new FleetViewModel("아군함대");
			this.SecondFleet = new FleetViewModel("호위함대");
			this.Enemies = new FleetViewModel("적함대");

			this.CompositeDisposable.Add(new PropertyChangedEventListener(this.battleData)
            {
				{
                    () => this.battleData.Cell,
                    (_, __) => this.RaisePropertyChanged(() => this.Cell)
                },
                {
                    () => this.battleData.UpdatedTime,
                    (_, __) => this.RaisePropertyChanged(() => this.UpdatedTime)
                },
                {
                    () => this.battleData.BattleSituation,
                    (_, __) => this.RaisePropertyChanged(() => this.BattleSituation)
                },
                {
                    () => this.battleData.FriendAirSupremacy,
                    (_, __) => this.RaisePropertyChanged(() => this.FriendAirSupremacy)
                },
                {
                    () => this.battleData.FirstFleet,
                    (_, __) => this.FirstFleet.Fleet = this.battleData.FirstFleet
                },
                {
                    () => this.battleData.SecondFleet,
                    (_, __) => this.SecondFleet.Fleet = this.battleData.SecondFleet
                },
                {
                    () => this.battleData.Enemies,
                    (_, __) => this.Enemies.Fleet = this.battleData.Enemies
                },
				{
                    () => this.battleData.RankResult,
                    (_, __) => this.RaisePropertyChanged(() => this.RankResult)
                },
            });
		}

		public void ShowPreviewPopUp()
		{
			var message = new TransitionMessage("Show/ToolPopup")
			{
				TransitionViewModel = new ToolPopupViewModel()
			};
			this.Messenger.RaiseAsync(message);
		}

		public void OpenEnemyWindow()
		{
			var message = new TransitionMessage("Show/EnemyWindow")
			{
				TransitionViewModel = new EnemyWindowViewModel()
			};
			this.Messenger.RaiseAsync(message);
		}
	}
}
