using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedREMOTE1
{
    public class Remote1 : OneSheeldUser, IOneSheeldSketch, ISubscribeCallback
    {
        RemoteOneSheeld usaSheeld = null;

        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            usaSheeld = new RemoteOneSheeld("-----REMOTE-1SHEELD-ADDRESS-----");

            OneSheeld.listenToRemoteOneSheeld(usaSheeld);

            usaSheeld.subscribeToChanges(OneSheeld.ConvertPinToByte(Pins.GPIO_PIN_D11));

            usaSheeld.setOnSubsribeOrDigitalChange(this);

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (VOICERECOGNITION.isNewCommandReceived())
                usaSheeld.sendMessage("USA", VOICERECOGNITION.getLastCommand());
        }

        public void OnSubscribeOrDigitalChange(byte incomingPinNumber, bool incommingPinValue)
        {
            if (led == null)
                led = new OutputPort(OneSheeld.ConvertByteToPin(incomingPinNumber), false);

            led.Write(incommingPinValue);
        }
    }
}
