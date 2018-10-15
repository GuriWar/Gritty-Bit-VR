using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class SaveScoreToFile : MonoBehaviour {
	public struct Score
	{
		public Score(string x, string name)
		{
			scoreString = x;
			scoreValue = int.Parse(scoreString);
            playerName = name;
		}

		public Score (int x, string name)
		{
			scoreValue =x;
			scoreString = scoreValue.ToString();
            playerName = name;
		}
	
		public int GetScoreValue()
		{
			return scoreValue;
		}

		public string GetScoreString()
		{
			return scoreString;
		}

	    int scoreValue;
		string scoreString;
        public string playerName;
	}
	//needs to read, organize and then write all scores
	public List <Score> Scores = new List<Score>();
    void Awake()
    {
        GetComponent<dreamloLeaderBoard>().LoadScores();

    }

    void Start()
	{
        ReadScore();
		OrganizeScores ();
    }

    void ReadScore()
	{
        StreamReader reader;
        if (File.Exists(Application.dataPath + "Scores.txt"))
        {
            reader = new StreamReader(Application.dataPath + "Scores.txt");
        }
        else
        {
            System.IO.File.WriteAllText(Application.dataPath + "Scores.txt", "");
            reader = new StreamReader(Application.dataPath + "Scores.txt");

        }
       
        string s = reader.ReadLine();
        while (s!= null)
		{
			char[] delimiter = {' '};
			string[] fields = s.Split(delimiter);
			Score temp = new Score (fields[1], fields[2]);
			Scores.Add(temp);
			s = reader.ReadLine();
		}
        reader.Close();
	}

	void OrganizeScores()
	{
		//need to loop through all the scores and make sure they're all in the right spot
		int max = 0;
        string tempName = "";
		float loopIteration = Scores.Count;
		List <Score> tempScores = new List<Score>();
		for (int i = 0; i < Scores.Count; i++) 
		{
			tempScores.Add( new Score(Scores [i].GetScoreValue(), Scores[i].playerName));
		}
		Scores.Clear ();
		for (int i = 0; i <loopIteration; i++) 
		{
			for (int j = 0; j < tempScores.Count; j++) 
			{
				if (tempScores [j].GetScoreValue() >= max) 
				{
					max = tempScores [j].GetScoreValue();
                    tempName = tempScores[j].playerName;
				}
			}
			Score temp = new Score (max, tempName);
			tempScores.Remove (temp);
			Scores.Add(temp);
			max =0;
		}
	}

	public void AddScore(int score, string name)
	{
		Score temp = new Score (score, name);
		Scores.Add (temp);
        GetComponent<dreamloLeaderBoard>().AddScore(name, score);
    }

   
	public void SaveScore()
	{
		OrganizeScores();
        StreamWriter writer;
        writer = new StreamWriter(Application.dataPath + "Scores.txt");

       
        for (int i = 0; i < Scores.Count; i++) 
		{
			int num = i + 1;
			writer.WriteLine (num +". " + Scores[i].GetScoreString() + " " + Scores[i].playerName);
		}
		writer.Close();
	}
}
