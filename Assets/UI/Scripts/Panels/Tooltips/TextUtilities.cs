using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DaleranGames.TBSFramework;
using UnityEngine.EventSystems;
using System;

namespace DaleranGames.UI
{
    public static class TextUtilities
    {
        public static string ColorBasedOnNumber(string text, int number, bool withPlus)
        {
            if (number > 0)
            {
                if (withPlus)
                    return "<style=\"PosColor\">+" + text + "</style>";
                else
                    return "<style=\"PosColor\">" + text + "</style>";
            }
            else if (number == 0)
                return text;
            else
                return "<style=\"NegColor\">" + text + "</style>";
        }

        public static string ToPositiveColor(this string text)
        {
            return "<style=\"PosColor\">" + text + "</style>";
        }

        public static string ToNegativeColor(this string text)
        {
            return "<style=\"NegColor\">" + text + "</style>";
        }

        public static string ToTitleStyle(this string text)
        {
            return "<style=\"Title\">" + text + "</style>";
        }

        public static string ToHeaderStyle(this string text)
        {
            return "<style=\"Header\">" + text + "</style>";
        }

        public static string ToFootnoteStyle(this string text)
        {
            return "<style=\"Footnote\">" + text + "</style>";
        }

        public static string GetGoodTypeDescription (GoodType type)
        {
            switch (type)
            {
                case GoodType.Food:
                    return "Each clan member requires one food every turn to live. If they do not recieve any food, there is a 50% chance they will die.";
                case GoodType.Wood:
                    return "Wood is used in construction and heating dwellings in the winter. Larger dwellings require more wood. If a dwelling does not recieve enough wood, there is a 25% chance clan members could die.";
                case GoodType.Stone:
                    return "Stone is used in advanced construction projects. It is expensive and requires a lot of labor to quarry and move.";
                case GoodType.Gold:
                    return "Gold is used for buying land and trading goods on the market.";
                case GoodType.Population:
                    return "Your clan's populations is limited by the size of their dwelling.";
                case GoodType.Labor:
                    return "Labor represents the amount work you can reasonably do per season. An average laborer produces 6 labor a season, one for each day of the week excluding Sunday.";
                default:
                    return "";
            }
        }

        public static string GetGoodTypeIcon(GoodType type)
        {
            switch (type)
            {
                case GoodType.Food:
                    return "<sprite name=\"UIAtlas_Icon_Food\">";
                case GoodType.Wood:
                    return "<sprite name=\"UIAtlas_Icon_Wood\">";
                case GoodType.Stone:
                    return "<sprite name=\"UIAtlas_Icon_Stone\">";
                case GoodType.Gold:
                    return "<sprite name=\"UIAtlas_Icon_Coins\">";
                case GoodType.Population:
                    return "<sprite name=\"UIAtlas_Icon_House\">";
                case GoodType.Labor:
                    return "<sprite name=\"UIAtlas_Icon_Hammer\">";
                default:
                    return "";
            }
        }
    }
}