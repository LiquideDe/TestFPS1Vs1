using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FPS
{
    public class TimeResultHolder
    {
        private List<float> _timeResults = new List<float>();

        public TimeResultHolder()
        {
            if (File.Exists($"{Application.streamingAssetsPath}/Saves/Best.json"))
            {
                string results;
                results = File.ReadAllText($"{Application.streamingAssetsPath}/Saves/Best.json");
                BestResultsSaveLoad bestResultsSaveLoad = JsonUtility.FromJson<BestResultsSaveLoad>(results);
                FirstPlace = bestResultsSaveLoad.firstResult;
                SecondPlace = bestResultsSaveLoad.secondResult;
                ThirdPlace = bestResultsSaveLoad.thirdResult;

                if (FirstPlace != 0)
                    _timeResults.Add(FirstPlace);
                if (SecondPlace != 0)
                    _timeResults.Add(SecondPlace);
                if (ThirdPlace != 0)
                    _timeResults.Add(ThirdPlace);
            }
            else
            {
                FirstPlace = 0;
                SecondPlace = 0;
                ThirdPlace = 0;
            }
        }

        public float FirstPlace { get; private set; }
        public float SecondPlace { get; private set; }
        public float ThirdPlace { get; private set; }

        public void AddNewResults(List<float> newTimeResults)
        {
            foreach (var item in newTimeResults)
            {
                if (TryAddResult(item))
                    _timeResults.Add(item);
            }

            List<float> sortedList = _timeResults.OrderBy(f => f).ToList();
            foreach (var item in sortedList)
                Debug.Log(item);

            BestResultsSaveLoad bestResults = new BestResultsSaveLoad();
            if (sortedList.Count >= 1)
                bestResults.firstResult = sortedList[0];
            if (sortedList.Count >= 2)
                bestResults.secondResult = sortedList[1];
            if (sortedList.Count >= 3)
                bestResults.thirdResult = sortedList[2];

            List<string> data = new List<string>
        {
            JsonUtility.ToJson(bestResults)
        };
            if (Directory.Exists($"{Application.streamingAssetsPath}/Saves") == false)
                Directory.CreateDirectory($"{Application.streamingAssetsPath}/Saves");

            File.WriteAllLines($"{Application.streamingAssetsPath}/Saves/Best.json", data);
        }

        private bool TryAddResult(float result)
        {
            foreach (var item in _timeResults)
                if (item == result)
                    return false;

            return true;
        }
    }
}


