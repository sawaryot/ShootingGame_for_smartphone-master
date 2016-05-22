using UnityEngine;
using System.Collections;
//using System.Convert;
//using System;

using System.IO;  // Directotyクラスを使うにはこれが必要

public class Emitter : MonoBehaviour
{
	/* ---------------------
	 * 初期化
	 * ---------------------
	 */

	// Waveプレハブを格納する
	public GameObject[] waves;

	// 現在のWave
	private int currentWave;

	// Managerコンポーネント
	private Manager manager;

	// Playerコンポーネント
	private Player player;

	// プレハブの種類の数(プレイヤー武器の数、回復アイテムの数)
	public int prefabCnt;

	// 現在のステージの種類
	public string currentStage;

	// 出現させる敵を初期化
	GameObject prefabEnemy = null;

	// 出現させるアイテムを初期化
	GameObject prefabItem = null;

	// 初期化
	string prefabFolderPathDefault = "/Resources/Prefabs";

	IEnumerator Start ()
	{
		// Waveが存在しなければコルーチンを終了する
		if (waves.Length == 0) {
			yield break;
		}

		// Managerコンポーネントをシーン内から探して取得する
		manager = FindObjectOfType<Manager>();

		// ステージ情報を更新(ステージ1-1)
		currentStage = "firstOne";

		// Playerコンポーネントをシーン内から探して取得する
		//player = FindObjectOfType<Player>();

		// Stage1-1
		while (true) {

			// タイトル表示中は待機
			while(manager.IsPlaying() == false) {
				yield return new WaitForEndOfFrame ();
			}

            Debug.Log(currentStage);

            // アイテムを出現
            prefabItem = ItemAppear();

			// Waveを作成する
			GameObject g = (GameObject)Instantiate (waves [currentWave], transform.position, Quaternion.identity);

			// WaveをEmitterの子要素にする
			g.transform.parent = transform;

			// Waveの子要素のEnemyが全て削除されるまで待機する
			while (g.transform.childCount != 0) {
				yield return new WaitForEndOfFrame ();
			}

			// Wave削除
			Destroy (g);

			// アイテム削除
			Destroy(prefabItem);

			// 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
			if (waves.Length <= ++currentWave) {
				//currentWave = 0;
				// 次のステージに移る
				currentStage = "firstTwo";
                Debug.Log(currentStage);
                break;
			}

		}

		// Stage1以降の敵とアイテム出現
		StageSwitcher (currentStage);



	}

	void Update () {

		// ここなんとか一回処理のみで済ませたい・・・
		// Playerコンポーネントをシーン内から探して取得する
		player = FindObjectOfType<Player>();


		// Stage1-2
		/*
		if(currentStage == "firstTwo") {

			// 敵を出現させる


			// アイテムを出現させる

		}
		*/
		/*
		// Stage1-3		
		else if() {




		}
		// Stage1-4
		else if() {


		}
		// Stage2-1
		else if() {

		}
		// Stage2-1
		else {

		}
		*/




	}

	// 敵を表示させる
	GameObject EnemyAppear(string prefabFolderPath){

		// ステージによって表示させる敵を決める


		// Waveを作成する
		GameObject g = (GameObject)Instantiate ((GameObject)Resources.Load (prefabFolderPath),  transform.position, Quaternion.identity) as GameObject;
		
		// WaveをEmitterの子要素にする
		g.transform.parent = transform;

		return g;

	}

