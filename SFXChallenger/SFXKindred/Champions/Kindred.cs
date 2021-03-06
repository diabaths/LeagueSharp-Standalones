#region License

/*
 Copyright 2014 - 2015 Nikita Bernthaler
 Kindred.cs is part of SFXKindred.

 SFXKindred is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 SFXKindred is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with SFXKindred. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion License

///*
// Copyright 2014 - 2015 Nikita Bernthaler
// Kindred.cs is part of SFXKindred.

// SFXKindred is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// SFXKindred is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with SFXKindred. If not, see <http://www.gnu.org/licenses/>.
//*/

//#endregion License

////            //TODO: Check if buff name is correct, Champion charge
////            TargetSelector.Weights.Register(
////                new TargetSelector.Weights.Item(
////                    "kindred-charge", "Kindred Charge", 10, false, hero => hero.HasBuff("kindredcharge") ? 1 : 0));
////            //TODO: E Debuff?
////            //TargetSelector.Weights.Register(new TargetSelector.Weights.Item("kindred-charge", "Kindred Charge", 10, false, hero => hero.HasBuff("kindredcharge") ? 1 : 0));



//#region License

///*
// Copyright 2014 - 2015 Nikita Bernthaler
// MissFortune.cs is part of SFXKindred.

// SFXKindred is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// SFXKindred is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with SFXKindred. If not, see <http://www.gnu.org/licenses/>.
//*/

//#endregion License

//#region

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using LeagueSharp;
//using LeagueSharp.Common;
//using SFXKindred.Abstracts;
//using SFXKindred.Args;
//using SFXKindred.Enumerations;
//using SFXKindred.Helpers;
//using SFXKindred.Library;
//using SFXKindred.Library.Logger;
//using SFXKindred.Managers;
//using SharpDX;
//using MinionManager = SFXKindred.Library.MinionManager;
//using MinionOrderTypes = SFXKindred.Library.MinionOrderTypes;
//using MinionTeam = SFXKindred.Library.MinionTeam;
//using MinionTypes = SFXKindred.Library.MinionTypes;
//using Orbwalking = SFXKindred.SFXTargetSelector.Orbwalking;
//using Spell = SFXKindred.Wrappers.Spell;
//using TargetSelector = SFXKindred.SFXTargetSelector.TargetSelector;
//using Utils = SFXKindred.Helpers.Utils;

//#endregion

//namespace SFXKindred.Champions
//{
//    internal class KindredTesting : Champion
//    {
//        private const float QAdditionalRange = 500f;
//        private const float RAdditionalRange = 500f;
//        protected override ItemFlags ItemFlags
//        {
//            get { return ItemFlags.Offensive | ItemFlags.Defensive | ItemFlags.Flee; }
//        }

//        protected override ItemUsageType ItemUsage
//        {
//            get { return ItemUsageType.AfterAttack; }
//        }

//        protected override void OnLoad()
//        {
//            GapcloserManager.OnGapcloser += OnEnemyGapcloser;
//            Orbwalking.AfterAttack += OnOrbwalkingAfterAttack;
//        }

//        protected override void SetupSpells()
//        {
//            Q = new Spell(SpellSlot.Q, 340f); //+500

//            W = new Spell(SpellSlot.W, 800f);

//            E = new Spell(SpellSlot.E, 500f);
//            E.Range += GameObjects.EnemyHeroes.Select(e => e.BoundingRadius).DefaultIfEmpty(25).Min();

//            R = new Spell(SpellSlot.R, 400f); //+500
//            R.Range += GameObjects.EnemyHeroes.Select(e => e.BoundingRadius).DefaultIfEmpty(25).Min();
//        }

//        protected override void AddToMenu()
//        {
//            var comboMenu = Menu.AddSubMenu(new Menu("Combo", Menu.Name + ".combo"));
//            comboMenu.AddItem(new MenuItem(comboMenu.Name + ".q", "Use Q").SetValue(true));
//            comboMenu.AddItem(new MenuItem(comboMenu.Name + ".w", "Use W").SetValue(true));
//            comboMenu.AddItem(new MenuItem(comboMenu.Name + ".e", "Use E").SetValue(true));

