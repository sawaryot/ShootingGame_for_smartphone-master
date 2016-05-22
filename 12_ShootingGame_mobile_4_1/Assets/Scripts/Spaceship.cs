using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Spaceship : MonoBehaviour
{
	// 移動スピード
	public float speed;

	// 弾を撃つ間隔
	public float shotDelay;

	// 弾のPrefab
	public GameObject bullet;

	// 弾を撃つかどうか
	public bool canShot;

	// 弾のレベル
	public int bulletLevel;

	// 機体の体力

	// 爆発のPrefab
	public GameObject explosion;

	// アニメーターコンポーネント
	private Animator animator;

	void Start ()
	{
		// アニメーターコンポーネントを取得
		animator = GetComponent<Animator> ();
	}

	// 爆発の作成
	public void Explosion ()
	{
		Instantiate (explosion, transform.position, transform.rotation);
	}

	// 弾の作成
	public void Shot (Transform origin, int Level = 0)
	{

		if(Level == 1) {
			// プレハブを取得
			bullet = (GameObject)Resources.Load ("Prefabs/PlayerBullet/PlayerBulletSecond");
		}
		else if(Level == 2){
			// プレハブを取得
			bullet = (GameObject)Resources.Load ("Prefabs/PlayerBullet/PlayerBulletThird");
		}

		Instantiate (bullet, origin.position, origin.rotation);
	}

	// アニメーターコンポーネントの取得
	public Animator GetAnimator()
	{
		return animator;
	}
}