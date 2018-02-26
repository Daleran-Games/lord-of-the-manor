using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using UnityEngine.SceneManagement;
using DaleranGames.UI;

namespace DaleranGames
{
    public class GameEnd : MonoBehaviour
    {
        [SerializeField]
        GroupManager groups;
        GroupGoods goods;

        // Use this for initialization
        void Start()
        {
            goods = groups.PlayerGroup.Goods;
            goods.GoodChanged += OnPopulationChange;
        }

        private void OnDestroy()
        {
            goods.GoodChanged -= OnPopulationChange;
        }

        void OnPopulationChange (GoodsCollection goods, GoodType type)
        {
            if (type == GoodType.Population && goods[type] < 1)
                ShowEndGameDialog();
        }

        void ShowEndGameDialog()
        {
            DialogBox.Instance.DialogBoxClosed += RestartButtonPressed;
            DialogBox.Instance.Show("Everyone in your realm has died.", "Exit");
        }

        void RestartButtonPressed()
        {
            DialogBox.Instance.DialogBoxClosed -= RestartButtonPressed;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
        }

    }

}