//            var harassMenu = Menu.AddSubMenu(new Menu("Harass", Menu.Name + ".harass"));
//            ResourceManager.AddToMenu(
//                harassMenu,
//                new ResourceManagerArgs(
//                    "harass", ResourceType.Mana, ResourceValueType.Percent, ResourceCheckType.Minimum)
//                {
//                    DefaultValue = 30
//                });
//            harassMenu.AddItem(new MenuItem(harassMenu.Name + ".q", "Use Q").SetValue(true));
//            harassMenu.AddItem(new MenuItem(harassMenu.Name + ".w", "Use W").SetValue(false));
//            harassMenu.AddItem(new MenuItem(harassMenu.Name + ".e", "Use E").SetValue(false));

//            var laneClearMenu = Menu.AddSubMenu(new Menu("Lane Clear", Menu.Name + ".lane-clear"));
//            ResourceManager.AddToMenu(
//                laneClearMenu,
//                new ResourceManagerArgs(
//                    "lane-clear", ResourceType.Mana, ResourceValueType.Percent, ResourceCheckType.Minimum)
//                {
//                    Advanced = true,
//                    LevelRanges = new SortedList<int, int> { { 1, 6 }, { 6, 12 }, { 12, 18 } },
//                    DefaultValues = new List<int> { 50, 40, 40 }
//                });
//            laneClearMenu.AddItem(new MenuItem(laneClearMenu.Name + ".q", "Use Q").SetValue(true));
//            laneClearMenu.AddItem(new MenuItem(laneClearMenu.Name + ".q-min", "Q Min.").SetValue(new Slider(3, 1, 5)));
//            laneClearMenu.AddItem(new MenuItem(laneClearMenu.Name + ".w", "Use W").SetValue(true));
//            laneClearMenu.AddItem(new MenuItem(laneClearMenu.Name + ".w-min", "W Min.").SetValue(new Slider(5, 1, 10)));
//            laneClearMenu.AddItem(new MenuItem(laneClearMenu.Name + ".e", "Use E").SetValue(true));
//            laneClearMenu.AddItem(new MenuItem(laneClearMenu.Name + ".e-min", "E Min.").SetValue(new Slider(3, 1, 5)));

//            var jungleClearMenu = Menu.AddSubMenu(new Menu("Jungle Clear", Menu.Name + ".jungle-clear"));
//            ResourceManager.AddToMenu(
//                jungleClearMenu,
//                new ResourceManagerArgs(
//                    "jungle-clear", ResourceType.Mana, ResourceValueType.Percent, ResourceCheckType.Minimum)
//                {
//                    Advanced = true,
//                    LevelRanges = new SortedList<int, int> { { 1, 6 }, { 6, 12 }, { 12, 18 } },
//                    DefaultValues = new List<int> { 30, 20, 20 }
//                });
//            jungleClearMenu.AddItem(new MenuItem(jungleClearMenu.Name + ".q", "Use Q").SetValue(true));
//            jungleClearMenu.AddItem(new MenuItem(jungleClearMenu.Name + ".w", "Use W").SetValue(true));
//            jungleClearMenu.AddItem(new MenuItem(jungleClearMenu.Name + ".e", "Use E").SetValue(true));

//            var fleeMenu = Menu.AddSubMenu(new Menu("Flee", Menu.Name + ".flee"));
//            fleeMenu.AddItem(new MenuItem(fleeMenu.Name + ".q", "Use Q").SetValue(true));
//            fleeMenu.AddItem(new MenuItem(fleeMenu.Name + ".w", "Use W").SetValue(true));
//            fleeMenu.AddItem(new MenuItem(fleeMenu.Name + ".e", "Use E").SetValue(true));

//            var miscMenu = Menu.AddSubMenu(new Menu("Misc", Menu.Name + ".miscellaneous"));

