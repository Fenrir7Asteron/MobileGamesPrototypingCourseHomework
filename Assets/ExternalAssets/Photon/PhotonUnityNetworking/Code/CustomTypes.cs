// ----------------------------------------------------------------------------
// <copyright file="CustomTypes.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2018 Exit Games GmbH
// </copyright>
// <summary>
// Sets up support for Unity-specific types. Can be a blueprint how to register your own Custom Types for sending.
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------


namespace Photon.Pun
{
    using UnityEngine;
    using Photon.Realtime;
    using ExitGames.Client.Photon;
    using Destructible2D;

    /// <summary>
    /// Internally used class, containing de/serialization method for PUN specific classes.
    /// </summary>
    internal static class CustomTypes
    {
        private const int SizeRect = 4 * 4;

        /// <summary>Register de/serializer methods for PUN specific types. Makes the type usable in RaiseEvent, RPC and sync updates of PhotonViews.</summary>
        internal static void Register()
        {
            PhotonPeer.RegisterType(typeof(Player), (byte) 'P', SerializePhotonPlayer, DeserializePhotonPlayer);
            PhotonPeer.RegisterType(typeof(D2dRect), (byte) 'R', SerializeDestructibleRect, DeserializeDestructibleRect);
        }


        #region Custom De/Serializer Methods

        public static readonly byte[] memPlayer = new byte[4];

        private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
        {
            int ID = ((Player) customobject).ActorNumber;

            lock (memPlayer)
            {
                byte[] bytes = memPlayer;
                int off = 0;
                Protocol.Serialize(ID, bytes, ref off);
                outStream.Write(bytes, 0, 4);
                return 4;
            }
        }

        private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
        {
            if (length != 4)
            {
                return null;
            }

            int ID;
            lock (memPlayer)
            {
                inStream.Read(memPlayer, 0, length);
                int off = 0;
                Protocol.Deserialize(out ID, memPlayer, ref off);
            }

            if (PhotonNetwork.CurrentRoom != null)
            {
                Player player = PhotonNetwork.CurrentRoom.GetPlayer(ID);
                return player;
            }
            return null;
        }

        public static readonly byte[] memD2dRect= new byte[SizeRect];

        private static short SerializeDestructibleRect(StreamBuffer outStream, object customobject)
        {
            D2dRect rect = (D2dRect) customobject;

            lock (memD2dRect)
            {
                byte[] bytes = memD2dRect;
                int off = 0;
                Protocol.Serialize(rect.MinX, bytes, ref off);
                Protocol.Serialize(rect.MaxX, bytes, ref off);
                Protocol.Serialize(rect.MinY, bytes, ref off);
                Protocol.Serialize(rect.MaxY, bytes, ref off);
                outStream.Write(bytes, 0, SizeRect);
                return SizeRect;
            }
        }

        private static object DeserializeDestructibleRect(StreamBuffer inStream, short length)
        {
            if (length != SizeRect)
            {
                return null;
            }

            D2dRect rect = new D2dRect();
            lock (memD2dRect)
            {
                inStream.Read(memD2dRect, 0, length);
                int off = 0;
                Protocol.Deserialize(out rect.MinX, memD2dRect, ref off);
                Protocol.Deserialize(out rect.MaxX, memD2dRect, ref off);
                Protocol.Deserialize(out rect.MinY, memD2dRect, ref off);
                Protocol.Deserialize(out rect.MaxY, memD2dRect, ref off);
            }

            return rect;
        }

        #endregion
    }
}