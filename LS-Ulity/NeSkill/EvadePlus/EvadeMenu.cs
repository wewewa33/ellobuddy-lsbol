using System.Collections.Generic;
using System.Linq;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace EvadePlus
{
    internal class EvadeMenu
    {
        public static Menu MainMenu { get; private set; }
        public static Menu SkillshotMenu { get; private set; }
        public static Menu SpellMenu { get; private set; }
        public static Menu DrawMenu { get; private set; }
        public static Menu HotkeysMenu { get; private set; }

        public static readonly Dictionary<string, EvadeSkillshot> MenuSkillshots = new Dictionary<string, EvadeSkillshot>();

        public static void CreateMenu()
        {
            if (MainMenu != null)
            {
                return;
            }

            MainMenu = EloBuddy.SDK.Menu.MainMenu.AddMenu("LS+ Né Skill", "Né chiêu né tuyệt chiêu");

            // Set up main menu
            MainMenu.AddGroupLabel("Cài Đặt Chung");
            MainMenu.Add("fowDetection", new CheckBox("Phát hiện sử dụng skill ám sát"));
            MainMenu.AddLabel("On: for dodging through fog of war, Off: for more human behaviour");
            MainMenu.AddSeparator(3);

            MainMenu.Add("processSpellDetection", new CheckBox("Kích hoạt tính năng quá trình Phát hiện lỗi"));
            MainMenu.AddLabel("phát hiện skillshot trước khi tên lửa được tạo ra tướng AD, khuyến cáo: On");
            MainMenu.AddSeparator(3);

            MainMenu.Add("limitDetectionRange", new CheckBox("Phát hiện sử dụng skill ám sát"));
            MainMenu.AddLabel("phát hiện chỉ có skillshots gần bạn, đề nghị: On");
            MainMenu.AddSeparator(3);

            MainMenu.Add("recalculatePosition", new CheckBox("Cho phép tính toán lại vị trí né tránh", false));
            MainMenu.AddLabel("cho phép thay đổi trốn tránh, đề nghị: Tắt");
            MainMenu.AddSeparator(3);

            MainMenu.Add("moveToInitialPosition", new CheckBox("Di chuyển đến vị trí mong muốn sau khi né tránh.", false));
            MainMenu.AddLabel("di chuyển đến vị trí mong muốn của bạn sau khi trốn");
            MainMenu.AddSeparator(3);

            MainMenu.Add("serverTimeBuffer", new Slider("Server Time Buffer", 0, 0, 200));
            MainMenu.AddLabel("the extra time it is included during evade calculation");
            MainMenu.AddSeparator();

            MainMenu.AddGroupLabel("Humanizer");
            MainMenu.Add("skillshotActivationDelay", new Slider("Né skill chậm trễ", 0, 0, 400));
            MainMenu.AddSeparator(10);

            MainMenu.Add("extraEvadeRange", new Slider("thêm Né Skill dài", 0, 0, 300));
            MainMenu.Add("randomizeExtraEvadeRange", new CheckBox("Ngẫu nhiên thêm né skill trong  Phạm vi", false));

            // Set up skillshot menu
            var heroes = Program.DeveloperMode ? EntityManager.Heroes.AllHeroes : EntityManager.Heroes.Enemies;
            var heroNames = heroes.Select(obj => obj.ChampionName).ToArray();
            var skillshots =
                SkillshotDatabase.Database.Where(s => heroNames.Contains(s.SpellData.ChampionName)).ToList();
            skillshots.AddRange(
                SkillshotDatabase.Database.Where(
                    s =>
                        s.SpellData.ChampionName == "AllChampions" &&
                        heroes.Any(obj => obj.Spellbook.Spells.Select(c => c.Name).Contains(s.SpellData.SpellName))));

            SkillshotMenu = MainMenu.AddSubMenu("Chiêu tướng (Skill)");
            SkillshotMenu.AddLabel(string.Format("Skillshots Loaded {0}", skillshots.Count));
            SkillshotMenu.AddSeparator();

            foreach (var c in skillshots)
            {
                var skillshotString = c.ToString().ToLower();

                if (MenuSkillshots.ContainsKey(skillshotString))
                    continue;

                MenuSkillshots.Add(skillshotString, c);

                SkillshotMenu.AddGroupLabel(c.DisplayText);
                SkillshotMenu.Add(skillshotString + "/enable", new CheckBox("Dodge"));
                SkillshotMenu.Add(skillshotString + "/draw", new CheckBox("Draw"));

                var dangerous = new CheckBox("Dangerous", c.SpellData.IsDangerous);
                dangerous.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                {
                    GetSkillshot(sender.SerializationId).SpellData.IsDangerous = args.NewValue;
                };
                SkillshotMenu.Add(skillshotString + "/dangerous", dangerous);

                var dangerValue = new Slider("Danger Value", c.SpellData.DangerValue, 1, 5);
                dangerValue.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                {
                    GetSkillshot(sender.SerializationId).SpellData.DangerValue = args.NewValue;
                };
                SkillshotMenu.Add(skillshotString + "/dangervalue", dangerValue);

                SkillshotMenu.AddSeparator();
            }

            // Set up spell menu
            SpellMenu = MainMenu.AddSubMenu("Né Tướng đánh Phép");
            SpellMenu.AddGroupLabel("Flash");
            SpellMenu.Add("flash", new Slider("Danger Value", 5, 0, 5));

            // Set up draw menu
            DrawMenu = MainMenu.AddSubMenu("vẽ đường đi của skill");
            DrawMenu.AddGroupLabel("Evade Drawings");
            DrawMenu.Add("disableAllDrawings", new CheckBox("Vô hiệu hoá tất cả các vẽ đường đi của skill", false));
            DrawMenu.Add("drawEvadePoint", new CheckBox("Vẽ điểm né skill"));
            DrawMenu.Add("drawEvadeStatus", new CheckBox("vẽ trạng thái né skill"));
            DrawMenu.Add("drawDangerPolygon", new CheckBox("Vẽ Nguy hiểm", false));
            DrawMenu.AddSeparator();
            DrawMenu.Add("drawPath", new CheckBox("Vẽ auto đường đi bị gank"));

            // Set up controls menu
            HotkeysMenu = MainMenu.AddSubMenu("Phím tắt");
            HotkeysMenu.AddGroupLabel("Phím nhanh");
            HotkeysMenu.Add("enableEvade", new KeyBind("vô hiệu hóa Né Skill", true, KeyBind.BindTypes.PressToggle, 'M'));
            HotkeysMenu.Add("dodgeOnlyDangerous", new KeyBind("Chỉ tránh skill nguy hiểm", false, KeyBind.BindTypes.HoldActive));
        }

        private static EvadeSkillshot GetSkillshot(string s)
        {
            return MenuSkillshots[s.ToLower().Split('/')[0]];
        }

        public static bool IsSkillshotEnabled(EvadeSkillshot skillshot)
        {
            var valueBase = SkillshotMenu[skillshot + "/enable"];
            return valueBase != null && valueBase.Cast<CheckBox>().CurrentValue;
        }

        public static bool IsSkillshotDrawingEnabled(EvadeSkillshot skillshot)
        {
            var valueBase = SkillshotMenu[skillshot + "/draw"];
            return valueBase != null && valueBase.Cast<CheckBox>().CurrentValue;
        }
    }
}