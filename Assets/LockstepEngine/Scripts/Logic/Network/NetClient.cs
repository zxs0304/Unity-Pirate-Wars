using System;
using System.Net;
using Lockstep.Network;
using LockstepTutorial;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace Lockstep.Logic{
    public class NetClient : IMessageDispatcher {
        public static IPEndPoint serverIpPoint = NetworkUtil.ToIPEndPoint("192.168.0.117", 10083);
        private NetOuterProxy net = new NetOuterProxy();
        public Session Session;

        private int count = 0;
        public int id;

        public void Start(){
            net.Awake(NetworkProtocol.TCP);
            net.MessageDispatcher = this;
            net.MessagePacker = MessagePacker.Instance;
            Session = net.Create(serverIpPoint);
            Session.Start();
        }


        public void Dispatch(Session session, Packet packet){
            ushort opcode = packet.Opcode();
            var message = session.Network.MessagePacker.DeserializeFrom(opcode, packet.Bytes, Packet.Index,
                packet.Length - Packet.Index) as IMessage;
            var type = (EMsgType) opcode;
            switch (type) {
                case EMsgType.FrameInput:
                    OnFrameInput(session, message);
                    break;
                case EMsgType.StartGame:
                    OnStartGame(session, message);
                    break;
            }
        }

        public void OnFrameInput(Session session, IMessage message){
            var msg = message as Msg_FrameInput;

            GameManager.PushFrameInput(msg.input);
            //Debug.Log($"{DateTime.Now:HH:mm:ss.fff} , {GameManager.Instance.localPlayerId}ºÅ ÍøÂçÊÕµ½µÚ{msg.input.tick}Ö¡ ");
        }

        public void OnStartGame(Session session, IMessage message){
            var msg = message as Msg_StartGame;
            GameManager.StartGame(msg);
        }

        public void Send(IMessage msg){
            Session.Send(msg);
        }
    }
}