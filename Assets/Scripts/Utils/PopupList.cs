using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Хранилище имен существующих префабов. Ничего лучше пока не придумал
 * 
 * TODO: сделать слабые ссылки и префабы загружать из res
*/
public static class PopupList
{
    public static readonly string Settings = "Settings";
    public static readonly string Main = "Main";
    public static readonly string Loss = "Loss";
    public static readonly string BossFight = "BossFight";
    public static readonly string BossDefeat = "BossDefeat";
}
