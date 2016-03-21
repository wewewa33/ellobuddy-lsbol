using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Microsoft.Win32;
using SharpDX;
using System.Threading;
using System.Net;
using EloBuddy.SDK.Rendering;
using System.Diagnostics;
using Color = System.Drawing.Color;
using System.Drawing;

namespace Skin
{
    class Program
    {
        public static Menu Menu;//Menu
        public static String NomeChamp = Player.Instance.ChampionName;//Detecta o Campeão carregado original
        public static Text Text1 = new EloBuddy.SDK.Rendering.Text("", new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Bold));
        public static Text Text = new EloBuddy.SDK.Rendering.Text("", new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 10, System.Drawing.FontStyle.Bold));
        //Load
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_Iniciar;
           // EloBuddy.Hacks.RenderWatermark = false;
        }
        //Load Menu
        private static void Game_Iniciar(EventArgs args)
        {
            var MeuCampeao = ObjectManager.Player.ChampionName;
            Menu = MainMenu.AddMenu("SkinHack 2016 BETA", "SkinModelHack");
            Menu.AddGroupLabel("/////////////////////////////////////////");
            Menu.AddGroupLabel("  ◣  SkinHack ◥");
            Menu.Add("UseSkinHack", new CheckBox("✔   (" + MeuCampeao + " - Use SkinHack 1 To 11)", true));
            Menu.Add("ResetSkin&Model", new KeyBind("Reset / Resetar (Bug Reset)", false, KeyBind.BindTypes.HoldActive, 'L'));
            var SkinHack = Menu.Add("SkinID", new Slider("SkinHack Select", 1, 0, 11));
            var ID = new[] {"Classic","SkinHack 1","SkinHack 2","SkinHack 3","SkinHack 4","SkinHack 5","SkinHack 6","SkinHack 7","SkinHack 8","SkinHack 9","SkinHack 10","SkinHack 11"};
            SkinHack.DisplayName = ID[SkinHack.CurrentValue];
            SkinHack.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs){sender.DisplayName = ID[changeArgs.NewValue];};

           /* Menu.AddGroupLabel("─────────────────────────────────────────");
            Menu.AddGroupLabel("  ◣  Ward Skin ◥");
            var WardSkin = Menu.Add("WardID", new Slider("Ward Select", 1, 0, 50));
            var ID2 = new[] { "Classic", "WardSkin 1", "WardSkin 2", "WardSkin 3", "WardSkin 4", "WardSkin 5", "WardSkin 6", "WardSkin 7", "WardSkin 8", "WardSkin 9", "WardSkin 10", "WardSkin 11" };
            WardSkin.DisplayName = ID2[WardSkin.CurrentValue];
            WardSkin.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs) { sender.DisplayName = ID2[changeArgs.NewValue]; };
            */



            //Chat.Print(Player.Instance.BaseSkinName);
            Player.Instance.SetModel(NomeChamp);//Reset My Champion!
            Menu.AddGroupLabel("─────────────────────────────────────────");
            Menu.AddGroupLabel("  ◤  ModelHack  ◢");
            Menu.Add("UseModelHack", new CheckBox("✔   (" + MeuCampeao + " - Use ModelHack 1 To 128)", false));
            Menu.Add("ModelLoad", new KeyBind("Active / Ligar", false, KeyBind.BindTypes.HoldActive, 'N'));
            var ModelHack =  Menu.Add("ModelID", new Slider("ModelHack Select", 1, 0, 128));
            var ID1 = new[] {
            "Aatrox","Ahri","Akali","Alistar","Amumu","Anivia","Annie","Ashe","Azir","Bard","Blitzcrank","Brand","Braum","Caitlyn",
            "Cassiopeia","ChoGath","Corki","Darius","Diana","Draven","Ekko","Elise","Evelynn","Ezreal","Fiddlesticks","Fiora","Fizz",
            "Galio","Gangplank","Garen","Gnar","Gragas","Graves"," Hecarim","Heimerdinger","Illaoi","Irelia","Janna"," Jarvan IV ","Jax",
            "Jayce","Jhin","Jinx","Kalista","Karma","Karthus","Kassadin","Katarina","Kayle","Kennen","Khazix","Kindred","KogMaw","LeBlanc",
            "LeeSin","Leona","Lissandra","Lucian","Lulu","Lux","Malphite","Malzahar","Maokai"," MasterYi","MissFortune","Mordekaiser","Morgana",
            "Dr.Mundo","Nami","Nasus","Nautilus","Nidalee","Nocturne","Nunu","Olaf","Orianna","Pantheon","Poppy","Quin","Rammus","RekSai",
            "Renekton","Rengar","Riven","Rumble","Ryze","Sejuani","Shaco","Shen","Shyvana","Singed","Sion","Sivir"," Skarner","Sona","Soraka",
            "Swain","Syndra","TahmKench","Talon","Taric","Teemo","Thresh","Tristana","Trundle","Tryndamere","TwistedFate","Twitch","Udyr","Urgot",
            "Varus","Vayne","Veigar","Velkoz","Vi","Viktor","Vladimir","Volibear","Warwick","Wukong","Xerath","XinZhao","Yasuo","Yorick","Zac",
            "Zed","Ziggs","Zilean","Zyra"};
            ModelHack.DisplayName = ID1[ModelHack.CurrentValue];
            ModelHack.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs) { sender.DisplayName = ID1[changeArgs.NewValue]; };
          
            
            Menu.AddGroupLabel("─────────────────────────────────────────");
            Menu.AddGroupLabel("  ◣  Extra  ◥");
            Menu.Add("DrawTarget", new CheckBox("✔   GetLine ( TargetSelector )", true));
            Menu.Add("DrawTEXT", new CheckBox("✔   Show Text ( Config )", true));
            Menu.AddGroupLabel("─────────────────────────────────────────");
            Menu.AddGroupLabel("By: UnrealSkill99");

            Chat.Print("|| SkinHack 2016 BETA || <font color='#00d459'>By: UnrealSkill99 </font>", System.Drawing.Color.White);
            Chat.Print("|| SkinHack 2016 BETA || <font color='#00d459'>Model Original Load " + NomeChamp + "</font>", System.Drawing.Color.White);
            Game.OnTick += Game_Atualizar;
            Drawing.OnDraw += Game_OnDraw;
        }

        //Drawing
     public static void Game_OnDraw(EventArgs args)
        {
       
            if (Menu["DrawTEXT"].Cast<CheckBox>().CurrentValue)
            {
               /* Drawing.DrawText(Drawing.Width - 290, 100, Color.Gray, "SkinHack ID: ");
                Drawing.DrawText(Drawing.Width - 200, 100, Color.White, Program.Menu["SkinID"].DisplayName);

                Drawing.DrawText(Drawing.Width - 290, 80, Color.Gray, "ModelHack ID: ");
                Drawing.DrawText(Drawing.Width - 190, 80, Color.White, Program.Menu["ModelID"].DisplayName);

                Drawing.DrawText(Drawing.Width - 290, 60, Color.Gray, "Original ID: ");
                Drawing.DrawText(Drawing.Width - 215, 60, Color.White, NomeChamp.ToString());*/
                
                Text1.Position = Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(100, -20);
                Text1.Color = Color.White ;
                Text1.TextValue = "✔ Original: ";
                Text1.Draw();
                Text1.Position = Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(10, -20);
                Text1.Color = Color.LimeGreen;
                Text1.TextValue = NomeChamp.ToString();
                Text1.Draw();

                Text1.Position = Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(100, -40);
                Text1.Color = Color.White;
                Text1.TextValue = "✔ ModelHack: ";
                Text1.Draw();
                Text1.Position = Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(10, -40);
                Text1.Color = Color.LimeGreen;
                Text1.TextValue = Program.Menu["ModelID"].DisplayName;
                Text1.Draw();

                Text1.Position = Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(100, -60);
                Text1.Color = Color.White;
                Text1.TextValue = "✔ SkinHack: ";
                Text1.Draw();
                Text1.Position = Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(10, -60);
                Text1.Color = Color.LimeGreen;
                Text1.TextValue = Program.Menu["SkinID"].DisplayName;
                Text1.Draw();
            }
            //Drawing TargetSelect
            if (Menu["DrawTarget"].Cast<CheckBox>().CurrentValue)
            {
                var Inimigo = TargetSelector.GetTarget(1500, DamageType.Physical);
                new Circle() { Color = Color.White, Radius = ObjectManager.Player.GetAutoAttackRange(), BorderWidth = 4 }.Draw(ObjectManager.Player.Position);
                Drawing.DrawLine(ObjectManager.Player.Position.WorldToScreen(), Inimigo.Position.WorldToScreen(), 3, Color.White);
                foreach (var ai in EntityManager.Heroes.Enemies)
                {
                    if (ai.IsValidTarget())
                    {
                        Text.Position = Drawing.WorldToScreen(Inimigo.Position) - new Vector2(0, 0);
                        Text.Color = Color.White;
                        Text.TextValue = "✖ " + Inimigo.ChampionName + "";
                        Text.Draw();
                    }
                }
            }
        }
       
        //DetectaObjeto de Jogador
     public static void DetectarObjeto()
        {
            var SkinHackSelect = Program.Menu["SkinID"].DisplayName;
            if (NomeChamp == "Aatrox")
            {

            }
            if (NomeChamp == "Ahri")
            {

            }
            if (NomeChamp == "Akali")
            {
            }
            if (NomeChamp == "Alistar")
            {
            }
            if (NomeChamp == "Amumu")
            {
            }
            if (NomeChamp == "Anivia")
            {
            }
            if (NomeChamp == "Annie")
            {
                foreach (var Objeto in ObjectManager.Get<Obj_AI_Minion>().Where(o => o.IsValid && o.BaseSkinName == "AnnieTibbers"))
                {
                    switch (SkinHackSelect)
                    {
                        case "Classic":
                            Objeto.SetSkinId(0);
                            break;
                        case "SkinHack 1":
                            Objeto.SetSkinId(1);
                            break;
                        case "SkinHack 2":
                            Objeto.SetSkinId(2);
                            break;
                        case "SkinHack 3":
                            Objeto.SetSkinId(3);
                            break;
                        case "SkinHack 4":
                            Objeto.SetSkinId(4);
                            break;
                        case "SkinHack 5":
                            Objeto.SetSkinId(5);
                            break;
                        case "SkinHack 6":
                            Objeto.SetSkinId(6);
                            break;
                        case "SkinHack 7":
                            Objeto.SetSkinId(7);
                            break;
                        case "SkinHack 8":
                            Objeto.SetSkinId(8);
                            break;
                        case "SkinHack 9":
                            Objeto.SetSkinId(9);
                            break;
                        case "SkinHack 10":
                            Objeto.SetSkinId(10);
                            break;
                    }
                    //                }
                }
            }
            if (NomeChamp == "Ashe")
            {
            }
            if (NomeChamp == "Azir")
            {
          
            }
            if (NomeChamp == "Bard")
            {
            }
            if (NomeChamp == "Blitzcrank")
            {
            }
            if (NomeChamp == "Brand")
            {
            }
            if (NomeChamp == "Braum")
            {
            }
            if (NomeChamp == "Caitlyn")
            {
            }
            if (NomeChamp == "Cassiopeia")
            {
            }
            if (NomeChamp == "ChoGath")
            {
            }
            if (NomeChamp == "Corki")
            {
            }
            if (NomeChamp == "Darius")
            {
            }
            if (NomeChamp == "Diana")
            {
            }
            if (NomeChamp == "Draven")
            {
            }
            if (NomeChamp == "Ekko")
            {
            }
            if (NomeChamp == "Elise")
            {
            }
            if (NomeChamp == "Evelynn")
            {
            }
            if (NomeChamp == "Ezreal")
            {
            }
            if (NomeChamp == "Fiddlesticks")
            {
            }
            if (NomeChamp == "Fiora")
            {
            }
            if (NomeChamp == "Fizz")
            {
            }
            if (NomeChamp == "Galio")
            {
            }
            if (NomeChamp == "Gangplank")
            {
                foreach (var Objeto in ObjectManager.Get<Obj_AI_Base>().Where(o => o.Name.ToLower().Equals("barrel")))
                {
                    switch (SkinHackSelect)
                    {
                        case "Classic":
                            Objeto.SetSkinId(0);
                            break;
                        case "SkinHack 1":
                            Objeto.SetSkinId(1);
                            break;
                        case "SkinHack 2":
                            Objeto.SetSkinId(2);
                            break;
                        case "SkinHack 3":
                            Objeto.SetSkinId(3);
                            break;
                        case "SkinHack 4":
                            Objeto.SetSkinId(4);
                            break;
                        case "SkinHack 5":
                            Objeto.SetSkinId(5);
                            break;
                        case "SkinHack 6":
                            Objeto.SetSkinId(6);
                            break;
                        case "SkinHack 7":
                            Objeto.SetSkinId(7);
                            break;
                    }
                }
             }
            if (NomeChamp == "Garen")
            {
            }
            if (NomeChamp == "Gnar")
            {
            }
            if (NomeChamp == "Gragas")
            {
            }
            if (NomeChamp == "Graves")
            {
            }
            if (NomeChamp == "Hecarim")
            {
            }
            if (NomeChamp == "Heimerdinger")
            {
            }
            if (NomeChamp == "Illaoi")
            {
            }
            if (NomeChamp == "Irelia")
            {
            }
            if (NomeChamp == "Janna")
            {
            }
            if (NomeChamp == "Jarvan IV")
            {
            }
            if (NomeChamp == "Jax")
            {
            }
            if (NomeChamp == "Jayce")
            {
            }
            if (NomeChamp == "Jhin")
            {
            }
            if (NomeChamp == "Jinx")
            {
            }
            if (NomeChamp == "Kalista")
            {
            }
            if (NomeChamp == "Karma")
            {
            }
            if (NomeChamp == "Karthus")
            {
            }
            if (NomeChamp == "Kassadin")
            {
            }
            if (NomeChamp == "Katarina")
            {
            }
            if (NomeChamp == "Kayle")
            {
            }
            if (NomeChamp == "Kennen")
            {
            }
            if (NomeChamp == "Khazix")
            {
            }
            if (NomeChamp == "Kindred")
            {
            }
            if (NomeChamp == "KogMaw")
            {
            }
            if (NomeChamp == "LeBlanc")
            {
            }
            if (NomeChamp == "LeeSin")
            {
            }
            if (NomeChamp == "Leona")
            {
            }
            if (NomeChamp == "Lissandra")
            {
            }
            if (NomeChamp == "Lucian")
            {
            }
            if (NomeChamp == "Lulu")
            {
                var LuluEShield = Player.Instance.HasBuff("LuluEShield");

                foreach (var Particula1 in ObjectManager.Get<Obj_AI_Minion>().Where(o => o.IsValid && !o.IsDead && o.IsVisible && o.BaseSkinName == "LuluFaerie"))
                {
                    switch (SkinHackSelect)
                    {
                        case "Classic":
                            Particula1.SetSkinId(0);
                            break;
                        case "SkinHack 1":
                            Particula1.SetSkinId(1);
                            break;
                        case "SkinHack 2":
                            Particula1.SetSkinId(2);
                            break;
                        case "SkinHack 3":
                            Particula1.SetSkinId(3);
                            break;
                        case "SkinHack 4":
                            Particula1.SetSkinId(4);
                            break;
                        case "SkinHack 5":
                            Particula1.SetSkinId(5);
                            break;
                        case "SkinHack 6":
                            Particula1.SetSkinId(6);
                            break;
                    }
                }
            }
            if (NomeChamp == "Lux")
            {
            }
            if (NomeChamp == "Malphite")
            {
            }
            if (NomeChamp == "Malzahar")
            {
            }
            if (NomeChamp == "Maokai")
            {
            }
            if (NomeChamp == "MasterYi")
            {
            }
            if (NomeChamp == "MissFortune")
            {
            }
            if (NomeChamp == "Mordekaiser")
            {
            }
            if (NomeChamp == "Morgana")
            {
            }
            if (NomeChamp == "DrMundo")
            {
            }
            if (NomeChamp == "Nami")
            {
            }
            if (NomeChamp == "Nasus")
            {
              /*  foreach (var Objeto in ObjectManager.Get<Obj_AI_Minion>().Where(o => o.IsValid && !o.IsDead && o.IsVisible && o.BaseSkinName == "Nasus_E_Green_Ring.troy"))
               // foreach (var Objeto in ObjectManager.Get<Obj_AI_Base>().Where(o => o.Name.ToLower().Equals("Nasus_Base_E_SpiritFire.troy")))
                {
                    switch (SkinHackSelect)
                    {
                        case "Classic":
                            Objeto.SetSkinId(0);
                            break;
                        case "SkinHack 1":
                            Objeto.SetSkinId(1);
                            break;
                        case "SkinHack 2":
                            Objeto.SetSkinId(2);
                            break;
                        case "SkinHack 3":
                            Objeto.SetSkinId(3);
                            break;
                        case "SkinHack 4":
                            Objeto.SetSkinId(4);
                            break;
                        case "SkinHack 5":
                            Objeto.SetSkinId(5);
                            break;
                        case "SkinHack 6":
                            Objeto.SetSkinId(6);
                            break;
                        case "SkinHack 7":
                            Objeto.SetSkinId(7);
                            break;
                    }
                }*/
            }
            if (NomeChamp == "Nautilus")
            {
            }
            if (NomeChamp == "Nidalee")
            {
            }
            if (NomeChamp == "Nocturne")
            {
            }
            if (NomeChamp == "Nunu")
            {
            }
            if (NomeChamp == "Olaf")
            {
              //  foreach (var Objeto in ObjectManager.Get<Obj_AI_Base>().Where(o => o.Name.ToLower().Equals("OlafAxe")))
                foreach (var Objeto in ObjectManager.Get<Obj_AI_Minion>().Where(o => o.IsValid && o.BaseSkinName == "OlafAxe"))
                {
                    switch (SkinHackSelect)
                    {
                        case "Classic":
                            Objeto.SetSkinId(0);
                            break;
                        case "SkinHack 1":
                            Objeto.SetSkinId(1);
                            break;
                        case "SkinHack 2":
                            Objeto.SetSkinId(2);
                            break;
                        case "SkinHack 3":
                            Objeto.SetSkinId(3);
                            break;
                        case "SkinHack 4":
                            Objeto.SetSkinId(4);
                            break;
                        case "SkinHack 5":
                            Objeto.SetSkinId(5);
                            break;
                    }
                }
            }
            if (NomeChamp == "Orianna")
            {
            }
            if (NomeChamp == "Pantheon")
            {
            }
            if (NomeChamp == "Poppy")
            {
            }
            if (NomeChamp == "Quin")
            {
            }
            if (NomeChamp == "Rammus")
            {
            }
            if (NomeChamp == "RekSai")
            {
            }
            if (NomeChamp == "Renekton")
            {
            }
            if (NomeChamp == "Rengar")
            {
            }
            if (NomeChamp == "Riven")
            {
            }
            if (NomeChamp == "Rumble")
            {
            }
            if (NomeChamp == "Ryze")
            {
            }
            if (NomeChamp == "Sejuani")
            {
            }
            if (NomeChamp == "Shaco")
            {
            }
            if (NomeChamp == "Shen")
            {
            }
            if (NomeChamp == "Shyvana")
            {
            }
            if (NomeChamp == "Singed")
            {
            }
            if (NomeChamp == "Sion")
            {
            }
            if (NomeChamp == "Sivir")
            {
            }
            if (NomeChamp == "Skarner")
            {
            }
            if (NomeChamp == "Sona")
            {
            }
            if (NomeChamp == "Soraka")
            {
            }
            if (NomeChamp == "Swain")
            {
            }
            if (NomeChamp == "Syndra")
            {
            }
            if (NomeChamp == "TahmKench")
            {
            }
            if (NomeChamp == "Talon")
            {
            }
            if (NomeChamp == "Taric")
            {
            }
            if (NomeChamp == "Teemo")
            {
            }
            if (NomeChamp == "Thresh")
            {
            }
            if (NomeChamp == "Tristana")
            {
            }
            if (NomeChamp == "Trundle")
            {
            }
            if (NomeChamp == "Tryndamere")
            {
            }
            if (NomeChamp == "TwistedFate")
            {
            }
            if (NomeChamp == "Twitch")
            {
            }
            if (NomeChamp == "Udyr")
            {
                foreach (var Particula1 in ObjectManager.Get<Obj_AI_Base>().Where(o => o.Name.ToLower().Equals("UdyrPhoenix"))
                    .Where(o => o.Name.ToLower().Equals("UdyrPhoenixUlt")).Where(o => o.Name.ToLower().Equals("UdyrTigerUlt"))
                    .Where(o => o.Name.ToLower().Equals("UdyrTurtle")).Where(o => o.Name.ToLower().Equals("UdyrTurtleUlt"))
                    .Where(o => o.Name.ToLower().Equals("UdyrUlt")))
                {
                /*    var Particula2 = ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(o => o.Name.ToLower().Equals("UdyrPhoenixUlt"));
                    var Particula3 = ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(o => o.Name.ToLower().Equals("UdyrTigerUlt"));
                    var Particula4 = ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(o => o.Name.ToLower().Equals("UdyrTurtle"));
                    var Particula5 = ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(o => o.Name.ToLower().Equals("UdyrTurtleUlt"));
                    var Particula6 = ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(o => o.Name.ToLower().Equals("UdyrUlt"));*/
             
                    switch (SkinHackSelect)
                    {
                        case "Classic":
                            Particula1.SetSkinId(0);
                       /*     Particula2.SetSkinId(0);
                            Particula3.SetSkinId(0);
                            Particula4.SetSkinId(0);
                            Particula5.SetSkinId(0);
                            Particula6.SetSkinId(0);
                          //  Particula7.SetSkinId(0);*/
                            break;
                        case "SkinHack 1":
                            Particula1.SetSkinId(1);
                        /*    Particula2.SetSkinId(1);
                            Particula3.SetSkinId(1);
                            Particula4.SetSkinId(1);
                            Particula5.SetSkinId(1);
                            Particula6.SetSkinId(1);
                         //   Particula7.SetSkinId(1);*/
                            break;
                        case "SkinHack 2":
                            Particula1.SetSkinId(2);
                       /*     Particula2.SetSkinId(2);
                            Particula3.SetSkinId(2);
                            Particula4.SetSkinId(2);
                            Particula5.SetSkinId(2);
                            Particula6.SetSkinId(2);
                          //  Particula7.SetSkinId(2);*/
                            break;
                        case "SkinHack 3":
                            Particula1.SetSkinId(3);
                        /*    Particula2.SetSkinId(3);
                            Particula3.SetSkinId(3);
                            Particula4.SetSkinId(3);
                            Particula5.SetSkinId(3);
                            Particula6.SetSkinId(3);
                         //   Particula7.SetSkinId(3);*/
                            break;
                        case "SkinHack 4":
                            Particula1.SetSkinId(4);
                        /*    Particula2.SetSkinId(4);
                            Particula3.SetSkinId(4);
                            Particula4.SetSkinId(4);
                            Particula5.SetSkinId(4);
                            Particula6.SetSkinId(4);
                        //    Particula7.SetSkinId(4);*/
                            break;
                        case "SkinHack 5":
                            Particula1.SetSkinId(5);
                      /*      Particula2.SetSkinId(5);
                            Particula3.SetSkinId(5);
                            Particula4.SetSkinId(5);
                            Particula5.SetSkinId(5);
                            Particula6.SetSkinId(5);
                        //    Particula7.SetSkinId(5);*/
                            break;
                        case "SkinHack 6":
                            Particula1.SetSkinId(6);
                       /*     Particula2.SetSkinId(6);
                            Particula3.SetSkinId(6);
                            Particula4.SetSkinId(6);
                            Particula5.SetSkinId(6);
                            Particula6.SetSkinId(6);
                         //   Particula7.SetSkinId(6);*/
                            break;
                    }
                }
            }
            if (NomeChamp == "Urgot")
            {
            }
            if (NomeChamp == "Varus")
            {
            }
            if (NomeChamp == "Vayne")
            {
            }
            if (NomeChamp == "Veigar")
            {
            }
            if (NomeChamp == "Velkoz")
            {
            }
            if (NomeChamp == "Vi")
            {
            }
            if (NomeChamp == "Viktor")
            {
            }
            if (NomeChamp == "Vladimir")
            {
            }
            if (NomeChamp == "Volibear")
            {
            }
            if (NomeChamp == "Warwick")
            {
            }
            if (NomeChamp == "MonkeyKing")
            {
                foreach (var Objeto in ObjectManager.Get<Obj_AI_Minion>().Where(o => o.IsValid && o.BaseSkinName == "MonkeyKing"))
                {
                    switch (SkinHackSelect)
                    {
                        case "Classic":
                            Objeto.SetSkinId(0);
                            break;
                        case "SkinHack 1":
                            Objeto.SetSkinId(1);
                            break;
                        case "SkinHack 2":
                            Objeto.SetSkinId(2);
                            break;
                        case "SkinHack 3":
                            Objeto.SetSkinId(3);
                            break;
                        case "SkinHack 4":
                            Objeto.SetSkinId(4);
                            break;
                        case "SkinHack 5":
                            Objeto.SetSkinId(5);
                            break;
                    }
                }
            }
            if (NomeChamp == "Xerath")
            {
            }
            if (NomeChamp == "XinZhao")
            {
            }
            if (NomeChamp == "Yasuo")
            {
            }
            if (NomeChamp == "Yorick")
            {
            }
            if (NomeChamp == "Zac")
            {
            }
            if (NomeChamp == "Zed")
            {
                foreach (var Objeto in ObjectManager.Get<Obj_AI_Base>().Where(o => o.Name.ToLower().Equals("ZedShadow")))
                {
                    switch (SkinHackSelect)
                    {
                        case "Classic":
                            Objeto.SetSkinId(0);
                            break;
                        case "SkinHack 1":
                            Objeto.SetSkinId(1);
                            break;
                        case "SkinHack 2":
                            Objeto.SetSkinId(2);
                            break;
                    }
                }
            }
            if (NomeChamp == "Ziggs")
            {
            }
            if (NomeChamp == "Zilean")
            {
            }
            if (NomeChamp == "Zyra")
            {
            }  
        }

        //Skins
     public static void LoadingSkin()
        {
            DetectarObjeto();
            var SkinHackSelect = Program.Menu["SkinID"].DisplayName;
            switch (SkinHackSelect)
            {
                case "Classic":
                   Player.Instance.SetSkinId(0);
                    break;
                case "SkinHack 1":
                  Player.Instance.SetSkinId(1);
                    break;
                case "SkinHack 2":
                  Player.Instance.SetSkinId(2);
                    break;
                case "SkinHack 3":
                 Player.Instance.SetSkinId(3);
                    break;
                case "SkinHack 4":
                  Player.Instance.SetSkinId(4);
                    break;
                case "SkinHack 5":
                    Player.Instance.SetSkinId(5);
                    break;
                case "SkinHack 6":
                  Player.Instance.SetSkinId(6);
                    break;
                case "SkinHack 7":
                  Player.Instance.SetSkinId(7);
                    break;
                case "SkinHack 8":
                   Player.Instance.SetSkinId(8);
                    break;
                case "SkinHack 9":
                   Player.Instance.SetSkinId(9);
                    break;
                case "SkinHack 10":
                Player.Instance.SetSkinId(10);
                    break;
                case "SkinHack 11":
                    Player.Instance.SetSkinId(11);
                    break;
            }
            //Reset model e skin
            if (Menu.Get<KeyBind>("ResetSkin&Model").CurrentValue)
            {
                Player.Instance.SetModel(NomeChamp);
            }
        }

        //ModelHack
     public static void ActiveModelHack()
        {
                if (Menu.Get<KeyBind>("ModelLoad").CurrentValue)
                {
                    var ModelHackSelect = Program.Menu["ModelID"].DisplayName;
                    Player.Instance.SetModel(ModelHackSelect);
                    Chat.Print("|| SkinHack 2016 BETA || <font color='#ff0000'>Carregado / Load ModelHack " + ModelHackSelect + "</font>", System.Drawing.Color.White);
                }
        }
        //Ward Skin
     public static void WardSKinHack()
     {
         foreach (var ObjetoWard in ObjectManager.Get<Obj_AI_Base>().Where(o => o.Name.ToLower().Equals("YellowTrinket")))
         {
             var WardSelect = Program.Menu["WardID"].DisplayName;
             switch (WardSelect)
             {
                 case "Classic":
                     ObjetoWard.SetSkinId(0);
                     break;
                 case "WardSkin 1":
                     ObjetoWard.SetSkinId(1);
                     break;
                 case "WardSkin 2":
                     ObjetoWard.SetSkinId(2);
                     break;
                 case "WardSkin 3":
                     ObjetoWard.SetSkinId(3);
                     break;
                 case "WardSkin 4":
                     ObjetoWard.SetSkinId(4);
                     break;
                 case "WardSkin 5":
                     ObjetoWard.SetSkinId(5);
                     break;
                 case "WardSkin 6":
                     ObjetoWard.SetSkinId(6);
                     break;
                 case "WardSkin 7":
                     ObjetoWard.SetSkinId(7);
                     break;
                 case "WardSkin 8":
                     ObjetoWard.SetSkinId(8);
                     break;
                 case "WardSkin 9":
                     ObjetoWard.SetSkinId(9);
                     break;
                 case "WardSkin 10":
                     ObjetoWard.SetSkinId(10);
                     break;
                 case "WardSkin 11":
                     ObjetoWard.SetSkinId(11);
                     break;
             }
         }

     }

        private static void Game_Atualizar(EventArgs args)
        {
            if (Menu["UseSkinHack"].Cast<CheckBox>().CurrentValue) LoadingSkin();
            if (Menu["UseModelHack"].Cast<CheckBox>().CurrentValue) ActiveModelHack();
             //   WardSKinHack();
        }
    }
}
