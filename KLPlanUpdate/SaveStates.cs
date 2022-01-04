namespace Fujitsu.Tools.KLPlanUpdate
{
    internal enum SaveStates
    {
        Saved,
        New,
        Updated,
        Deleted
    }

    internal static class SaveStateImage
    {
        public static int GetImageIndex(SaveStates pState)
        {
            switch (pState)
            {
                case SaveStates.New:
                    return 1;
                case SaveStates.Updated:
                    return 2;
                case SaveStates.Deleted:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
