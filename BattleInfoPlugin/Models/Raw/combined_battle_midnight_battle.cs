﻿namespace BattleInfoPlugin.Models.Raw
{
	/// <summary>
	/// 연합함대 - 야전 / 연합함대 - 개막야전
	/// combined_battle_midnight_battle, combined_battle_sp_midnight
	/// </summary>
	public class combined_battle_midnight_battle : ICommonFirstBattleMembers
	{
		public int api_deck_id { get; set; }
		public int[] api_ship_ke { get; set; }
		public int[] api_ship_lv { get; set; }
		public int[] api_nowhps { get; set; }
		public int[] api_maxhps { get; set; }
		public int[] api_nowhps_combined { get; set; }
		public int[] api_maxhps_combined { get; set; }
		public int[][] api_eSlot { get; set; }
		public int[][] api_eKyouka { get; set; }
		public int[][] api_fParam { get; set; }
		public int[][] api_eParam { get; set; }
		public int[][] api_fParam_combined { get; set; }
		public int[] api_formation { get; set; }
		public int[] api_touch_plane { get; set; }
		public int[] api_flare_pos { get; set; }
		public Midnight_Hougeki api_hougeki { get; set; }
	}

}
