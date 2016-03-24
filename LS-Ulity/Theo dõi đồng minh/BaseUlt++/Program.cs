using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace BaseUltPlusPlus
{
    public class Program
    {
        public static Menu BaseUltMenu { get; set; }

        public static void Main(string[] args)
        {
            // Wait till the name has fully loaded
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Menu
            BaseUltMenu = MainMenu.AddMenu("Theo Dõi Tướng", "BUP");
            BaseUltMenu.AddGroupLabel("Theo dõi tướng biến về");
            BaseUltMenu.AddSeparator();
            BaseUltMenu.Add("baseult", new CheckBox("Theo dõi tướng đang biến về"));
            BaseUltMenu.Add("showrecalls", new CheckBox("Hiện nhớ lại"));
            BaseUltMenu.Add("showallies", new CheckBox("Hiện Đồng Minh"));
            BaseUltMenu.Add("showenemies", new CheckBox("Hiện kẻ thù"));
            BaseUltMenu.Add("checkcollision", new CheckBox("Kiểm tra va chạm"));
            BaseUltMenu.AddSeparator();
            BaseUltMenu.Add("timeLimit", new Slider("Thời gian giới hạn", 0, 0, 120));
            BaseUltMenu.AddSeparator();
            BaseUltMenu.Add("nobaseult", new KeyBind("Không có thời gian theo dõi", false, KeyBind.BindTypes.HoldActive, 32));
            BaseUltMenu.AddSeparator();
            BaseUltMenu.Add("x", new Slider("Offset X", 0, -500, 500));
            BaseUltMenu.Add("y", new Slider("Offset Y", 0, -500, 500));
            BaseUltMenu.AddGroupLabel("BaseUlt++ Targets");
            foreach (var unit in EntityManager.Heroes.Enemies)
            {
                BaseUltMenu.Add("target" + unit.ChampionName,
                    new CheckBox(string.Format("{0} ({1})", unit.ChampionName, unit.Name)));
            }

            BaseUltMenu.AddGroupLabel("BaseUlt++ Credits");
            BaseUltMenu.AddLabel("By: H4ckercfqq2 Edit VietNamese");

            // Initialize the Addon
            OfficialAddon.Initialize();

            // Listen to the two main events for the Addon
            Game.OnUpdate += args1 => OfficialAddon.Game_OnUpdate();
            Drawing.OnEndScene += args1 => OfficialAddon.Drawing_OnEndScene();
            Teleport.OnTeleport += OfficialAddon.Teleport_OnTeleport;
        }
    }
}