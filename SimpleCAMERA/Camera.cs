using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;


namespace SimpleCAMERA
{
    public class Camera
    {
        OneSheeld sheeld = null;

        InputPort button = null;
        OutputPort led = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, true, Port.ResistorMode.Disabled);
            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (button.Read())
            {
                led.Write(true);
                sheeld.CAMERA.setFlash(sheeld.CAMERA.ON);
                sheeld.CAMERA.rearCapture();
                Thread.Sleep(10000); 
                sheeld.TWITTER.tweetLastPicture("Posted by @1Sheeld");
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
