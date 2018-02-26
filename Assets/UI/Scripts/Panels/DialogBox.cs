using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class DialogBox : Singleton<DialogBox>
    {
        protected DialogBox() { }

        [SerializeField]
        TextMeshProUGUI text;
        [SerializeField]
        TextMeshProUGUI confimText;
        [SerializeField]
        GameObject dialog;

        public event System.Action DialogBoxClosed;

        private void Start()
        {
            confimText.text = "Confirm";
            this.text.text = TutorialText();
        }

        public void Show(string text, string buttonText)
        {
            dialog.SetActive(true);
            this.text.text = text;
            confimText.text = buttonText;
        }

        public void Hide()
        {
            DialogBoxClosed?.Invoke();
            dialog.SetActive(false);
        }

        string TutorialText()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Lord of the Manor".ToTitleStyle());
            sb.AppendLine("How to Play".ToHeaderStyle());
            sb.AppendLine("- Start by placing a dwelling somewhere within your borders. (The red tiles) A dwelling is required to increase your "+StatType.MaxPopulation.Icon+" Population Cap.");
            sb.AppendLine("- Everything costs "+GoodType.Labor.Icon+" Labor to do per turn.");
            sb.AppendLine("- Place a few farms to gain " + GoodType.Food.Icon + " Food during the harvest months. Your people need food every turn or they will die.");
            sb.AppendLine("- Place a logging camp on a forest to gain " + GoodType.Wood.Icon + " Wood to survive the winter. Logging camps will also clear the land for agricultural use.");
            sb.AppendLine("- Use " + GoodType.Gold.Icon + " Gold to but and sell resources and buy new land.");
            sb.AppendLine();
            sb.AppendLine("Tips".ToHeaderStyle());
            sb.AppendLine("- Farms in LotM work on a two field crop rotation system. For every year they grow, they must lay fallow for another year.");
            sb.AppendLine("- You will find a lot more labor is needed during planting (Spring) and harvest (Fall) season so you might want to set your logging camps to only work during the Summer and Winter.");
            sb.AppendLine("- Mouse over all the UI elements to learn more about what each game concept does.");
            sb.AppendLine();

            return sb.ToString();
        }

    }
}

