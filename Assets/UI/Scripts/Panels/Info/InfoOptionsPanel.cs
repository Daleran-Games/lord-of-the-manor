using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;
using System;

namespace DaleranGames.UI
{
    public class InfoOptionsPanel : MonoBehaviour
    {
        [SerializeField] GameObject infoObjectPanel;
        [Header("Season Toggles")]
        [SerializeField] Toggle springToggle;
        [SerializeField] Toggle summerToggle;
        [SerializeField] Toggle fallToggle;
        [SerializeField] Toggle winterToggle;
        [Header("Work Buttons")]
        [SerializeField] Button workButton;
        ScriptTooltip workTooltip;
        [SerializeField] Image workIcon;
        [SerializeField] Sprite workSprite;
        [SerializeField] Sprite sleepSprite;
        [Header("Cancel Buttons")]
        [SerializeField] Button cancelButton;
        ScriptTooltip cancelTooltip;
        [SerializeField] Image cancelIcon;
        [SerializeField] Sprite razeSprite;
        [SerializeField] Sprite cancelSprite;
        [Header("Other Buttons")]
        [SerializeField] Button upgradeButton;
        ScriptTooltip upgradeTooltip;
        [SerializeField] Button buyLandButton;
        ScriptTooltip buyLandTooltip;


        // Use this for initialization
        void Start()
        {
            InfoPanel.Instance.TileSelected += OnTileSelected;
            InfoPanel.Instance.TileDeselected += OnTileDeselected;

            infoObjectPanel.SetActive(false);
            SetAllButtonsState(false);

            workTooltip = workButton.GetComponent<ScriptTooltip>();
            cancelTooltip = cancelButton.GetComponent<ScriptTooltip>();
            upgradeTooltip = upgradeButton.GetComponent<ScriptTooltip>();
            buyLandTooltip = buyLandButton.GetComponent<ScriptTooltip>();
        }

        void OnDestroy()
        {
            InfoPanel.Instance.TileSelected -= OnTileSelected;
            InfoPanel.Instance.TileDeselected -= OnTileDeselected;
        }

        void OnTileSelected(HexTile tile)
        {
            if (tile == null)
            {
                infoObjectPanel.SetActive(false);
            }      
            else
            {
                if (tile.Owner == Group.Null || tile.Owner == null)
                {
                    infoObjectPanel.SetActive(true);
                    buyLandButton.gameObject.SetActive(true);
                    BuyLandCommand newBuy = new BuyLandCommand();
                    buyLandTooltip.Text = newBuy.GetInfo(tile, GroupManager.Instance.PlayerGroup);

                    if (!newBuy.IsValidCommand(tile, GroupManager.Instance.PlayerGroup))
                        buyLandButton.interactable = false;
                    else
                        buyLandButton.interactable = true;

                } else if (tile.Feature != FeatureType.Null || tile.Feature != null)
                {
                    bool found = false;

                    IWorkable workable = tile.Feature as IWorkable;
                    if (workable != null)
                    {
                        workButton.gameObject.SetActive(true);

                        if (tile.Work.Paused)
                            workIcon.sprite = workSprite;
                        else
                            workIcon.sprite = sleepSprite;

                        WorkCommand newWork = new WorkCommand();
                        workTooltip.Text = newWork.GetInfo(tile, GroupManager.Instance.PlayerGroup);

                        if (!newWork.IsValidCommand(tile, GroupManager.Instance.PlayerGroup))
                            workButton.interactable = false;
                        else
                            workButton.interactable = true;

                        found = true;
                    }

                    ICancelable cancelable = tile.Feature as ICancelable;
                    if (cancelable != null)
                    {
                        cancelButton.gameObject.SetActive(true);

                        CancelCommand newCancel = new CancelCommand();
                        cancelTooltip.Text = newCancel.GetInfo(tile, GroupManager.Instance.PlayerGroup);

                        if (!newCancel.IsValidCommand(tile, GroupManager.Instance.PlayerGroup))
                            cancelButton.interactable = false;
                        else
                            cancelButton.interactable = true;

                        found = true;
                    }

                    IUpgradeable upgradeable = tile.Feature as IUpgradeable;
                    if (upgradeable != null)
                    {
                        upgradeButton.gameObject.SetActive(true);

                        UpgradeCommand newUpgrade = new UpgradeCommand();
                        upgradeTooltip.Text = newUpgrade.GetInfo(tile, GroupManager.Instance.PlayerGroup);

                        if (!newUpgrade.IsValidCommand(tile, GroupManager.Instance.PlayerGroup))
                            upgradeButton.interactable = false;
                        else
                            upgradeButton.interactable = true;

                        found = true;
                    }

                    ISeasonable seasonable = tile.Feature as ISeasonable;
                    if (seasonable != null && tile.Owner == GroupManager.Instance.PlayerGroup)
                    {
                        springToggle.gameObject.SetActive(true);
                        summerToggle.gameObject.SetActive(true);
                        fallToggle.gameObject.SetActive(true);
                        winterToggle.gameObject.SetActive(true);

                        springToggle.isOn = !tile.Work.WorkSpring;
                        summerToggle.isOn = !tile.Work.WorkSummer;
                        fallToggle.isOn = !tile.Work.WorkFall;
                        winterToggle.isOn = !tile.Work.WorkWinter;

                        springToggle.onValueChanged.AddListener(ToggleSpring);
                        summerToggle.onValueChanged.AddListener(ToggleSummer);
                        fallToggle.onValueChanged.AddListener(ToggleFall);
                        winterToggle.onValueChanged.AddListener(ToggleWinter);

                    }

                    if (found)
                        infoObjectPanel.SetActive(true);

                }
            }
        }

