using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

namespace DaleranGames.UI
{
    public class DialogBox : Singleton<DialogBox>
    {
        protected DialogBox() { }

        [SerializeField] TextMeshProUGUI text;

        private void Start()
        {
            this.text.text = TutorialText();
        }

        public void Show(string text)
        {
            gameObject.SetActive(true);
            this.text.text = text;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        string TutorialText()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Lord of the Manor".ToTitleStyle());
            sb.AppendLine("Concept".ToHeaderStyle());
            sb.AppendLine("Lord of the Manor is a new strategy city builder in the vein of Stronghold and the Lords of the Realm. The key mechanic is that instead of controlling everything directly, you will manage a complex network of fuedal allegiances and vassals to build your power. This prototype demonstrates some of the basic economic systems and builds a solid foundation for future gameplay.");
            sb.AppendLine();
            sb.AppendLine("During the prototype, you will play a wealthy farmer that owns a freehold fief. While you have no vassals, you can build and upgrade your homstead, farm the land, chop wood, quarry, and trade resources.");
            sb.AppendLine();
            sb.AppendLine("What to do First".ToHeaderStyle());
            sb.AppendLine("Your first step should be to site your dwelling somewhere in the red borders that mark your territory. Once established, the next step is to place your first farm plots. Farms in LotM work on a two field crop rotation system. For every year they grow, they must lay fallow for another year. You can also place some logging sites to build up wood to heat your home in the winter. Keep an eye on your labor, the basic resource represented by a hammer that allocates how much you can do in a turn.");
            sb.AppendLine();
            sb.AppendLine("You will find a lot more labor is needed during planting (Spring) and harvest (Fall) season so you might want to set your logging camps to only work during the Summer and Winter. To learn more about different concepts, read the tooltips on the UI elements.");
            sb.AppendLine();

            return sb.ToString();
        }

    }
}

