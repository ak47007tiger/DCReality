namespace DC.GameLogic
{
    public class PlayerDataMgr : Singleton<PlayerDataMgr>
    {
        private int mLastGenerateActorId;

        public int GetMainActorId()
        {
            return 13;
        }

        public int GenerateActorId()
        {
            if (mLastGenerateActorId == 0)
            {
                mLastGenerateActorId = GetMainActorId();
            }
            var generateActorId = mLastGenerateActorId + 1;
            return generateActorId;
        }
    }
}