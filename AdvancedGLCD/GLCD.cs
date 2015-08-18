using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedGLCD
{
    public class GLCD : OneSheeldUser, IOneSheeldSketch
    {
        /* Borders for the interface. */
        GLCDRectangle border1 = null;
        GLCDRectangle border2 = null;

        /* The three Buttons.*/
        GLCDButton lightButton1 = null;
        GLCDButton lightButton2 = null;
        GLCDButton coffeeMakerButton = null;

        public void Setup()
        {
            OneSheeld.begin();
            GLCD.clear();

            border1 = new GLCDRectangle(0, 0, 255, 127);
            border2 = new GLCDRectangle(2, 2, 251, 123);

            lightButton1 = new LightButton1(20, 49, 50, 30, "Light1");
            lightButton2 = new LightButton2(105, 49, 50, 30, "Light2");
            coffeeMakerButton = new CoffeeMakerButton(190, 49, 50, 30, "CM:ON");

            drawAllShapes();
        }

        public void Loop()
        {
        }

        void drawAllShapes()
        {
            /* Draw the two borders and the three buttons. */
            GLCD.draw(border1);
            GLCD.draw(border2);
            GLCD.draw(lightButton1);
            GLCD.draw(lightButton2);
            GLCD.draw(coffeeMakerButton);
        }
    }
}
