#region License

/*
 Copyright 2014 - 2015 Nikita Bernthaler
 IChampion.cs is part of SFXMissFortune.

 SFXMissFortune is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 SFXMissFortune is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with SFXMissFortune. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion License

#region

using System.Collections.Generic;
using LeagueSharp.Common;
using Orbwalking = SFXMissFortune.SFXTargetSelector.Orbwalking;
using Spell = SFXMissFortune.Wrappers.Spell;

#endregion

namespace SFXMissFortune.Interfaces
{
    public interface IChampion
    {
        Menu SFXMenu { get; }
        Menu Menu { get; }
        Orbwalking.Orbwalker Orbwalker { get; }
        List<Spell> Spells { get; }
        void Combo();
        void Harass();
        void LaneClear();
        void JungleClear();
        void Flee();
        void Killsteal();
    }
}