        void OnTileDeselected(HexTile tile)
        {
            springToggle.onValueChanged.RemoveListener(ToggleSpring);
            summerToggle.onValueChanged.RemoveListener(ToggleSummer);
            fallToggle.onValueChanged.RemoveListener(ToggleFall);
            winterToggle.onValueChanged.RemoveListener(ToggleWinter);

            infoObjectPanel.SetActive(false);
            SetAllButtonsState(false);

        }

        void SetAllButtonsState(bool state)
        {
            springToggle.gameObject.SetActive(state);
            summerToggle.gameObject.SetActive(state);
            fallToggle.gameObject.SetActive(state);
            winterToggle.gameObject.SetActive(state);
            workButton.gameObject.SetActive(state);
            cancelButton.gameObject.SetActive(state);
            upgradeButton.gameObject.SetActive(state);
            buyLandButton.gameObject.SetActive(state);
        }

        void ToggleSpring(bool work)
        {
            WorkSeasonCommand newWork = new WorkSeasonCommand(Seasons.Spring, !work);
            newWork.PreformCommand(InfoPanel.Instance.SelectedTile, GroupManager.Instance.PlayerGroup);
            OnTileDeselected(InfoPanel.Instance.SelectedTile);
            OnTileSelected(InfoPanel.Instance.SelectedTile);
        }

        void ToggleSummer(bool work)
        {
            WorkSeasonCommand newWork = new WorkSeasonCommand(Seasons.Summer, !work);
            newWork.PreformCommand(InfoPanel.Instance.SelectedTile, GroupManager.Instance.PlayerGroup);
            OnTileDeselected(InfoPanel.Instance.SelectedTile);
            OnTileSelected(InfoPanel.Instance.SelectedTile);
        }

        void ToggleFall(bool work)
        {
            WorkSeasonCommand newWork = new WorkSeasonCommand(Seasons.Fall, !work);
            newWork.PreformCommand(InfoPanel.Instance.SelectedTile, GroupManager.Instance.PlayerGroup);
            OnTileDeselected(InfoPanel.Instance.SelectedTile);
            OnTileSelected(InfoPanel.Instance.SelectedTile);
        }

        void ToggleWinter(bool work)
        {
            WorkSeasonCommand newWork = new WorkSeasonCommand(Seasons.Winter, !work);
            newWork.PreformCommand(InfoPanel.Instance.SelectedTile, GroupManager.Instance.PlayerGroup);
            OnTileDeselected(InfoPanel.Instance.SelectedTile);
            OnTileSelected(InfoPanel.Instance.SelectedTile);
        }

        public void ToggleWork()
        {
            WorkCommand newWork = new WorkCommand();
            newWork.PreformCommand(InfoPanel.Instance.SelectedTile, GroupManager.Instance.PlayerGroup);
            OnTileDeselected(InfoPanel.Instance.SelectedTile);
            OnTileSelected(InfoPanel.Instance.SelectedTile);
        }

        public void UpgradeCommand()
        {
            UpgradeCommand newUpgrade = new UpgradeCommand();
            newUpgrade.PreformCommand(InfoPanel.Instance.SelectedTile, GroupManager.Instance.PlayerGroup);
            OnTileDeselected(InfoPanel.Instance.SelectedTile);
            OnTileSelected(InfoPanel.Instance.SelectedTile);

        }

        public void CancelCommand()
        {
            CancelCommand newCancel = new CancelCommand();
            newCancel.PreformCommand(InfoPanel.Instance.SelectedTile, GroupManager.Instance.PlayerGroup);
            OnTileDeselected(InfoPanel.Instance.SelectedTile);
            OnTileSelected(InfoPanel.Instance.SelectedTile);
        }

        public void BuyTile()
        {
            BuyLandCommand newBuy = new BuyLandCommand();
            newBuy.PreformCommand(InfoPanel.Instance.SelectedTile, GroupManager.Instance.PlayerGroup);
            OnTileDeselected(InfoPanel.Instance.SelectedTile);
            OnTileSelected(InfoPanel.Instance.SelectedTile);
        }

    }
}
