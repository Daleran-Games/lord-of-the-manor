using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DaleranGames.UI
{
    public class StatsPanelView : PanelView
    {

        //BaseShip playerShip;

        UITextBox shipName;
        UIStatBar hp;
        UITextBox damage;
        UITextBox accuracy;
        UITextBox speed;
        UITextBox maneuverability;


        // Use this for initialization
        protected override void Awake()
        {
            base.Awake();
            //playerShip = GameObject.FindGameObjectWithTag("Player").GetRequiredComponent<BaseShip>();
        }

        private void Start()
        {
            EnableImage(false);
            
            /*
            shipName = UITextBox.CreateTextBox(transform, playerShip.ShipName, false);
            hp = UIStatBar.CreateStatBar(transform, "HP", playerShip.HitPoints, playerShip.MaxHitPoints, UIManager.Instance.CurrentUIStyle.StatIncreaseColor);
            damage = UITextBox.CreateTextBox(transform, "DMG: " + playerShip.MinDamage + "-" + playerShip.MaxDamage, false);
            accuracy = UITextBox.CreateTextBox(transform, "ACC: " + playerShip.Accuracy, false);
            speed = UITextBox.CreateTextBox(transform, "SPD: " + playerShip.Speed, false);
            maneuverability = UITextBox.CreateTextBox(transform, "MNV: " + playerShip.Maneuverability, false);
            
   
            playerShip.ShipNameChanged += OnNameChange;
            playerShip.HitPointsChanged += OnHPChange;
            playerShip.MaxHitPointsChanged += OnHPChange;
            playerShip.MinDamageChanged += OnDamageChange;
            playerShip.MaxDamageChanged += OnDamageChange;
            playerShip.AccuracyChanged += OnAccuracyChange;
            playerShip.SpeedChanged += OnSpeedChange;
            playerShip.ManeuverabilityChanged += OnManeuverabilityChange;
            */

        }

        private void OnDestroy()
        {
            /*
            playerShip.ShipNameChanged -= OnNameChange;
            playerShip.HitPointsChanged -= OnHPChange;
            playerShip.MaxHitPointsChanged -= OnHPChange;
            playerShip.MinDamageChanged -= OnDamageChange;
            playerShip.MaxDamageChanged -= OnDamageChange;
            playerShip.AccuracyChanged -= OnAccuracyChange;
            playerShip.SpeedChanged -= OnSpeedChange;
            playerShip.ManeuverabilityChanged -= OnManeuverabilityChange;
            */
        }

        public void OnNameChange(string newName)
        {
            //shipName.SetText(playerShip.ShipName);
        }

        public void OnHPChange(int i)
        {
           // hp.ChangeValue(playerShip.HitPoints);
            //hp.ChangeMaxValue(playerShip.MaxHitPoints);
            //hp.ChangeValue(playerShip.HitPoints);
        }

        public void OnDamageChange(int i)
        {
            //damage.SetText("DMG: " + playerShip.MinDamage + "-" + playerShip.MaxDamage);
        }

        public void OnAccuracyChange(int i)
        {
            //accuracy.SetText("ACC: " + playerShip.Accuracy);
        }

        public void OnSpeedChange(int i)
        {
            //speed.SetText("SPD: " + playerShip.Speed);
        }

        public void OnManeuverabilityChange(int i)
        {
            //maneuverability.SetText("MNV: " + playerShip.Maneuverability);
        }


    } 
}