//            var eGapcloserMenu = miscMenu.AddSubMenu(new Menu("E Gapcloser", miscMenu.Name + "e-gapcloser"));
//            GapcloserManager.AddToMenu(
//                eGapcloserMenu,
//                new HeroListManagerArgs("e-gapcloser")
//                {
//                    IsWhitelist = false,
//                    Allies = false,
//                    Enemies = true,
//                    DefaultValue = false
//                }, true);
//            BestTargetOnlyManager.AddToMenu(eGapcloserMenu, "e-gapcloser", true);

//            IndicatorManager.AddToMenu(DrawingManager.Menu, true);
//           // IndicatorManager.Add("Q", hero => Q.IsReady() ? 1 : 0);
//            IndicatorManager.Add(Q);
//            IndicatorManager.Add(E);
//            IndicatorManager.Finale();
//        }

//        private void OnOrbwalkingAfterAttack(AttackableUnit unit, AttackableUnit target)
//        {
//            try
//            {
//                if (unit.IsMe)
//                {
//                    var useW = false;
//                    if (Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear)
//                    {
//                        var minion = target as Obj_AI_Minion;
//                        if (minion != null)
//                        {
//                            if (target.Team == GameObjectTeam.Neutral)
//                            {
//                                useW = Menu.Item(Menu.Name + ".jungle-clear.w").GetValue<bool>() &&
//                                       ResourceManager.Check("jungle-clear");
//                            }
//                            else
//                            {
//                                useW = Menu.Item(Menu.Name + ".lane-clear.w").GetValue<bool>() &&
//                                       ResourceManager.Check("lane-clear") &&
//                                       MinionManager.GetMinions(W.Range).Count >=
//                                       Menu.Item(Menu.Name + ".lane-clear.w-min").GetValue<Slider>().Value;
//                            }
//                        }
//                    }
//                    if (Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Mixed)
//                    {
//                        useW = target is Obj_AI_Hero && Menu.Item(Menu.Name + ".harass.w").GetValue<bool>();
//                    }
//                    if (Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo)
//                    {
//                        useW = target is Obj_AI_Hero && Menu.Item(Menu.Name + ".combo.w").GetValue<bool>();
//                    }
//                    if (useW)
//                    {
//                        W.Cast();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Global.Logger.AddItem(new LogItem(ex));
//            }
//        }

//        private Obj_AI_Hero GetBestRTarget(Obj_AI_Hero hero)
//        {
//            var cEnemyCount = int.MaxValue;
//            var cDistance = float.MaxValue;
//            Obj_AI_Hero cAlly = null;
//            foreach (var ally in GameObjects.AllyHeroes.Where(a => a.Distance(Player) <= R.Range && a.Distance(hero) <= RAdditionalRange * 0.8f))
//            {
//                var distance = ally.Distance(hero);
//                var enemyCount = ally.CountEnemiesInRange(RAdditionalRange * 1.2f) - ally.CountAlliesInRange(RAdditionalRange * 0.8f) - 1;
//                if (enemyCount < cEnemyCount || enemyCount == cEnemyCount && (distance < cDistance || ally.NetworkId.Equals(hero.NetworkId)))
//                {
//                    cEnemyCount = enemyCount;
//                    cDistance = distance;
//                    cAlly = ally;
//                }
//            }
//            return cAlly ?? hero;
//        }

//        protected override void OnPreUpdate()
//        {
//            if (Game.Time - _lastRCast < 5 && !_lastRPosition.Equals(Vector3.Zero))
//            {
//                var hits = GameObjects.EnemyHeroes.Count(e => e.IsValidTarget(R.Width * 2.5f, true, _lastRPosition));
//                if (hits <= 0)
//                {
//                    BlockOrdersManager.Automatic = false;
//                    BlockOrdersManager.Enabled = false;
//                    Player.IssueOrder(GameObjectOrder.Stop, Player.Position);
//                    Utility.DelayAction.Add(1000, delegate { BlockOrdersManager.Automatic = true; });
//                }
//            }
//        }

//        protected override void OnPostUpdate()
//        {
//            if (Ultimate.IsActive(UltimateModeType.Assisted) && R.IsReady())
//            {
//                if (Ultimate.ShouldMove(UltimateModeType.Assisted))
//                {
//                    Orbwalking.MoveTo(Game.CursorPos, Orbwalker.HoldAreaRadius);
//                }

