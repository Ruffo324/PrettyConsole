# PrettyConsole (No future support (10.12.2017))
# Well, almost a year later (PrettyConsole was my first C# project) I have to realize that "PrettyConsole" can be implemented much more elegantly and beautifully. Therefore: Should not be used.
C # library for better formatted and colored console output

```c# 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



//////// Using Example ////////
using PrettyConsole;
using console = PrettyConsole.ConsoleFunctions;
//////// Using Example ////////


namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            console.Log("Quick ".cCyan() + " and ".cReset() + "Easy".cGreen() + " colorized console Output.".cReset());
            console.Line(); //Line with Default ("-".cGray(), 50)

            //Disable this if you want to use this in a normal c# project.. Or leave it enabled to use 
            //https://wiki.gtanet.work/index.php?title=Fonts Colors in the Strings directly..
            console.FIncludeGTANetworkColors = false;

            #region Output All Colors

            console.Line("#", 10);
            console.Log("DarkBlue".cDarkBlue());
            console.Log("DarkGreen".cDarkGreen());
            console.Log("DarkRed".cDarkRed());
            console.Log("DarkMagenta".cDarkMagenta());
            console.Log("DarkYellow".cDarkYellow());
            console.Log("Green".cGreen());
            console.Log("DarkCyan".cDarkCyan());
            console.Log("DarkGray".cDarkGray());
            console.Line("#", 10);
            console.Log("Black".cBlack());
            console.Log("Gray".cGray());
            console.Log("Blue".cBlue());
            console.Log("Green".cGreen());
            console.Log("Cyan".cCyan());
            console.Log("Red".cRed());
            console.Log("Magenta".cMagenta());
            console.Log("Yellow".cYellow());
            console.Log("White".cWhite());
            console.Log("New" + "Line in the String".cNewLine());
            console.Log("Orignal-Console-Color".cReset());
            console.Line("#", 10);

            #endregion //Output All Colors



            #region Propertys Test

            //Setting diffrent default Line-Lenght
            console.FLineLength = 2;
            console.Line();

            //Custom DateTimeFormatStr for Log
            console.FDateTimeFormatStr = "dd.mm.yyyy";
            console.Log("Date-Time-Format");

            //Setting Diffrent StartColorCodeStr StopColorCodeStr
            console.FStartColorCodeStr = "#_#";
            console.FStopColorCodeStr = "+_*";
            console.Log("Test");

            #endregion //Propertys Test

            #region Example for stepped text

            console.Write("Starting Process.. ".cGreen(), true); //Calling Write with Timestamp
            Thread.Sleep(1000);
            console.Write(" ... ");
            Thread.Sleep(1000);
            console.WriteLine(" ..Failed!".cRed());

            #endregion //Example for stepped text end

            #region Gta-Network Specific

            console.WriteLine("~r~ Gta-Network support not enabled!");
            console.FIncludeGTANetworkColors = true;
            console.WriteLine("~r~ Gta-Network support enabled now!");
            console.WriteLine("~r~ = Red " +
                            "~b~ = Blue " +
                            "~g~ = Green " +
                            "~y~ = Yellow " +
                            "~p~ = Purple " +
                            "~o~ = Orange " +
                            "~c~ = Grey " +
                            "~m~ = Darker Grey " +
                            "~u~ = Black " +
                            "~n~ = New Line " +
                            "~s~ = Default White " +
                            "~w~ = White " +
                            "~h~ = Bold Text");
            #endregion //Gta-Network Specific

            //Pause here
            Console.ReadKey();
        }
    }
}

```
