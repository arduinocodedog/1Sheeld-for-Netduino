namespace OneSheeldClasses
{
    public interface ITwitterCallback
    {
        void OnNewTweet(string userName, string tweetText);
        void OnTwitterSelected();
    }
}
