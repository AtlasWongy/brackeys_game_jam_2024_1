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

        private int? _PersonalityDemand = null;
        public int? PersonalityDemand
        {
            get { return _PersonalityDemand; }
            set { if (_PersonalityDemand == null) _PersonalityDemand = value; }
        }

        private int? _AttributeDemand = null;
        public int? AttributeDemand
        {
            get { return _AttributeDemand; }
            set { if (_AttributeDemand == null) _AttributeDemand = value; }
        }

        private int? _AchievementDemand = null;
        public int? AchievementDemand
        {
            get { return _AchievementDemand; }
            set { if (_AchievementDemand == null) _AchievementDemand = value; }
        }

        //TRACKERS FOR GAME DATA

        private int CombatSuccesses;
        private int CombatDefeats;
        private int EventSuccesses;
        private int EventDefeats;

        private int DemandsMet = 0;

        private int DoorCounter = 0;

        
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

        private List<string> UsedEventNames = new List<string>();
        private OptionEvent[] optionEventList;
        public string optionEventFolder;

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

            PrincessStartGenerate();
            optionEventList = Resources.LoadAll<OptionEvent>(optionEventFolder);

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
                    }
                    else if (_optionEvent.eventType == "Random")
                    {
                        LoadNpc();
                    }
                    else if (_optionEvent.eventType == "Shop")
                    {
                        LoadMerchant();
                    }
                }
            }

        }

        public void PrincessStartGenerate()
        {
            PersonalityDemand = UnityEngine.Random.Range(1, 3);
            AttributeDemand = UnityEngine.Random.Range(1, 3);
            AchievementDemand = UnityEngine.Random.Range(1, 3);

            //THIS SHOULD BE 11, 3 NOW FOR TESTING

            Debug.LogFormat("{0},{1},{2} are the demands", PersonalityDemand, AttributeDemand, AchievementDemand);


        }

        private bool PrincessPersonalityCheck(int? demand){
            switch(demand){
                case 1:
                    return(PlayerClass.PlayerInstance.IsPersonalityBrave());
                case 2:
                    return(PlayerClass.PlayerInstance.IsPersonalityCowardly());
                case 3:
                    return(PlayerClass.PlayerInstance.IsPersonalityCunning());
                case 4:
                    return(PlayerClass.PlayerInstance.IsPersonalityDull());
                case 5:
                    return(PlayerClass.PlayerInstance.IsPersonalityNoble());
                case 6:
                    return(PlayerClass.PlayerInstance.IsPersonalitySelfish());
                case 7:
                    return(PlayerClass.PlayerInstance.IsGoodHigherThanEvil());
                case 8:
                    return(!PlayerClass.PlayerInstance.IsGoodHigherThanEvil());
                default:
                    return false;

            }
        }

        private bool PrincessAttributeCheck(int? demand)
        {
            switch(demand)
            {
                case 1:
                    return (PlayerClass.PlayerInstance.GetGold()>10);
                case 2:
                    return (PlayerClass.PlayerInstance.GetGold()<5);
                
                default:
                    return false;
                
            }
        }

        private bool PrincessAchievementCheck(int? demand)
        {
            switch(demand)
            {
            case 1:
                return (CombatSuccesses > 3);

            case 2:
                return (CombatDefeats > 1);

            case 3:
                return (EventSuccesses > 3);

            case 4:
                return (EventDefeats > 1);

            default:
                    return false;
            }
        }

        private bool PrincessEndCheck()
        {
            DemandsMet = 0;
            //need 2 out of 3 right to win the game

            if(PrincessPersonalityCheck(PersonalityDemand)){
                DemandsMet += 1;
            }

            if(PrincessAttributeCheck(AttributeDemand)){
                DemandsMet += 1;
            }

            if(PrincessAchievementCheck(AchievementDemand)){
                DemandsMet += 1;
            }
            Debug.Log(DemandsMet.ToString());
            return(DemandsMet>=2);
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
            
            if(optionEvent.merchantType != null)
            {
                PlayerClass.PlayerInstance.HandleMerchant(optionEvent.merchantType);
            }

            _optionEvent = optionEvent;

            if (PlayerClass.PlayerInstance.GetStats().Health <= 0)
            {
                Debug.Log("You died! Game over.");
                //Show game over screen, if applicable
            }

            DoorCounter += 1;

            if (DoorCounter == 2) //NUMBER OF DOORS IN THE GAME
            {   
                //Are we visually displaying the princess somehow? Here's where we are doing it if we are.
                if(PrincessEndCheck())
                {
                    Debug.Log("The Princess accepts you! You win.");
                }
                else
                {
                    Debug.Log("The Princess rejected you! Game over.");
                }
            }

            

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

        public void LoadEvent(Button eventButton, OptionEvent optionEvent, TextMeshProUGUI _eventDesc)
        {
            eventButton.GetComponent<EventDisplay>().optionEvent = getOptionEvent();

            _eventDesc = eventButton.GetComponentInChildren<TextMeshProUGUI>();

            _eventDesc.text = optionEvent.description;
        }

        private OptionEvent getOptionEvent()
        {
            Debug.Log(optionEventList.Length);
            if (optionEventList.Length > 0)
            {
                //while (true)
                //{
                int randomIndex = UnityEngine.Random.Range(0, optionEventList.Length);

                OptionEvent assignedOptionEvent = optionEventList[randomIndex];
                //    Debug.Log(GameManager.Instance.TrackEventOptions(null));
                //    if (!GameManager.Instance.TrackEventOptions(null).Contains(assignedOptionEvent.optionName))
                //    {
                //GameManager.Instance.TrackEventOptions(assignedOptionEvent.optionName);
                return assignedOptionEvent;
                //}
                //    else
                //    {
                //        continue;
                //    };
                //}
            }
            else
            {
                return null;

            }
        }
    }
}

