using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EloBuddy;
using EloBuddy.SDK.Events;
using LSJhin.Champions;
using LSJhin.Managers;
using LSJhin.Utilities;

namespace LSJhin
{
    // ReSharper disable once InconsistentNaming
    public static class AIO
    {
        public static ChampionBase CurrentChampion;

        public static HashSet<Champion> SupportedChampions = new HashSet<Champion>
        {
            Champion.LSJhin,
        };

        public static bool Initialized;

        public static List<Action> Initializers = new List<Action>
        {
            SpellManager.Initialize,
            UnitManager.Initialize,
            DrawingsManager.Initialize,
            MissileManager.Initialize,
            YasuoWallManager.Initialize,
            DamageIndicator.Initialize,
            ItemManager.Initialize,
            FpsBooster.Initialize,
            CacheManager.Initialize,
            LanguageTranslator.Initialize
        };

        public static AIHeroClient MyHero
        {
            get { return Player.Instance; }
        }

        public static void WriteInConsole(string message)
        {
            Console.WriteLine("LSJhin: " + message);
        }

        public static void WriteInChat(string message)
        {
            Chat.Print("LSJhin: " + message);
        }

        private static void Main()
        {
            Loading.OnLoadingComplete += delegate
            {
                LoadChampion(MyHero.Hero);
            };
        }

        public static void LoadChampion(Champion champion)
        {
            try
            {
                if (SupportedChampions.Contains(champion))
                {
                    CurrentChampion =
                        (ChampionBase)
                            Activator.CreateInstance(
                                Assembly.GetExecutingAssembly()
                                    .GetTypes()
                                    .First(type => type.Name.Equals(champion.ToString().Replace(" ", ""))));
                    foreach (var action in Initializers)
                    {
                        action();
                    }
                    Initialized = true;
                }
            }
            catch (Exception e)
            {
                WriteInConsole(e.ToString());
            }
        }
    }
}