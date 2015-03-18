using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class TwitterShield : ShieldParent
    {
        string userName = null;
 	    string tweetText = null;
 	    bool isCallBackAssigned = false;
 	    bool isItNewTweet = false;
        ITwitterCallback twitterCallback = null;

        public TwitterShield()
            : base(ShieldIds.TWITTER_ID)
        {
        }

        public void tweet(string data)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(data.Length, System.Text.Encoding.UTF8.GetBytes(data));

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.TWITTER_ID, 0, TWITTER_SEND, 1, args);
        }

        public void sendMessage(string username, string message)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(username.Length, System.Text.Encoding.UTF8.GetBytes(username));
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(message.Length, System.Text.Encoding.UTF8.GetBytes(message));
            args.Add(arg2);


            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.TWITTER_ID, 0, TWITTER_SEND_DIRECT_MESSAGE, 2, args);
        }

        public void tweetLastPicture(string pictureText, byte imageSource)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(pictureText.Length, System.Text.Encoding.UTF8.GetBytes(pictureText));
            args.Add(arg1);

            byte[] imgsrc = new byte[1];
            imgsrc[0] = imageSource;

            FunctionArg arg2 = new FunctionArg(1, imgsrc);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.TWITTER_ID, 0, TWITTER_POST_LAST_PIC, 2, args);
        }

        public void tweetLastPicture(string pictureText)
        {
            tweetLastPicture(pictureText, (byte) 0);
        }

        //Check if new tweet 
        public bool isNewTweet()
        {
	        return isItNewTweet;
        }

        public void trackKeyword(string keyword)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(keyword.Length, System.Text.Encoding.UTF8.GetBytes(keyword));
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.TWITTER_ID,0,TWITTER_TRACK_KEYWORD,1,args);
        }

        public void untrackKeyword(string keyword)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(keyword.Length, System.Text.Encoding.UTF8.GetBytes(keyword));
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.TWITTER_ID,0,TWITTER_UNTRACK_KEYWORD,1,args);
        }

        // UserName Getter
        string getUserName()
        {
	        isItNewTweet=false;
	        return userName;
        }

        //Tweet Getter
        string getTweet()
        {
	        return tweetText;
        }

        public override void processData()
        {
	        //Checking Function-ID
            byte functionId = getOneSheeldInstance().getFunctionId();
	        if( functionId == TWITTER_GET_TWEET)
	        {	
		        isItNewTweet = true;

		        if(userName != "")
		        {
			        userName = "";
		        }

		        if (tweetText != "")
		        {
			        tweetText = "";
		        }

                int userNameLength = getOneSheeldInstance().getArgumentLength(0);
		        for (int j=0; j<userNameLength; j++)
		        {
                    userName += getOneSheeldInstance().getArgumentData(0)[j];
		        }

                int tweetLength = getOneSheeldInstance().getArgumentLength(1);
		        for(int i=0 ;i<tweetLength;i++)
		        {
                    tweetText += getOneSheeldInstance().getArgumentData(1)[i];
		        }

                //Users Function Invoked
		        if(isCallBackAssigned && !isInACallback())
		        {
                    enteringACallback();
			        twitterCallback.OnNewTweet(userName,tweetText);
                    exitingACallback();
		        }
	        }
	        else if(functionId == TWITTER_CHECK_SELECTED) //called when twitter shield is selected
	        {
                if (isCallBackAssigned && !isInACallback())
                {
                    enteringACallback();
                    twitterCallback.OnTwitterSelected();
                    exitingACallback();
                }
            }
        }

        //Setup Callback
        public void setTwitterCallback(ITwitterCallback userCallback)
        {
            isCallBackAssigned = true;
	        twitterCallback = userCallback;
        }

        //Output Function ID
        const byte TWITTER_SEND = 0x01;  
        const byte TWITTER_SEND_DIRECT_MESSAGE = 0x02;
        const byte TWITTER_POST_LAST_PIC = 0x03;
        const byte TWITTER_TRACK_KEYWORD = 0x04;
        const byte TWITTER_UNTRACK_KEYWORD = 0x05;

        //Input Functions ID's
        const byte TWITTER_GET_TWEET = 0x01;
        const byte TWITTER_CHECK_SELECTED = 0x02;
    }
}
