using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;
using SharpDX;
using Color = System.Drawing.Color;

namespace TowerFocus
{
    class Program
    {
        public static readonly Obj_AI_Hero player = ObjectManager.Player; //get player function
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad; // Braucht man, ansonsten funzt es nicht
        }

        private static void Game_OnGameLoad(EventArgs args)
        {

            Drawing.OnDraw += Drawing_OnDraw; //Braucht man wenn man etwas drawen will
            Game.OnGameUpdate += Game_OnGameUpdate; //wiederholt sich schneller als OnGameLoad 
            Game.PrintChat("<font color='#9933FF'>Herkules</font><font color='#FFFFFF'>- TowerRange</font>"); //because is mine :)
        }


        private static void Drawing_OnDraw(EventArgs args) //if statement um tower in range ist & ob man etwas ausgeben muss
        {

            var nearestHero = GetTurret(); //GetTurrent function wird gecalled und das ergebnis in nearestHero gesetzt
             if (nearestHero != null) //Wenn kein turret in range ist, soll nichts passieren "null"
             {
                 Utility.DrawCircle(nearestHero.Position, 125f, Color.BlueViolet); //wenn doch soll er es drawen
             }
        }

         private static Obj_AI_Hero GetTurret() 
         {
             Obj_AI_Hero target = null; //variable ist null, wenn man  nichts findet, das ist standart
              var nearestTurret = ObjectManager.Get<Obj_AI_Turret>().First(turret => !turret.IsDead && turret.IsEnemy && player.Distance(turret.Position) < 775f);
        // get list of turrets, erster turret, nicht tod, turret is enemy, und in einer range von unter 775
        if (nearestTurret!=null) //wenn es den turret gibt, get nearest hero
        {
            target = ObjectManager.Get<Obj_AI_Hero>().Where(hero => hero.Distance(nearestTurret) < 775f && !hero.IsDead && (hero.IsAlly || hero.IsMe)).OrderBy(hero => hero.Distance(nearestTurret)).FirstOrDefault();
        //target = in turret range, alive, verbündet und am nähsten am tower
        }
            return target;


    }
      private static void Game_OnGameUpdate(EventArgs args)
      {
             //Doesn't need now
        }
    }
}
