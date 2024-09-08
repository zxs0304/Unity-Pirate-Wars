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

            // ����һ���µ� GUIStyle
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                fontSize = 50, // ���������СΪ 24
                normal = { textColor = Color.green } // ����������ɫΪ��ɫ
            };

            // �����������
            Rect labelRect = new Rect(50, 50, 500, 500);
            GUI.Label(labelRect, $"Ping: {GameManager.PingVal}ms", style);
        }
    }
}