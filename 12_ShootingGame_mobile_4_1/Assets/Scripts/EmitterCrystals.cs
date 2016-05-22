using UnityEngine;
using System.Collections;

public class EmitterCrystals : MonoBehaviour
{
	// BlueCrystalプレハブを格納する
	//public GameObject[] crystals;

	// 現在のWave
	//private int currentWave;

	// Managerコンポーネント
	private Manager manager;

	IEnumerator Start ()
	{
/*
Debug.Log(crystals);
Debug.Log(crystals[0]);
Debug.Break();
*/
		// Waveが存在しなければコルーチンを終了する
		/*
		if (crystals.Length == 0) {
			yield break;
		}
		*/

		// Managerコンポーネントをシーン内から探して取得する
		manager = FindObjectOfType<Manager>();

		while (true) {

			// タイトル表示中は待機
			while(manager.IsPlaying() == false) {
				yield return new WaitForEndOfFrame ();
			}
/*
			// Crystalsを作成する
			GameObject g = (GameObject)Instantiate (crystals [0], transform.position, Quaternion.identity);

			// CrystalsをEmitterCrystalsの子要素にする
			g.transform.parent = transform;

			// Waveの子要素のEnemyが全て削除されるまで待機する
			while (g.transform.childCount != 0) {
				yield return new WaitForEndOfFrame ();
			}

			// Waveの削除
			Destroy (g);
*/
			// 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
			/*
			if (crystals.Length <= ++currentWave) {
				currentWave = 0;
			}
			*/
//Debug.Log(Time.realtimeSinceStartup);
			// ゲーム開始時間から一定時間経った場合
			if(Time.realtimeSinceStartup % 10 == 0) {

				// 出現させる位置を決める


				// crystalを作成する
				/*
				GameObject crystal = (GameObject)Instantiate (crystals [currentWave], transform.position, Quaternion.identity);
				*/
				/*
				GameObject crystal = (GameObject)Instantiate (crystals [0], transform.position, Quaternion.identity);
				*/
				GameObject crystal = (GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/BlueCrystal"), transform.position, Quaternion.identity);


				// crystalをEmitterの子要素にする
				//crystal.transform.parent = transform;

				// crystalの子要素のseedが全て削除されるまで待機する
				/*
				while (crystal.transform.childCount != 0) {
					yield return new WaitForEndOfFrame ();
				}
				*/

				// crystal要素が削除されるまで待機
				while (crystal.transform.childCount != 0) {
					yield return new WaitForEndOfFrame ();
				}

				// crystalの削除
				Destroy (crystal);
			}




		}
	}
}