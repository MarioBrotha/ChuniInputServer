using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using WindowsInput;


public class ChuniInputServer
{
    static InputSimulator sim = new InputSimulator();
    static char[] keyboardArray = new char[18];



    public static void StartListening()
    {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[6];

        // Create a TCP/IP socket.  
        /*Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);*/

        TcpListener tcpListener = new TcpListener(ipAddress, 7800);
        tcpListener.Start();
        StreamReader streamReader;
        NetworkStream networkStream;

        //testing
        /*int totalBytesRead = 0;
        int bytesRead = 0;
        byte[] buffer = new byte[62];*/

        Console.WriteLine("The Server has started on port 7800");
        Socket serverSocket = tcpListener.AcceptSocket();

        // Bind the socket to the local endpoint and listen for incoming connections.  ij
        try
        {
            while (true)
            {
                networkStream = new NetworkStream(serverSocket);

                streamReader = new StreamReader(networkStream);

                char[] newBuffer = new char[18];

                while (true)
                {
                    //grabbing a chunk of 33 chars
                    char[] buffer = new char[37];
                    int n = streamReader.ReadBlock(buffer, 0, 37);
                    
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if (buffer[i] == '\n')
                        {
                            //after a newline, pass the next 18 chars as a valid piano input, then call keypress function
                            try 
                            {
                                Array.Copy(buffer, i + 1, newBuffer, 0, 18);
                                /*if (System.Diagnostics.Debugger.IsAttached)
                                    Console.WriteLine(newBuffer);*/
                            }
                            catch (Exception e)
                            { 
                                Console.WriteLine(e.ToString());
                            }
                            
                            if (!Enumerable.SequenceEqual(newBuffer,keyboardArray))
                            {
                                newBuffer.CopyTo(keyboardArray,0);
                                if (System.Diagnostics.Debugger.IsAttached)
                                    Console.WriteLine(keyboardArray);
                                UpdateKeyboard();
                            }
                            break;
                        }
                    }
                    //Copied from chuni-hands :p
                    //Thread.Sleep(1000 / 60);
                    streamReader.DiscardBufferedData();

                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static void UpdateKeyboard()
    {
        bool pressed;
        for (int i = 0; i < 18; i++)
        {
            pressed = keyboardArray[i] == '1' ? true : false;
            switch (pressed)
            {
                case true:
                {
                    switch (i)
                    {
                            case 0:
                                if (Keyboard.IsKeyUp(Key.D1))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_1);
                                break;
                            case 1:
                                if (Keyboard.IsKeyUp(Key.Q))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_Q);
                                break;
                            case 2:
                                if (Keyboard.IsKeyUp(Key.D2))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_2);
                                break;
                            case 3:
                                if (Keyboard.IsKeyUp(Key.W))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W);
                                break;
                            case 4:
                                if (Keyboard.IsKeyUp(Key.D3))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_3);
                                break;
                            case 5:
                                if (Keyboard.IsKeyUp(Key.E))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_E);
                                break;
                            case 6:
                                if (Keyboard.IsKeyUp(Key.D4))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_4);
                                break;
                            case 7:
                                if (Keyboard.IsKeyUp(Key.R))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_R);
                                break;
                            case 8:
                                if (Keyboard.IsKeyUp(Key.D5))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_5);
                                break;
                            case 9:
                                if (Keyboard.IsKeyUp(Key.T))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_T);
                                break;
                            case 10:
                                if (Keyboard.IsKeyUp(Key.D6))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_6);
                                break;
                            case 11:
                                if (Keyboard.IsKeyUp(Key.Y))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_Y);
                                break;
                            case 12:
                                if (Keyboard.IsKeyUp(Key.D7))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_7);
                                break;
                            case 13:
                                if (Keyboard.IsKeyUp(Key.U))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_U);
                                break;
                            case 14:
                                if (Keyboard.IsKeyUp(Key.D8))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_8);
                                break;
                            case 15:
                                if (Keyboard.IsKeyUp(Key.I))
                                    sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_I);
                                break;
                    }
                }
                    break;
                case false:
                {
                    switch (i)
                    {
                            case 0:
                                if (Keyboard.IsKeyDown(Key.D1))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_1);
                                break;
                            case 1:
                                if (Keyboard.IsKeyDown(Key.Q))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_Q);
                                break;
                            case 2:
                                if (Keyboard.IsKeyDown(Key.D2))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_2);
                                break;
                            case 3:
                                if (Keyboard.IsKeyDown(Key.W))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_W);
                                break;
                            case 4:
                                if (Keyboard.IsKeyDown(Key.D3))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_3);
                                break;
                            case 5:
                                if (Keyboard.IsKeyDown(Key.E))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_E);
                                break;
                            case 6:
                                if (Keyboard.IsKeyDown(Key.D4))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_4);
                                break;
                            case 7:
                                if (Keyboard.IsKeyDown(Key.R))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_R);
                                break;
                            case 8:
                                if (Keyboard.IsKeyDown(Key.D5))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_5);
                                break;
                            case 9:
                                if (Keyboard.IsKeyDown(Key.T))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_T);
                                break;
                            case 10:
                                if (Keyboard.IsKeyDown(Key.D6))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_6);
                                break;
                            case 11:
                                if (Keyboard.IsKeyDown(Key.Y))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_Y);
                                break;
                            case 12:
                                if (Keyboard.IsKeyDown(Key.D7))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_7);
                                break;
                            case 13:
                                if (Keyboard.IsKeyDown(Key.U))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_U);
                                break;
                            case 14:
                                if (Keyboard.IsKeyDown(Key.D8))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_8);
                                break;
                            case 15:
                                if (Keyboard.IsKeyDown(Key.I))
                                    sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_I);
                                break;
                    }
                }
                break;
            }
        }
    }


    /// <summary>
    /// idk if this is faster
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    [STAThread]
    public static void KeyboardInput3(object s, DoWorkEventArgs e)
    {
        if (keyboardArray[0] == '1' && Keyboard.IsKeyUp(Key.D1))
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_1);
        else if (Keyboard.IsKeyDown(Key.D1) && keyboardArray[0] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_1);

        if (keyboardArray[1] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_Q);
        else if (Keyboard.IsKeyDown(Key.Q) && keyboardArray[1] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_Q);

        if (keyboardArray[2] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_2);
        else if (Keyboard.IsKeyDown(Key.D2) && keyboardArray[2] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_2);

        if (keyboardArray[3] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W);
        else if (Keyboard.IsKeyDown(Key.W) && keyboardArray[3] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_W);

        if (keyboardArray[4] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_3);
        else if (Keyboard.IsKeyDown(Key.D3) && keyboardArray[4] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_3);

        if (keyboardArray[5] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_E);
        else if (Keyboard.IsKeyDown(Key.E) && keyboardArray[5] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_E);

        if (keyboardArray[6] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_4);
        else if (Keyboard.IsKeyDown(Key.D4) && keyboardArray[6] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_4);

        if (keyboardArray[7] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_R);
        else if (Keyboard.IsKeyDown(Key.R) && keyboardArray[7] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_R);

        if (keyboardArray[8] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_5);
        else if (Keyboard.IsKeyDown(Key.D5) && keyboardArray[8] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_5);

        if (keyboardArray[9] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_T);
        else if (Keyboard.IsKeyDown(Key.T) && keyboardArray[9] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_T);

        if (keyboardArray[10] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_6);
        else if (Keyboard.IsKeyDown(Key.D6) && keyboardArray[10] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_6);

        if (keyboardArray[11] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_Y);
        else if (Keyboard.IsKeyDown(Key.Y) && keyboardArray[11] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_Y);

        if (keyboardArray[12] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_7);
        else if (Keyboard.IsKeyDown(Key.D7) && keyboardArray[12] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_7);

        if (keyboardArray[13] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_U);
        else if (Keyboard.IsKeyDown(Key.U) && keyboardArray[13] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_U);

        if (keyboardArray[14] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_8);
        else if (Keyboard.IsKeyDown(Key.D8) && keyboardArray[14] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_8);

        if (keyboardArray[15] == '1')
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_I);
        else if (Keyboard.IsKeyDown(Key.I) && keyboardArray[15] == '0')
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_I);

        if (keyboardArray[16] == '1')
        {
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.OEM_2);
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.OEM_PERIOD);
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.OEM_7);
        }
        else if (Keyboard.IsKeyDown(Key.OemPeriod) && keyboardArray[16] == '0')
        {
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.OEM_2);
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.OEM_PERIOD);
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.OEM_7);
        }

        if (keyboardArray[17] == '1')
        {
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.OEM_2);
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.OEM_PERIOD);
            sim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.OEM_7);
        }
        else if (Keyboard.IsKeyDown(Key.Oem1) && keyboardArray[17] == '0')
        {
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.OEM_2);
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.OEM_PERIOD);
            sim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.OEM_7);
        }

        //Thread.Sleep(TimeSpan.FromMilliseconds(1000 / 60));
    }

    [STAThread]
    public static int Main(String[] args)
    {
        StartListening();
        return 0;
    }
}
