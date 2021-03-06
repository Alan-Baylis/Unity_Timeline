﻿using UnityEngine;
using System.Collections;

public partial class Timeline
{
    /// <summary>
    /// 次の処理まで、指定された時間だけ処理を止めます。
    /// </summary>
    /// <param name="time">ストップする時間</param>
    /// <returns>メソッドチェイン</returns>
    public Timeline Delay(float time)
    {
        return this.Then(t => { }, time);
    }

    /// <summary>
    /// 指定された位置に向かって、指定された時間をかけて移動します。
    /// </summary>
    /// <param name="to">向かう地点</param>
    /// <param name="time">移動時間</param>
    /// <returns>メソッドチェイン</returns>
    public Timeline MoveTo(Vector3 to, float time)
    {
        bool setFrom = false;
        Vector3 from = Vector3.zero;
        return this.Then(t =>
        {
            if (setFrom == false)
            {
                setFrom = true;
                from = this.target.transform.position;
            }
            this.target.transform.position = Vector3.Lerp(from, to, time > 0 ? t / time : 1);
        }, time);
    }

    /// <summary>
    /// １秒間に指定された分だけ移動するペースで、指定された時間だけ移動します。
    /// </summary>
    /// <param name="by">移動の早さ[1/s]</param>
    /// <param name="time">移動時間</param>
    /// <returns></returns>
    public Timeline MoveBy(Vector3 by, float time)
    {
        return this.Then(t => this.target.transform.position += by * Time.deltaTime, time);
    }

    /// <summary>
    /// オブジェクトを破棄します。
    /// </summary>
    /// <returns></returns>
    public Timeline Destroy()
    {
        return this.Then(t => GameObject.Destroy(this.target)); // 破棄
    }

    /// <summary>
    /// SpriteRendererをもつオブジェクトにのみ使用できます。Spriteのカラーをアニメーションで変更します。
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public Timeline SpriteColor(Color color, float time)
    {
        var render = this.target.GetComponent<SpriteRenderer>();
        var from = render.color;
        return this.Then(t => render.color = Color.Lerp(from, color, time > 0 ? t / time : 1), time);
    }

    /// <summary>
    /// SpriteRendererをもつオブジェクトにのみ使用できます。Spriteを変更します
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public Timeline Sprite(Sprite sprite)
    {
        var render = this.target.GetComponent<SpriteRenderer>();
        return this.Then(t => render.sprite = sprite); // スプライトを変更
    }

}