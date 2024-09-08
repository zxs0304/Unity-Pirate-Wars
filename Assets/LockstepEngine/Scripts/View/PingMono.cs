using System.Collections.Generic;
using System.Linq;
using Lockstep.Math;
using UnityEngine;

namespace LockstepTutorial {
    public class PingMono : UnityEngine.MonoBehaviour {
        private float _guiTimer;
        public List<float> delays => GameManager.Delays;

        private void Update(){
            if (delays == null) return;
            _guiTimer += Time.deltaTime;
            if (_guiTimer > 0.5f) {
                _guiTimer = 0;
                GameManager.PingVal = (int) (delays.Sum() * 1000 / LMath.Max(delays.Count, 1));
                delays.Clear();
            }
        }

        private void OnGUI(){

            // 创建一个新的 GUIStyle
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                fontSize = 50, // 设置字体大小为 24
                normal = { textColor = Color.green } // 设置字体颜色为白色
            };

            // 定义矩形区域
            Rect labelRect = new Rect(50, 50, 500, 500);
            GUI.Label(labelRect, $"Ping: {GameManager.PingVal}ms", style);
        }
    }
}