//                if (!RLogic(UltimateModeType.Assisted, R.GetHitChance("combo"), TargetSelector.GetTarget(R)))
//                {
//                    RLogicSingle(UltimateModeType.Assisted, R.GetHitChance("combo"));
//                }
//            }

//            if (Ultimate.IsActive(UltimateModeType.Auto) && R.IsReady())
//            {
//                if (!RLogic(UltimateModeType.Auto, R.GetHitChance("combo"), TargetSelector.GetTarget(R)))
//                {
//                    RLogicSingle(UltimateModeType.Auto, R.GetHitChance("combo"));
//                }
//            }
//        }

//        private void OnEnemyGapcloser(object sender, GapcloserManagerArgs args)
//        {
//            try
//            {
//                if (args.UniqueId.Equals("e-gapcloser") && E.IsReady() &&
//                    BestTargetOnlyManager.Check("e-gapcloser", E, args.Hero))
//                {
//                    if (args.End.Distance(Player.Position) <= E.Range)
//                    {
//                        E.Cast(args.End);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Global.Logger.AddItem(new LogItem(ex));
//            }
//        }

//        protected override void Combo()
//        {
//            var q = Menu.Item(Menu.Name + ".combo.q").GetValue<bool>();
//            var e = Menu.Item(Menu.Name + ".combo.e").GetValue<bool>();
//            var r = Ultimate.IsActive(UltimateModeType.Combo);

//            if (q && Q.IsReady())
//            {
//                QLogic();
//            }
//            if (e && E.IsReady())
//            {
//                var target = TargetSelector.GetTarget(E);
//                if (target != null)
//                {
//                    ELogic(target, E.GetHitChance("combo"));
//                }
//            }
//            if (r && R.IsReady())
//            {
//                var target = TargetSelector.GetTarget(R);
//                if (target != null)
//                {
//                    if (!RLogic(UltimateModeType.Combo, R.GetHitChance("combo"), target))
//                    {
//                        RLogicSingle(UltimateModeType.Combo, R.GetHitChance("combo"));
//                    }
//                }
//            }
//        }

//        protected override void Harass()
//        {
//            if (!ResourceManager.Check("harass"))
//            {
//                return;
//            }
//            var q = Menu.Item(Menu.Name + ".harass.q").GetValue<bool>();
//            var e = Menu.Item(Menu.Name + ".harass.e").GetValue<bool>();

//            if (q && Q.IsReady())
//            {
//                QLogic();
//            }
//            if (e && E.IsReady())
//            {
//                var target = TargetSelector.GetTarget(E);
//                if (target != null)
//                {
//                    ELogic(target, E.GetHitChance("harass"));
//                }
//            }
//        }

//        private void QLogic()
//        {
//            try
//            {
//                var target = TargetSelector.GetTarget(Q.Range);
//                if (target != null)
//                {
//                    Q.CastOnUnit(target);
//                }
//                else if (Menu.Item(Menu.Name + ".miscellaneous.extended-q").GetValue<bool>())
//                {
//                    target = TargetSelector.GetTarget(Q1);
//                    if (target != null)
//                    {
//                        var heroPositions = (from t in GameObjects.EnemyHeroes
//                                             where t.IsValidTarget(Q1.Range)
//                                             let prediction = Q.GetPrediction(t)
//                                             select new CPrediction.Position(t, prediction.UnitPosition)).Where(
//                                t => t.UnitPosition.Distance(Player.Position) < Q1.Range).ToList();
//                        if (heroPositions.Any())
//                        {
//                            var minions = MinionManager.GetMinions(
//                                Q1.Range, MinionTypes.All, MinionTeam.NotAlly, MinionOrderTypes.None);

//                            if (minions.Any(m => m.IsMoving) && !heroPositions.Any(h => HasPassiveDebuff(h.Hero)))
//                            {
//                                return;
//                            }

