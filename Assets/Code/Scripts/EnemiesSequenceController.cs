using System;
using UnityEngine;

public class EnemiesSequenceController : MonoBehaviour
{
	[SerializeField]
	private Enemy[] enemies;

	[SerializeField]
	private Animator nextEnemyAnim;

	private int enemyIndex = 0;
	private int nextEnemyAnimHash = Animator.StringToHash("WinBattleAnim");
	private int winAnimHash = Animator.StringToHash("WinGameAnim");

	public Enemy GetNextEnemy()
	{
		if (enemyIndex > 0)
		{
			nextEnemyAnim.Play(nextEnemyAnimHash);
		}

		if (enemyIndex >= enemies.Length)
		{
			nextEnemyAnim.Play(winAnimHash);

			return null;
		}
		
		var enemy = enemies[enemyIndex];
		enemy.InitCopy();
		enemyIndex++;

		return enemy;
	}
}