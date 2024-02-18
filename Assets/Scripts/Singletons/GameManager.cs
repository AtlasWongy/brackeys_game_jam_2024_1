using System;
using System.Collections;
using System.Collections.Generic;
using NPC;
using Options;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Singletons
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerPrefab;
        [SerializeField] private Enemy.Enemy enemyPrefab;
        [SerializeField] private NpcInteraction npcPrefab;
        [SerializeField] private Merchant.Merchant merchantPrefab;
        [SerializeField] private GameObject dialogPrompt;
        
        public Button[] buttons;
        public static GameManager Instance { get; private set; }
        public bool playerWinsEncounter;
        public static Action OnDestroySignal;
        public OptionEvent _optionEvent;
        private PlayerMovement _player;
        private string _eventType;
        private NpcInteraction _npc;
        private Merchant.Merchant _merchant;
        private Enemy.Enemy enemy;

        private bool loadedOnce = false;

        private List<string> UsedEventNames = new List<string>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (FindObjectOfType<PlayerMovement>() == null)
            {
                LoadPlayer();
                if (SceneManager.GetActiveScene().name == "Repeat")
                {
                    if (_optionEvent.eventType == "Combat")
                    {
                        LoadEnemy();
                        loadedOnce = true;
                    }
                    else if (_optionEvent.eventType == "Random")
                    {
                        LoadNpc();
                        loadedOnce = true;
                    }
                    else if (_optionEvent.eventType == "Shop")
                    {
                        LoadMerchant();
                        loadedOnce = true;
                    }
                }
            }

            if (SceneManager.GetActiveScene().name == "Repeat")
            {
                if ((enemy == null || _npc == null || _merchant == null) && loadedOnce)
                {
                    ReloadButtons();
                    loadedOnce = false;
                }
            }
        }

        public void LoadPlayer()
        {
            _player = Instantiate(playerPrefab, new Vector3(-3.5f, 0.6f), Quaternion.identity);
        }

        public void LoadEnemy()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                enemy = Instantiate(enemyPrefab, new Vector3(0.038f, 0.77f), Quaternion.identity);
                enemy.GetComponent<SpriteRenderer>().sprite = _optionEvent.sprite;
                enemy.GetComponent<Animator>().runtimeAnimatorController = _optionEvent.runTimeAnimatorController;
                enemy.GetComponent<Animator>().Play("Goblin_Idle");

            }
        }

        public void LoadNpc()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _npc = Instantiate(npcPrefab, new Vector3(0.038f, 0.77f), Quaternion.identity);
                _npc.GetComponent<SpriteRenderer>().sprite = _optionEvent.sprite;
            }
        }

        private void LoadMerchant()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _merchant = Instantiate(merchantPrefab, new Vector3(0.038f, 0.77f), Quaternion.identity);
                _merchant.GetComponent<SpriteRenderer>().sprite = _optionEvent.sprite;
            }
        }

        public void HandleEventOutcome(OptionEvent optionEvent)
        {
            DisableButtons();
            
            Debug.LogFormat("Option Selected: {0}. Change in stats: {1} health, {2} wits, {3} guts, {4} heart, {5} good, {6} evil. You gained: {7} gold and {8} items.", optionEvent.optionName, optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil, optionEvent.rewardsObtained.Item2, optionEvent.rewardsObtained.Item1);

            PlayerClass.PlayerInstance.AdjustStats(optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil);
            PlayerClass.PlayerInstance.AdjustGold(optionEvent.rewardsObtained.Item2);
            _optionEvent = optionEvent;

            // if (_optionEvent.eventType == "Combat")
            // {
            //     playerWinsEncounter = playerWins;
            // }
        }

        // Method to update UI stat text
        public void UpdateUIStatText()
        {
            Debug.LogFormat("Current Stats: {0} health, {1} wits, {2} guts, {3} heart, {4} good, {5} evil.", Player.PlayerClass.PlayerInstance.GetStats().Health, Player.PlayerClass.PlayerInstance.GetStats().Wits, Player.PlayerClass.PlayerInstance.GetStats().Guts, Player.PlayerClass.PlayerInstance.GetStats().Heart, Player.PlayerClass.PlayerInstance.GetStats().Good, Player.PlayerClass.PlayerInstance.GetStats().Evil);
            // Call UIManager method to update UI stat text
            UIStatsManager.UIStatsInstance.updateText("HP", "HP: " + Player.PlayerClass.PlayerInstance.GetStats().Health.ToString());
            UIStatsManager.UIStatsInstance.updateText("Brain", "Brain: " +Player.PlayerClass.PlayerInstance.GetStats().Wits.ToString());
            UIStatsManager.UIStatsInstance.updateText("Guts", "Guts: " +Player.PlayerClass.PlayerInstance.GetStats().Guts.ToString());
            UIStatsManager.UIStatsInstance.updateText("Heart", "Heart: " +Player.PlayerClass.PlayerInstance.GetStats().Heart.ToString());
            UIStatsManager.UIStatsInstance.updateText("Gold Count", "Gold Count: " +Player.PlayerClass.PlayerInstance.GetGold().ToString());

        }
    
        // Method to check the win condition
        private bool CheckWinCondition()
        {
            return Player.PlayerClass.PlayerInstance.GetStats().Good > Player.PlayerClass.PlayerInstance.GetStats().Evil;
        }
    
        public bool ResolvePlayerRoll(OptionEvent optionEvent){
            return(Player.PlayerClass.PlayerInstance.DieRoll(optionEvent));
            //return true;
        }

        private void DisableButtons()
        {
            foreach (var button in buttons)
            {
                // DontDestroyOnLoad(button);
                button.interactable = false;
            }
        }

        public void EnableButtons()
        {
            foreach (var button in buttons)
            {
                button.interactable = true;
            }
        }

        public void HandleDialog()
        {
            dialogPrompt.SetActive(true);
            if (GameManager.Instance._optionEvent.eventOutCome == OptionEvent.EventOutCome.Success)
            {
                dialogPrompt.GetComponentInChildren<TextMeshProUGUI>().text = _optionEvent.dialog[0];
            }
            else
            {
                dialogPrompt.GetComponentInChildren<TextMeshProUGUI>().text = _optionEvent.dialog[1];
            }
            
            StartCoroutine(CloseTheDialogPrompt());
        }

        IEnumerator CloseTheDialogPrompt()
        {
            yield return new WaitForSeconds(2.5f);
            dialogPrompt.SetActive(false);
            Instance.EnableButtons();
            Instance.UpdateUIStatText();
            OnDestroySignal.Invoke();
        }


        public List<string> TrackEventOptions(String eventName)
        {
            if (eventName != null)
            {
                UsedEventNames.Add(eventName);
            }
            
            return UsedEventNames;
        }

        // Method to destroy the current game object and reload it
        public void ReloadButtons()
        {
            foreach (var button in buttons)
            {
                // Destroy the current instance
                if (button != null)
                {
                    Destroy(button);
                }

                // Instantiate a new instance of the prefab
                Instantiate(button, transform.position, transform.rotation);
            }
         
        }

    }
}