//                            var outerMinions = minions.Where(m => m.Distance(Player) > Q.Range).ToList();
//                            var innerPositions = minions.Where(m => m.Distance(Player) < Q.Range).ToList();
//                            foreach (var minion in innerPositions)
//                            {
//                                var lMinion = minion;
//                                var coneBuff = new Geometry.Polygon.Sector(
//                                    minion.Position,
//                                    Player.Position.Extend(minion.Position, Player.Distance(minion) + Q.Range * 0.5f),
//                                    (float)(40 * Math.PI / 180), Q1.Range - Q.Range);
//                                var coneNormal = new Geometry.Polygon.Sector(
//                                    minion.Position,
//                                    Player.Position.Extend(minion.Position, Player.Distance(minion) + Q.Range * 0.5f),
//                                    (float)(60 * Math.PI / 180), Q1.Range - Q.Range);
//                                foreach (var enemy in
//                                    heroPositions.Where(
//                                        m => m.UnitPosition.Distance(lMinion.Position) < Q1.Range - Q.Range))
//                                {
//                                    if (coneBuff.IsInside(enemy.Hero) && HasPassiveDebuff(enemy.Hero))
//                                    {
//                                        Q.CastOnUnit(minion);
//                                        return;
//                                    }
//                                    if (coneNormal.IsInside(enemy.UnitPosition))
//                                    {
//                                        var insideCone =
//                                            outerMinions.Where(m => coneNormal.IsInside(m.Position)).ToList();
//                                        if (!insideCone.Any() ||
//                                            enemy.UnitPosition.Distance(minion.Position) <
//                                            insideCone.Select(
//                                                m => m.Position.Distance(minion.Position) - m.BoundingRadius)
//                                                .DefaultIfEmpty(float.MaxValue)
//                                                .Min())
//                                        {
//                                            Q.CastOnUnit(minion);
//                                            return;
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Global.Logger.AddItem(new LogItem(ex));
//            }
//        }

//        private bool HasPassiveDebuff(Obj_AI_Hero target)
//        {
//            return target.HasBuff("missfortunepassive");
//        }

//        private void ELogic(Obj_AI_Hero target, HitChance hitChance)
//        {
//            try
//            {
//                if (target == null)
//                {
//                    return;
//                }
//                var best = CPrediction.Circle(E, target, hitChance);
//                if (best.TotalHits > 0 && !best.CastPosition.Equals(Vector3.Zero))
//                {
//                    E.Cast(best.CastPosition);
//                }
//            }
//            catch (Exception ex)
//            {
//                Global.Logger.AddItem(new LogItem(ex));
//            }
//        }

//        private bool RLogic(UltimateModeType mode, HitChance hitChance, Obj_AI_Hero target)
//        {
//            try
//            {
//                if (target == null || !Ultimate.IsActive(mode))
//                {
//                    return false;
//                }
//                var pred = CPrediction.Circle(R, target, hitChance);
//                if (pred.TotalHits > 0)
//                {
//                    if (Ultimate.Check(mode, pred.Hits))
//                    {
//                        _lastRCast = Game.Time;
//                        _lastRPosition = pred.CastPosition;
//                        R.Cast(pred.CastPosition);
//                        return true;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Global.Logger.AddItem(new LogItem(ex));
//            }
//            return false;
//        }

//        private void RLogicSingle(UltimateModeType mode, HitChance hitChance)
//        {
//            try
//            {
//                if (Ultimate.ShouldSingle(mode))
//                {
//                    return;
//                }
//                foreach (var t in GameObjects.EnemyHeroes.Where(t => Ultimate.CheckSingle(mode, t)))
//                {
//                    var pred = CPrediction.Circle(R, t, hitChance);
//                    if (pred.TotalHits > 0)
//                    {
//                        _lastRCast = Game.Time;
//                        _lastRPosition = pred.CastPosition;
//                        R.Cast(pred.CastPosition);
//                        break;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Global.Logger.AddItem(new LogItem(ex));
//            }
//        }

//        private float CalcUltimateDamage(Obj_AI_Hero target, float resMulti, bool rangeCheck)
//        {
//            try
//            {
//                if (target == null)
//                {
//                    return 0;
//                }

