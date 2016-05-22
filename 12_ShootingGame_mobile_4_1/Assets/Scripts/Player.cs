using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
//Joystick.csとPlayer.csの仲介をするCrossPlatformInputManagerを使うために名前空間を指定

public class Player : MonoBehaviour
{
	// Spaceshipコンポーネント
	Spaceship spaceship;

	// Backgroundコンポーネント
	Background background;

	// 初期化
	const int PLAYERHP    = 100;
	const int BULLETPOWER = 1;
	const int CUREFIRST   = 20;
	const char ADD        = '+';
	const char REMOVE     = '-';

	// ヒットポイント
	public int hp = PLAYERHP;
	
	IEnumerator Start ()
	{
		//hp = 100;

		// Spaceshipコンポーネントを取得
		spaceship = GetComponent<Spaceship> ();

		// Backgroundコンポーネントを取得。３つのうちどれか１つを取得する
		background = FindObjectOfType<Background>();
		/*
Debug.Log(spaceship.canShot);
Debug.Break();
*/
		// canShotがfalseの場合、ここでコルーチンを終了させる
		if (spaceship.canShot == false) {
			yield break;
		}

		// bulletLevelが1の場合
		/*
		if (spaceship.bulletLevel == 1) {
			// 放出する弾のオブジェクトをレベル1のものに切り替える

			//yield break;
		}
		*/

		// 弾のレベルを更新
		//spaceship.bulletLevel = 1;

		while (true) {

			// 弾をプレイヤーと同じ位置/角度で作成
			spaceship.Shot (transform, spaceship.bulletLevel);

			// ショット音を鳴らす
			GetComponent<AudioSource>().Play();

			// shotDelay秒待つ
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}

	void Update ()
	{
/*
		// 右・左
		float x = Input.GetAxisRaw ("Horizontal");

		// 上・下
		float y = Input.GetAxisRaw ("Vertical");
*/

		// ジョイスティックの動きを自機の移動に反映する
        // 右・左
        float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        // 上・下
        float y = CrossPlatformInputManager.GetAxisRaw("Vertical");

		// 移動する向きを求める
		//Vector2 direction = new Vector2 (x, y).normalized;
		Vector2 direction = new Vector2 (x, y);

		// 移動の制限
		Move (direction);

	}

	// 機体の移動
	//void Move (Vector2 direction)
	void Move (Vector3 direction)
	{
		// 背景のスケール
		Vector2 scale = background.transform.localScale;

		// 背景のスケールから取得
		Vector2 min = scale * -0.5f;
		Vector2 max = scale * 0.5f;

		// 画面左下のワールド座標をビューポートから取得
		//Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		// 画面右上のワールド座標をビューポートから取得
		//Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		// プレイヤーの座標を取得
		//Vector2 pos = transform.position;
		Vector3 pos = transform.position;

		// 移動量を加える
		pos += direction  * spaceship.speed * Time.deltaTime;

		// プレイヤーの位置が画面内に収まるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		// 制限をかけた値をプレイヤーの位置とする
		transform.position = pos;
	}

	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D (Collider2D c)
	{


		// レイヤー名を取得
		string layerName = LayerMask.LayerToName(c.gameObject.layer);

		// レイヤー名がBullet (Enemy)の時は弾を削除
		if( layerName == "Bullet (Enemy)")
		{
			// 弾の削除
			Destroy(c.gameObject);
		}

		// レイヤー名がBullet (Enemy)の場合は爆発
		//if( layerName == "Bullet (Enemy)" || layerName == "Enemy")
		if( layerName == "Bullet (Enemy)")
		{

			// 以下Enemy.csのマネ

	        // PlayerBulletのTransformを取得
			Transform enemyBulletTransform = c.transform;


			// Bulletコンポーネントを取得
			Bullet bullet = enemyBulletTransform.GetComponent<Bullet>();

			// ヒットポイントを減らす
			hp = hp - bullet.power;

            // HPスコアコンポーネントを取得してHPを減らす
			FindObjectOfType<PlayerStatus>().calHp(bullet.power, REMOVE);
			//上記は以下のように書いたほうがいいかも
            //FindObjectOfType<PlayerStatus>().removePoint(bullet.power);


			// ヒットポイントが0以下ならば
			if(hp <= 0) {

				// 爆発
				spaceship.Explosion ();

				// エネミーの削除
				Destroy (gameObject);

				// Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
				FindObjectOfType<Manager>().GameOver();

	            // HPスコアコンポーネントを取得してHPを元に戻す
				hp = PLAYERHP;

			}

		}

		// クリスタルをゲットしたとき
		// タグで判別してもいいし弾のようにレイヤー名から判別してもいい
		if (c.gameObject.tag == "BlueCrystal") {

			// 弾のレベルを更新
			spaceship.bulletLevel = 1;

			// クリスタルを削除
			Destroy (c.gameObject);
		} 
		else if (c.gameObject.tag == "GreenCrystal") {
			// 弾のレベルを更新
			spaceship.bulletLevel = 2;
			
			// クリスタルを削除
			Destroy (c.gameObject);

		}
		else if (c.gameObject.tag == "CureFirst") {
			// HPを更新
			hp += 20;

			// HPスコアコンポーネントを取得してHPを増やす
			FindObjectOfType<PlayerStatus>().calHp(CUREFIRST, ADD);
			
			// 効果音がなる
			
			// オブジェクトを削除
			Destroy (c.gameObject);
			
		}
	}
}