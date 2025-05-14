using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;
using System.IO;

namespace ChatbotPart2
{
    internal class Program
    {
        static string userName = "";
        static string userMood = "";
        static List<string> memory = new List<string>();
        static Random random = new Random();

        static void Main(string[] args)
        {
            DisplayLogo();
            DisplayVoiceGreeting();
            DisplayGreetUser();
            StartConversation();
        }

        static void DisplayLogo()
        {
            string logo = @"
  ________            __  __                   ________          __  __          __ 
 /_  __/ /_  ___     / / / /_  __________     / ____/ /_  ____ _/ /_/ /_  ____  / /_
  / / / __ \/ _ \   / /_/ / / / /_  /_  /    / /   / __ \/ __ `/ __/ __ \/ __ \/ __/
 / / / / / /  __/  / __  / /_/ / / /_/ /_   / /___/ / / / /_/ / /_/ /_/ / /_/ / /_  
/_/ /_/ /_/\___/  /_/ /_/\__,_/ /___/___/   \____/_/ /_/\__,_/\__/_.___/\____/\__/  
                                                                                    ";
            Console.WriteLine(logo);
        }

        static void DisplayVoiceGreeting()
        {
            try
            {
                if (File.Exists("welcome.wav"))
                {
                    using (SoundPlayer soundPlayer = new SoundPlayer("welcome.wav"))
                    {
                        soundPlayer.PlaySync();
                    }
                }
                else
                {
                    Console.WriteLine("Audio file 'welcome.wav' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Audio cannot be played. " + ex.Message);
            }
        }

        static void DisplayGreetUser()
        {
            Console.Write("What is your name? ");
            userName = Console.ReadLine();
            TypingDelay($"\nHello, {userName}! Welcome to The Huzz Chatbot, where you can be informed about the internet.");

            Console.Write("How are you feeling today? ");
            userMood = Console.ReadLine().ToLower();
            RespondToMood(userMood);
        }

        static void RespondToMood(string mood)
        {
            if (mood.Contains("happy") || mood.Contains("good") || mood.Contains("great"))
            {
                TypingDelay("I'm glad to hear you're doing well! Here's a quick tip: Always update your software to patch vulnerabilities.");
            }
            else if (mood.Contains("sad") || mood.Contains("down") || mood.Contains("bad"))
            {
                TypingDelay("I'm sorry to hear that. Remember, securing your online presence can give you peace of mind.");
            }
            else if (mood.Contains("angry") || mood.Contains("frustrated") || mood.Contains("mad"))
            {
                TypingDelay("Frustration is valid. Let's make sure digital threats aren't part of it. Use a firewall to keep intruders out.");
            }
            
            {
                TypingDelay("Thanks for sharing. I'm here to help however you're feeling.");
            }
        }

        static void StartConversation()
        {
            while (true)
            {
                Console.Write($"\n{userName}, how can I help you? ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(input)) continue;
                memory.Add(input);

                if (input == "exit")
                {
                    TypingDelay("Goodbye! Stay safe online.");
                    break;
                }

                if (input.Contains("password"))
                {
                    TypingDelay("Use a password manager and avoid reusing passwords across sites.\n A strong password should be at least 12 characters long and include uppercase and lowercase letters, numbers, and special characters");
                }
                else if (input.Contains("scam") || input.Contains("phishing"))
                {
                    TypingDelay("Be cautious of unsolicited emails asking for personal info. Always verify sources.");
                }
                else if (input.Contains("privacy"))
                {
                    TypingDelay("Limit what you share online. Adjust your social media privacy settings.");
                }
                else if (input.Contains("cybersecurity"))
                {
                    TypingDelay("Cybersecurity involves practices that protect your devices and data from threats.");
                }
                else if (input.Contains("help"))
                {
                    TypingDelay("You can ask me about:");
                    TypingDelay("- Password safety\n- Scam or phishing protection\n- Online privacy\n- Cybersecurity basics");
                }
                else if (input.Contains("remember") || input.Contains("what did i say"))
                {
                    TypingDelay("Here's what you've asked so far:");
                    foreach (var mem in memory)
                    {
                        TypingDelay("- " + mem);
                    }
                }
                else if (input.Contains("suggestion") || input.Contains("tip") || input.Contains("advice"))
                {
                    GiveRandomTip();
                }
                else
                {
                    TypingDelay("I'm not sure how to respond to that. Try asking about password safety, scams, or online privacy.");
                    GiveRandomTip();
                }
            }
        }

        static void GiveRandomTip()
        {
            string[] tips = {
                "Never use the same password on multiple sites.",
                "Enable two-factor authentication wherever possible.",
                "Update your software regularly to stay protected.",
                "Be careful what you download and from where.",
                "Think before you click—especially on suspicious links.",
                "Don't overshare personal information on social media.",
                "Use a VPN on public Wi-Fi for safer browsing."
            };

            string tip = tips[random.Next(tips.Length)];
            TypingDelay("Cyber Tip: " + tip);
        }

        static void TypingDelay(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
        }
    }
}
