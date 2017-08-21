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

        public static string OverlapCharacters (this string text)
        {
            return "<cspace=-3em>" + text + "</cspace>";
        }

        public static string ToSprite (this string text)
        {
            return "<sprite name=\"" + text + "\">";
        }

    }
}