	// アイテムを表示させてそのアイテムのオブジェクトを返す
	GameObject ItemAppear() {
		
		// playerのhpが50未満になったら
		if(player.hp < 100) {
			
			/*
			 * ----------------------------
			 * 出現させるアイテムを決める
			 * ----------------------------
			 */
			// 初期化
			string prefabFolderPath = prefabFolderPathDefault + "/Items";
			
			// 出現するプレハブ(プレイヤー武器か回復アイテムか)情報を取得
			string[] path_array = Directory.GetFiles( Application.dataPath + prefabFolderPath );
			
			// プレハブフォルダ(Weapons,Cures)の数の値の乱数を取得(PlayerBulletSecond,Enemy(,EventSystem)の分-2しておく)
			// ⇑これは無視
			// プレハブフォルダ(Weapons,Cures)の数の値の乱数を取得
			// (Range()は※min≦戻り値＜maxなので最大値+1とする)
			int prefabFlg = Random.Range(1, path_array.Length + 1);

			Debug.Log(path_array.Length);
			
			// プレイヤー武器か回復アイテムかを選ぶ
			if(prefabFlg == 1) {
				// プレイヤー武器のパスを設定
				prefabFolderPath = prefabFolderPath + "/Weapons";
			}
			else if(prefabFlg == 2) {
				// 回復アイテムのパスを設定
				prefabFolderPath = prefabFolderPath + "/Cures";
			}
			
			// Assetsフォルダまでの絶対パスを取得
			//Debug.Log (Application.dataPath);
			
			//string[] path_array = Directory.GetFiles( Application.dataPath , "*.*" );
			// 出現するプレハブに関連するファイル情報を取得
			path_array = Directory.GetFiles( Application.dataPath + prefabFolderPath );
			
			/*
	            for( int i = 0; i < array_num; i++ ){
	                Debug.Log( path_array[i] );
	            }
            	*/
			Debug.Log (prefabFolderPath);
			Debug.Log(path_array);
			// 出現させるプレハブの種類の数(実際に使用するプレハブファイルの数)
			prefabCnt = path_array.Length / 2;
			
			Debug.Log(prefabCnt);
			
			// プレハブの種類のパターン分の値を乱数で出す(※min≦戻り値＜maxなので最大値+1とする)
			int prefabFlgSec = Random.Range(1, prefabCnt + 1);
			//prefabFlgSec = 0;
			
			Debug.Log(prefabFlgSec);
			
			// プレイヤー武器か回復アイテムか
			// プレイヤー武器の場合
			if(prefabFlg == 1) {
				// プレイヤー武器１の場合
				if(prefabFlgSec == 1) {
					
					prefabFolderPath = prefabFolderPath + "/BlueCrystal";
					
				}
				// プレイヤー武器２の場合
				else if(prefabFlgSec == 2) {
					
					prefabFolderPath = prefabFolderPath + "/GreenCrystal";
				}
				
			} 
			// 回復アイテムの場合
			else if(prefabFlg == 2){
				prefabFlgSec = 1;
				
				// 小回復の場合
				if(prefabFlgSec == 1) {
					prefabFolderPath = prefabFolderPath + "/CureFirst";
				}
				// の場合
				else if(prefabFlgSec == 2) {
				}
				
			}
			
			/*
				if(prefabFlg == 1) {
					// 回復アイテムのパスを設定
					prefabFolderPath = "/Resources/Prefabs/Weapons";
				}
				*/
			//Debug.Break();
			/*
			 * ----------------------------
			 * 出現させるアイテムの座標を決める
			 * ----------------------------
			 */
			
			// ランダムでプレイヤー武器、回復アイテムが出現する座標を決める
			// x座標が-4~4
			//float offsetX = Random.Range(-4.0F, 4.0F);
			float offsetX = UnityEngine.Random.Range(-4.0F, 4.0F);
			
			//float offsetZ = Random.Range(40F, 50F);
			//Vector3 crystalPosition = new Vector3(offsetX, 0, offsetZ);
			// 出現させるy座標は固定
			Vector3 prefabPosition = new Vector3(offsetX, 3.6F);
			
			/*
			 * ---------------------------------------------------
			ランダムで作成するプレイヤー武器、回復アイテムの種類を決める
			 * ---------------------------------------------------
			 */
			
			// プレイヤー武器の種類の数を取得
			
			
			//float crystaltype = Random.Range(1.0F, 2.0F)
			//if()
			
			// Resources.Loadの引数として使えるようにするため、先頭の/Resources/を削除する
			//prefabFolderPath = prefabFolderPath.TrimStart("/Resources/");
			// 先頭から11文字削除(/Resources/)
			prefabFolderPath = prefabFolderPath.Remove(0, 11);
			
			//Debug.Log(prefabItem);
			
			// Crystal or Cureを作成する
			//Instantiate ((GameObject)Resources.Load ("Prefabs/BlueCrystal"), transform.position, Quaternion.identity);
			//Instantiate ((GameObject)Resources.Load ("Prefabs/Weapons/BlueCrystal"), prefabPosition, Quaternion.identity);
			//Instantiate ((GameObject)Resources.Load (prefabFolderPath + "/GreenCrystal"), crystalPosition, Quaternion.identity);
			prefabItem = (GameObject)Instantiate ((GameObject)Resources.Load (prefabFolderPath), prefabPosition, Quaternion.identity) as GameObject;
			//Debug.Log(prefabItem);
		}
		
		// プレイヤー武器、回復アイテムの削除
		//Destroy (prefabItem);

		// プレイヤー武器、回復アイテムの削除
		return prefabItem;

	}

