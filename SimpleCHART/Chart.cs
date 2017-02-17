using OneSheeldClasses;

namespace SimpleCHART
{
    public class Chart : OneSheeldUser, IOneSheeldSketch
    {
        public void Setup()
        {
            // Start communication.
            OneSheeld.begin();
            // Save a screenshot for chart 0.
            CHART.saveScreenshot(CHART.CHART_0);
            // Save a csv file for chart 0.
            CHART.saveCsv("MicValues", CHART.CHART_0);
            // Clear Chart 0.
            CHART.clear(CHART.CHART_0);
        }

        public void Loop()
        {
            // Add mic values to be ploted over chart. 
            CHART.add("Mic/db", MIC.getValue());
            // Plot the values. 
            CHART.plot();
            // Delay for 1 second. 
            OneSheeld.delay(1000);
        }
    }
}
