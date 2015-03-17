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
    public class Remote1 : ISubscribeCallback
    {
        OneSheeld sheeld = null;
        RemoteOneSheeld usaSheeld = null;

        OutputPort led = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            usaSheeld = new RemoteOneSheeld(sheeld, "-----REMOTE-1SHEELD-ADDRESS-----");

            sheeld.listenToRemoteOneSheeld(usaSheeld);

            usaSheeld.subscribeToChanges(sheeld.ConvertPinToByte(Pins.GPIO_PIN_D11));

            usaSheeld.setOnSubsribeOrDigitalChange(this);

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (OneSheeld.VOICERECOGNITION.isNewCommandReceived())
                usaSheeld.sendMessage("USA", OneSheeld.VOICERECOGNITION.getLastCommand());
        }

        public void OnSubscribeOrDigitalChange(byte incomingPinNumber, bool incommingPinValue)
        {
            if (led == null)
                led = new OutputPort(sheeld.ConvertByteToPin(incomingPinNumber), false);

            led.Write(incommingPinValue);
        }
    }
}
