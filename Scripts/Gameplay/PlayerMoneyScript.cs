using UnityEngine;
using System.Collections;

public class PlayerMoneyScript : MonoBehaviour {
	public int money;

	public void AddMoney(int amount)
	{
		money += amount;

	}

	public void DecreaseMoney(int amount)
	{
		money -= amount;
	}
}