	// 表示するステージを切り替える
	/*
	void StageSwitcher() {

		// ステージフラグ
		//int stageFlg = 0;


		if(stageFlg == ) {
		
		}


	}
	*/

	/* --------------------------------------------------------
	 * 最初のステージ以降のステージの敵、アイテムを出現させる
	 * --------------------------------------------------------
	 */
	IEnumerator StageSwitcher(string currentStage) {
	//void StageTwo(string currentStage) {

		// 初期化
		string prefabFolderPath = prefabFolderPathDefault + "/Enemy/StageFirstTwo";

        Debug.Log(currentStage);


        //ステージ1-2の敵とアイテム
        if (currentStage == "firstTwo") {

            Debug.Log(currentStage);


            // Waveが存在しなければコルーチンを終了する
            if (waves.Length == 0) {
				yield break;
			}
		
			// Managerコンポーネントをシーン内から探して取得する
			manager = FindObjectOfType<Manager> ();
		
			// ステージ情報を更新(ステージ1-1)
			//currentStage = "firstOne";
		
			// Playerコンポーネントをシーン内から探して取得する
			//player = FindObjectOfType<Player>();
		
			// Stage1-
			while (true) {
			
				// タイトル表示中は待機
				while (manager.IsPlaying() == false) {
					yield return new WaitForEndOfFrame ();
				}


				// アイテムを出現
				prefabItem = ItemAppear ();

				// 敵を出現
				prefabEnemy = EnemyAppear(prefabFolderPath);

/*			
				// Waveを作成する
				//GameObject g = (GameObject)Instantiate (waves [currentWave], transform.position, Quaternion.identity);
				GameObject g = (GameObject)Instantiate ((GameObject)Resources.Load (prefabFolderPath),  transform.position, Quaternion.identity) as GameObject;

				// WaveをEmitterの子要素にする
				g.transform.parent = transform;
			
				// Waveの子要素のEnemyが全て削除されるまで待機する
				while (g.transform.childCount != 0) {
					// 以下を使用するためメソッドの型はIEnumerator
					yield return new WaitForEndOfFrame ();
				}
*/			

				// Waveの子要素のEnemyが全て削除されるまで待機する
				while (prefabEnemy.transform.childCount != 0) {
					// 以下を使用するためメソッドの型はIEnumerator
					yield return new WaitForEndOfFrame ();
				}


				// Wave削除
				Destroy (prefabEnemy);
			
				// アイテム削除
				Destroy (prefabItem);
			
				// 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
				if (waves.Length <= ++currentWave) {
					//currentWave = 0;
					// 次のステージに移る
					currentStage = "firstTwo";
					break;
				}
			
			}
		}
		else if(currentStage == "firstThree"){


		}

		// 戻り値を返す
		//(IEnumerator 型を戻り値として yield return ステートメントをボディ部分に含める)
		//callback(result);
		//callback ();
		yield return true;


	}


	void StageTwo() {


	}


}