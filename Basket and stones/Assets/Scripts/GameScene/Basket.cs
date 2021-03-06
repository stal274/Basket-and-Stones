using System;
using System.Collections;
using MainScene;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameScene
{
    public class Basket : MonoBehaviour
    {
        private readonly byte _difficulty = ChangeDifficultyLevel.Difficulty;
        [SerializeField] private Text currentAmountOfStonesPanel, stonesToWinPanel;
        [SerializeField] private int currentAmountOfStones, stonesToWin;
        public static Basket Instance;

        public int StonesToWin
        {
            get => stonesToWin;
            private set => stonesToWin = value;
        }


        public int CurrentAmountOfStones
        {
            get => currentAmountOfStones;
            set => currentAmountOfStones = value;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Error");
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            int minStonesInBasket, maxStonesInBasket;
            switch (_difficulty)
            {
                case 0:
                    minStonesInBasket = 10;
                    maxStonesInBasket = 26;
                    break;
                case 1:
                    minStonesInBasket = 26;
                    maxStonesInBasket = 40;
                    break;
                case 2:
                    minStonesInBasket = 41;
                    maxStonesInBasket = 56;
                    break;
                default:
                    minStonesInBasket = 26;
                    maxStonesInBasket = 40;
                    break;
            }

            CurrentAmountOfStones = Random.Range(minStonesInBasket, maxStonesInBasket);

            var minStonesToWin = CurrentAmountOfStones * 1.3f;
            var maxStonesToWin = CurrentAmountOfStones * 2.2f;
            StonesToWin = Convert.ToInt32(Random.Range(minStonesToWin, maxStonesToWin));
            stonesToWinPanel.text = Convert.ToString(StonesToWin);

            StartCoroutine(StonesInBasketEditing(1f, 1.2f));
        }


        public int Calculate(char action, int value)
        {
            var expectedAmount = CurrentAmountOfStones;
            switch (action)
            {
                case '*':
                    expectedAmount *= value;
                    break;
                case '+':
                    expectedAmount += value;
                    break;
                case '-':
                    expectedAmount -= value;
                    break;
                case '/':
                    expectedAmount /= value;
                    break;
            }

            return expectedAmount;
        }

        public int Calculate(GameButton button)
        {
            return Calculate(button.Action, button.Value);
        }

        public void ApplyChangesToBasket(GameButton button)
        {
            CurrentAmountOfStones = Calculate(button.Action, button.Value);
            StartCoroutine(StonesInBasketEditing(1f, 1.2f));
        }


        public IEnumerator StonesInBasketEditing(float startFloat, float endFloat)
        {
            for (var j = int.Parse(currentAmountOfStonesPanel.text);;
            )
            {
                yield return new WaitForSeconds((float) 1 / (5 * Math.Abs(j - CurrentAmountOfStones)));
                if (j > CurrentAmountOfStones) j--;
                if (j < CurrentAmountOfStones) j++;
                StartCoroutine(BasketAnimation(startFloat, endFloat));
                currentAmountOfStonesPanel.text = Convert.ToString(j);
                if (j != CurrentAmountOfStones) continue;
                if (StonesToWin != CurrentAmountOfStones) break;
                EventAggregator.EventAggregator.GameIsOver.Publish(this);
                break;
            }

            StartCoroutine(BasketAnimation(endFloat, startFloat));
        }

        private IEnumerator BasketAnimation(float startFloat, float endFloat)
        {
            for (var i = startFloat; Math.Abs(i - endFloat) > 0.01f; i += startFloat < endFloat ? 0.05f : -0.05f)
            {
                yield return new WaitForSeconds(1 / 20f);
                currentAmountOfStonesPanel.transform.localScale = new Vector3(i, i);
            }
        }
    }
}