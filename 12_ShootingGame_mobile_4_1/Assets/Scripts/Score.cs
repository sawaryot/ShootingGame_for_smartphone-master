using UnityEngine;

// IEnumerator(コルーチン利用)を使うために記述
//using System.Collections;

public class Score : MonoBehaviour
{
	// スコアを表示するGUIText
	public GUIText scoreGUIText;

	// ハイスコアを表示するGUIText
	public GUIText highScoreGUIText;

	// ゲーム経過時間(開始してからの時間)を表示するGUIText
	public GUIText passedTimeGUIText;

	// スコア
	private int score;

	// ハイスコア
	private int highScore;

	// PlayerPrefsで保存するためのキー
	private string highScoreKey = "highScore";

	// ゲーム経過時間(開始してからの時間)
	public float passedTime;




	// Managerコンポーネント
	//private Manager manager;




	void Start ()
	{
		Initialize ();
	}

	void Update ()
	{
		// スコアがハイスコアより大きければ
		if (highScore < score) {
			highScore = score;
		}

		// ゲーム開始してからどれくらいたったかの時間
		passedTime = Time.realtimeSinceStartup;

		// スコア・ハイスコア・ゲーム開始からの時間を表示する
		scoreGUIText.text     = "Score : " + score.ToString ();
		highScoreGUIText.text = "HighScore : " + highScore.ToString ();
		passedTimeGUIText.text = "PassedTime : " + passedTime.ToString ();
	}

	// ゲーム開始前の状態に戻す
	private void Initialize ()
	{
		// スコアを0に戻す
		score = 0;

		// ハイスコアを取得する。保存されてなければ0を取得する。
		highScore = PlayerPrefs.GetInt (highScoreKey, 0);
	}

	// ポイントの追加
	public void AddPoint (int point)
	{
		score = score + point;
	}

	// ハイスコアの保存
	public void Save ()
	{
		// ハイスコアを保存する
		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();

/*

		// Managerコンポーネントをシーン内から探して取得する
		manager = FindObjectOfType<Manager>();

		// タイトル表示中は待機
		while(manager.IsPlaying() == false) {
			yield return new WaitForEndOfFrame ();
		}

*/

		// ゲーム開始前の状態に戻す
		Initialize ();
	}
}