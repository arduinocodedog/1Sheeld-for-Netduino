using OneSheeldClasses;

namespace SimpleGLCD
{
    public class GLCD : OneSheeldUser, IOneSheeldSketch
    {
        public void Setup()
        {
            OneSheeld.begin();
            GLCD.clear();

            drawHouse();
            drawSun();
            drawBoy();
            drawGirl();
            drawClouds();
            drawBirds();
        }

        public void Loop()
        {
        }

        void drawHouse()
        {
            /* House Elements. */
            GLCDRectangle House = new GLCDRectangle(60, 50, 70, 87, 0);
            GLCDRectangle HouseWindow1 = new GLCDRectangle(65, 60, 25, 15, 0);
            GLCDRectangle HouseWindow2 = new GLCDRectangle(100, 60, 25, 15, 0);
            GLCDRectangle HouseDoor = new GLCDRectangle(85, 90, 20, 37, 0);
            GLCDLine HouseDoorHand = new GLCDLine(102, 109, 104, 109);
            GLCDLine HouseRoofLine1 = new GLCDLine(60, 50, 95, 15);
            GLCDLine HouseRoofLine2 = new GLCDLine(130, 50, 95, 15);

            /* Drawing. */
            GLCD.draw(House);
            GLCD.draw(HouseWindow1);
            GLCD.draw(HouseWindow2);
            GLCD.draw(HouseDoor);
            GLCD.draw(HouseDoorHand);
            GLCD.draw(HouseRoofLine1);
            GLCD.draw(HouseRoofLine2);
        }

        void drawSun()
        {
            /* Sun Elements. */
            GLCDEllipse Sun = new GLCDEllipse(0, 0, 30, 30);
            GLCDLine SunLight1 = new GLCDLine(35, 8, 56, 13);
            GLCDLine SunLight2 = new GLCDLine(33, 15, 55, 25);
            GLCDLine SunLight3 = new GLCDLine(30, 22, 50, 38);
            GLCDLine SunLight4 = new GLCDLine(25, 29, 42, 46);
            GLCDLine SunLight5 = new GLCDLine(20, 35, 36, 54);
            GLCDLine SunLight6 = new GLCDLine(13, 37, 25, 58);
            GLCDLine SunLight7 = new GLCDLine(7, 39, 15, 61);

            /* Drawing. */
            GLCD.draw(Sun);
            GLCD.draw(SunLight1);
            GLCD.draw(SunLight2);
            GLCD.draw(SunLight3);
            GLCD.draw(SunLight4);
            GLCD.draw(SunLight5);
            GLCD.draw(SunLight6);
            GLCD.draw(SunLight7);
        }

        void drawBoy()
        {
            /* Boy Elements. */
            GLCDLine BoyBody = new GLCDLine(150, 100, 150, 118);
            GLCDLine BoyLeftArm = new GLCDLine(150, 104, 160, 110);
            GLCDLine BoyRightArm = new GLCDLine(150, 104, 140, 110);
            GLCDLine BoyLeftLeg = new GLCDLine(150, 118, 154, 126);
            GLCDLine BoyRightLeg = new GLCDLine(150, 118, 146, 126);
            GLCDEllipse BoyHead = new GLCDEllipse(150, 95, 5, 5);

            /* Drawing. */
            GLCD.draw(BoyBody);
            GLCD.draw(BoyRightArm);
            GLCD.draw(BoyLeftArm);
            GLCD.draw(BoyLeftLeg);
            GLCD.draw(BoyRightLeg);
            GLCD.draw(BoyHead);
        }

        void drawGirl()
        {
            /* Girl Elements. */
            GLCDEllipse GirlHead = new GLCDEllipse(190, 95, 5, 5);
            GLCDLine GirlBody = new GLCDLine(190, 100, 190, 112);
            GLCDLine GirlLeftArm = new GLCDLine(190, 104, 200, 110);
            GLCDLine GirlRightArm = new GLCDLine(190, 104, 180, 110);
            GLCDLine GirlJupeLeftLine = new GLCDLine(190, 112, 196, 120);
            GLCDLine GirlJupeRightLine = new GLCDLine(190, 112, 184, 120);
            GLCDLine GirlJupeHorizontalLine = new GLCDLine(184, 120, 196, 120);
            GLCDLine GirlLeftLeg = new GLCDLine(188, 120, 188, 126);
            GLCDLine GirlRightLeg = new GLCDLine(192, 120, 192, 126);

            /* Girl's Hair Elements. */
            GLCDLine GirlHairLine1 = new GLCDLine(190, 90, 194, 87);
            GLCDLine GirlHairLine2 = new GLCDLine(190, 90, 186, 87);
            GLCDLine GirlHairLine3 = new GLCDLine(186, 87, 185, 90);
            GLCDLine GirlHairLine4 = new GLCDLine(194, 87, 195, 90);
            GLCDLine GirlHairLine5 = new GLCDLine(190, 90, 192, 84);
            GLCDLine GirlHairLine6 = new GLCDLine(190, 90, 188, 84);
            GLCDLine GirlHairLine7 = new GLCDLine(188, 84, 182, 87);
            GLCDLine GirlHairLine8 = new GLCDLine(192, 84, 198, 87);

            /* Drawing. */
            GLCD.draw(GirlBody);
            GLCD.draw(GirlRightArm);
            GLCD.draw(GirlLeftArm);
            GLCD.draw(GirlLeftLeg);
            GLCD.draw(GirlRightLeg);
            GLCD.draw(GirlHead);
            GLCD.draw(GirlJupeLeftLine);
            GLCD.draw(GirlJupeRightLine);
            GLCD.draw(GirlJupeHorizontalLine);
            GLCD.draw(GirlHairLine1);
            GLCD.draw(GirlHairLine2);
            GLCD.draw(GirlHairLine3);
            GLCD.draw(GirlHairLine4);
            GLCD.draw(GirlHairLine5);
            GLCD.draw(GirlHairLine6);
            GLCD.draw(GirlHairLine7);
            GLCD.draw(GirlHairLine8);
        }

        void drawClouds()
        {
            /* Clouds Element. */
            GLCDEllipse Cloud = new GLCDEllipse(200, 20, 30, 10);

            /* Drawing. */
            GLCD.draw(Cloud);
        }

        void drawBirds()
        {
            /* First Bird Elements. */
            GLCDLine Bird1RightWing = new GLCDLine(155, 20, 150, 17);
            GLCDLine Bird1LeftWing = new GLCDLine(155, 20, 160, 17);

            /* Second Bird Elements. */
            GLCDLine Bird2RightWing = new GLCDLine(140, 15, 135, 12);
            GLCDLine Bird2LeftWing = new GLCDLine(140, 15, 145, 12);

            /* Third Bird Elements. */
            GLCDLine Bird3RightWing = new GLCDLine(210, 40, 205, 37);
            GLCDLine Bird3LeftWing = new GLCDLine(210, 40, 215, 37);

            /* Drawing. */
            GLCD.draw(Bird1RightWing);
            GLCD.draw(Bird1LeftWing);
            GLCD.draw(Bird2RightWing);
            GLCD.draw(Bird2LeftWing);
            GLCD.draw(Bird3RightWing);
            GLCD.draw(Bird3LeftWing);
        }

    }
}