//                float damage = 0;
//                if (R.IsReady() && (!rangeCheck || R.IsInRange(target)))
//                {
//                    var rMana = R.ManaCost * resMulti;
//                    if (rMana <= Player.Mana)
//                    {
//                        var waves = 10 + R.Level * 2 - 2;
//                        if (target.Distance(Player) < 250)
//                        {
//                            waves -= 4;
//                        }
//                        if (!Utils.IsImmobile(target))
//                        {
//                            waves -= 3;
//                            if (!Utils.IsSlowed(target))
//                            {
//                                waves -= 2;
//                            }
//                        }
//                        waves = Math.Max(3, waves);
//                        if (Player.Position.IsUnderTurret(false))
//                        {
//                            waves = target.Distance(Player) > Orbwalking.GetAttackRange(target) * 1.2f ? 1 : 0;
//                        }
//                        damage += R.GetDamage(target) * waves;
//                    }
//                }
//                return damage;
//            }
//            catch (Exception ex)
//            {
//                Global.Logger.AddItem(new LogItem(ex));
//            }
//            return 0;
//        }

//        protected override void LaneClear()
//        {
//            if (!ResourceManager.Check("lane-clear"))
//            {
//                return;
//            }
//            var useQ = Menu.Item(Menu.Name + ".lane-clear.q").GetValue<bool>() && Q.IsReady();
//            var useE = Menu.Item(Menu.Name + ".lane-clear.e").GetValue<bool>() && E.IsReady();
//            var minE = Menu.Item(Menu.Name + ".lane-clear.e-min").GetValue<Slider>().Value;

//            if (useQ)
//            {
//                var minion =
//                    MinionManager.GetMinions(Q.Range)
//                        .FirstOrDefault(m => m.Health > Q.GetDamage(m) * 1.5f || m.Health < Q.GetDamage(m));
//                if (minion != null)
//                {
//                    Q.CastOnUnit(minion);
//                }
//            }

//            if (useE)
//            {
//                Casting.Farm(E, MinionManager.GetMinions(E.Range + E.Width), minE);
//            }
//        }

//        protected override void JungleClear()
//        {
//            if (!ResourceManager.Check("jungle-clear"))
//            {
//                return;
//            }
//            var useQ = Menu.Item(Menu.Name + ".jungle-clear.q").GetValue<bool>() && Q.IsReady();
//            var useE = Menu.Item(Menu.Name + ".jungle-clear.e").GetValue<bool>() && E.IsReady();

//            if (useQ)
//            {
//                var minion =
//                    MinionManager.GetMinions(
//                        Q.ChargedMaxRange, MinionTypes.All, MinionTeam.Neutral, MinionOrderTypes.MaxHealth)
//                        .FirstOrDefault();
//                if (minion != null)
//                {
//                    Q.CastOnUnit(minion);
//                }
//            }

//            if (useE)
//            {
//                Casting.Farm(
//                    E,
//                    MinionManager.GetMinions(
//                        E.Range + E.Width, MinionTypes.All, MinionTeam.Neutral, MinionOrderTypes.MaxHealth), 1);
//            }
//        }

//        protected override void Flee()
//        {
//            if (Menu.Item(Menu.Name + ".flee.w").GetValue<bool>() && W.IsReady())
//            {
//                W.Cast();
//            }
//            if (Menu.Item(Menu.Name + ".flee.e").GetValue<bool>() && E.IsReady())
//            {
//                ELogic(
//                    GameObjects.EnemyHeroes.Where(e => e.IsValidTarget(E.Range))
//                        .OrderBy(e => e.Position.Distance(Player.Position))
//                        .FirstOrDefault(), HitChance.High);
//            }
//        }

//        protected override void Killsteal()
//        {
//            if (Menu.Item(Menu.Name + ".killsteal.q").GetValue<bool>() && Q.IsReady())
//            {
//                var killable =
//                    GameObjects.EnemyHeroes.FirstOrDefault(
//                        e => e.IsValidTarget(Q.Range) && Q.GetDamage(e) * 0.95f > e.Health);
//                if (killable != null)
//                {
//                    Q.CastOnUnit(killable);
//                }
//            }
//        }
//    }
//}