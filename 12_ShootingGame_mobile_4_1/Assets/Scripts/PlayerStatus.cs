using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
        // HPを表示するGUIText
        public GUIText playerHpGUIText;

        // ハイスコアを表示するGUIText
        //public GUIText highScoreGUIText;

        // HP
        private int iPlayerHp;

        // ハイスコア
        //private int highScore;

        // PlayerPrefsで保存するためのキー
        //private string playerHpKey = "iPlayerHp";

        void Start ()
        {
                Initialize ();
        }

        void Update ()
        {
                // プレイヤーのhpが0より低い場合
                if (iPlayerHp < 0) {

                        //highScore = score;
                }

                // プレイヤーHPを表示する
                playerHpGUIText.text = "PlayerHP : " + iPlayerHp.ToString ();
                //highScoreGUIText.text = "HighScore : " + highScore.ToString ();
        }

        // ゲーム開始前の状態に戻す
        private void Initialize ()
        {
                // HPを100に戻す
                iPlayerHp = 100;

                // ハイスコアを取得する。保存されてなければ0を取得する。
                //iPlayerHp = PlayerPrefs.GetInt (playerHpKey, 0);
        }

        // HPの計算
        public void calHp (int point, char operator_symbol)
        {
			if (operator_symbol == '+') {

				iPlayerHp = iPlayerHp + point;

			}
			else if (operator_symbol == '-') {
				
				iPlayerHp = iPlayerHp - point;
				
			}

        }


	// ハイスコアHPの保存
	public void Save ()
	{
                // ハイスコアを保存する
                //PlayerPrefs.SetInt (playerHpKey, iPlayerHp);
                //PlayerPrefs.Save ();

                // ゲーム開始前の状態に戻す
                Initialize ();
        }

        // ゲームオーバー時の初期化
        public void gameOver() {

        	// HPを100に戻す
        	iPlayerHp = 100;
        }
}