using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SetOutfit : ClientPacket
    {
        public byte UnknownByte1 { get; set; }

        public ushort MountId { get; set; }
        public ushort OutfitId { get; set; }

        public byte Addons { get; set; }
        public byte DetailColor { get; set; }
        public byte HeadColor { get; set; }
        public byte LegsColor { get; set; }
        public byte TorsoColor { get; set; }
        public byte MountDetailColor { get; set; }
        public byte MountHeadColor { get; set; }
        public byte MountLegsColor { get; set; }
        public byte MountTorsoColor { get; set; }

        public ushort FamiliarLook { get; set; }

        public SetOutfit(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.SetOutfit;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            if (Client.VersionNumber >= 12209066)
            {
                // TODO
                UnknownByte1 = message.ReadByte();
            }
            OutfitId = message.ReadUInt16();
            HeadColor = message.ReadByte();
            TorsoColor = message.ReadByte();
            LegsColor = message.ReadByte();
            DetailColor = message.ReadByte();
            Addons = message.ReadByte();
            MountId = message.ReadUInt16();
            if (Client.VersionNumber >= 126010468)
            {
                MountHeadColor = message.ReadByte();
                MountTorsoColor = message.ReadByte();
                MountLegsColor = message.ReadByte();
                MountDetailColor = message.ReadByte();
                FamiliarLook = message.ReadUInt16();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SetOutfit);
            if (Client.VersionNumber >= 12209066)
            {
                message.Write(UnknownByte1);
            }
            message.Write(OutfitId);
            message.Write(HeadColor);
            message.Write(TorsoColor);
            message.Write(LegsColor);
            message.Write(DetailColor);
            message.Write(MountId);
            if (Client.VersionNumber >= 126010468)
            {
                message.Write(MountHeadColor);
                message.Write(MountTorsoColor);
                message.Write(MountLegsColor);
                message.Write(MountDetailColor);
                message.Write(FamiliarLook);
            }
        }
    }
}
