using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class OpenRewardWall : ServerPacket
    {
        public string WarningText { get; set; }

        public uint TimeLeftToClaimCurrentReward { get; set; }
        public uint TimeRemainingUntilCanClaimNextReward { get; set; }

        public ushort EffectiveRewardsStreak { get; set; }
        public ushort RewardStreak { get; set; }

        public byte IndexOfNextReward { get; set; }

        public bool IsRewardShrine { get; set; }
        public bool HasWarningForNextReward { get; set; }
        public byte RecoverStreak { get; set; }
        public ushort JokersRequire { get; set; }

        public OpenRewardWall(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.OpenRewardWall;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsRewardShrine = message.ReadBool();
            TimeRemainingUntilCanClaimNextReward = message.ReadUInt32();
            IndexOfNextReward = message.ReadByte();
            HasWarningForNextReward = message.ReadBool();
            if (HasWarningForNextReward)
            {
                WarningText = message.ReadString();
            }
            
            if (Client.VersionNumber >= 126010468)
            {
                RecoverStreak = message.ReadByte();
                if (RecoverStreak == 1) {
                    JokersRequire = message.ReadUInt16();
                } else if (RecoverStreak == 2) {
                    TimeLeftToClaimCurrentReward = message.ReadUInt32();
                    JokersRequire = message.ReadUInt16();
                }
                EffectiveRewardsStreak = message.ReadUInt16();
            } else
            {
                TimeLeftToClaimCurrentReward = message.ReadUInt32();
                RewardStreak = message.ReadUInt16();
                EffectiveRewardsStreak = message.ReadUInt16();
            }

        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.OpenRewardWall);
            message.Write(IsRewardShrine);
            message.Write(TimeRemainingUntilCanClaimNextReward);
            message.Write(IndexOfNextReward);
            message.Write(HasWarningForNextReward);
            if (HasWarningForNextReward)
            {
                message.Write(WarningText);
            }

            if (Client.VersionNumber >= 126010468)
            {
                RecoverStreak = message.ReadByte();
                if (RecoverStreak == 1)
                {
                    message.Write(JokersRequire);
                }
                else if (RecoverStreak == 2)
                {
                    message.Write(TimeLeftToClaimCurrentReward);
                    message.Write(JokersRequire);
                }
                message.Write(EffectiveRewardsStreak);
            }
            else
            {
                message.Write(TimeLeftToClaimCurrentReward);
                message.Write(RewardStreak);
                message.Write(EffectiveRewardsStreak);
            }
        }
    }
}
