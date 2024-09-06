using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RankData {
    public string name;
    public int score;
    public int deliver;
}

public class Ranking
{
    public static List<RankData> data = new();

    public static void Load() {
        data.Clear();

        int rankCount = PlayerPrefs.GetInt("RankCount");
        string name;
        int score, deliver;
        
        for (int i = 0; i < rankCount; i++) {
            name = PlayerPrefs.GetString("RankName" + i);
            score = PlayerPrefs.GetInt("RankScore" + i);
            deliver = PlayerPrefs.GetInt("RankDeliver" + i);

            data.Add(new RankData(){
                name = name,
                score = score,
                deliver = deliver,
            });
        }
    }

    public static void Store() {
        int rankCount = PlayerPrefs.GetInt("RankCount");
        if (rankCount != data.Count) {
            PlayerPrefs.DeleteAll();
        }

        PlayerPrefs.SetInt("RankCount", data.Count);

        for (int i = 0; i < data.Count; i++) {
            PlayerPrefs.SetString("RankName" + i, data[i].name);
            PlayerPrefs.SetInt("RankScore" + i, data[i].score);
            PlayerPrefs.SetInt("RankDeliver" + i, data[i].deliver);
        }
    }
}
