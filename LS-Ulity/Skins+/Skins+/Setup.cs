namespace SkinsPlus
{
    using System;
    using System.Collections.Generic;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class Setup
    {
        #region Static Fields

        public static readonly Dictionary<int, bool> ChampEnabled = new Dictionary<int, bool>();

        public static readonly Dictionary<string, int> ChampSkins = new Dictionary<string, int>();

        public static readonly List<AIHeroClient> Heroes = new List<AIHeroClient>();

        private static Menu menu, heroSubMenu;

        private static Slider skinSelect;

        #endregion

        #region Public Methods and Operators

        public static void OnGameLoad(EventArgs gameArgs)
        {
            menu = MainMenu.AddMenu("Faker-Skin", "Faker-Skin");
            menu.AddGroupLabel("Faker Skin by H4ckercfqq2");
            menu.AddSeparator();
            menu.AddLabel("Sử dụng menu để thay đổi Skin");
            try
            {
                foreach (var hero in EntityManager.Heroes.AllHeroes)
                {
                    Heroes.Add(hero);

                    Re.Dead.Add(hero, false);

                    ChampEnabled.Add(hero.NetworkId, false);

                    var currenthero = hero;

                    heroSubMenu = menu.AddSubMenu(hero.ChampionName + " (" + hero.Name + ") ", hero.ChampionName);

                    skinSelect = heroSubMenu.Add("skin." + hero.ChampionName, new Slider("Skin ID", 0, 0, 15));

                    ChampSkins.Add(hero.Name, heroSubMenu["skin." + hero.ChampionName].Cast<Slider>().CurrentValue);

                    ChampEnabled[hero.NetworkId] = true;

                    hero.SetSkin(hero.ChampionName, ChampSkins[hero.Name]);

                    skinSelect.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                        {
                            ChampEnabled[hero.NetworkId] = true;
                            ChampSkins[currenthero.Name] = args.NewValue;
                            currenthero.SetSkin(currenthero.ChampionName, ChampSkins[currenthero.Name]);
                        };
                }
            }
            catch (Exception e)
            {
                Console.Write(e + " " + e.StackTrace);
            }
            Game.OnUpdate += Re.New;
        }

        #endregion
    }
}