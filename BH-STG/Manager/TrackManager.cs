using BH_STG.Utils;

namespace BH_STG.Manager
{
    public class TrackManager
    {
        public static Track GetTrack(int trackStyle)
        {
            if (trackStyle == (int) Config.TrackStyle.NormalTrack) return new NormalTrack();

            if (trackStyle == (int) Config.TrackStyle.NormalWithBias) return new NormalWithBias();

            if (trackStyle == (int) Config.TrackStyle.CircleTrack) return new CircleTrack();

            if (trackStyle == (int)Config.TrackStyle.TailTrack) return new TailTrack();

            if (trackStyle == (int)Config.TrackStyle.NormalStayTrack) return new NormalStayTrack();

            throw new System.TypeInitializationException("No such track, make sure you pass valid trackStyle", null);
        }
    }
}