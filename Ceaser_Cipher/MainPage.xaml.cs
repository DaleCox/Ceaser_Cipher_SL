using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Ceaser_Cipher
{
	public partial class MainPage : UserControl
	{
		private const String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private String key ="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private String orgCipher="";
		private String decodedCipher="";
		private String answer="";
		
		public MainPage()
		{
			// Required to initialize variables
			InitializeComponent();
		}

		private void NewCypher(object sender, System.Windows.RoutedEventArgs e)
		{
			reset();
			stringCypher();
		}
		
		private void reset(){
			key ="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			orgCypherText.Text = "";
			userCypherText.Text = "";
			orgCipher="";
			decodedCipher="";
			answer="";
		}
		
		private void stringCypher(){
			Random rand = new Random();
            int startIndex = rand.Next(0, 26);
            int numShift=rand.Next(1, 26);
            int direction = rand.Next(100);
            int endIndex;
            
			if(direction > 50){
                endIndex = startIndex - numShift;
                if(endIndex < 0)
                    endIndex = 26 + endIndex; //Adding negitive number
			}
            else{
                endIndex = startIndex + numShift;
                if(endIndex >25)
                    endIndex = endIndex - 26;
            }
                     
            encryptString(startIndex, endIndex);
        }
		
		private void encryptString(int startIndex, int endIndex){
                     char startChar = alphabet[startIndex];
                     char endChar = alphabet[endIndex];
                     
                     
                     //create Key                     
                     int shiftIndex = endIndex;
                     char[] keyChar = key.ToCharArray();
                     for(int i=0; i<keyChar.Length; ++i){
                           if(shiftIndex >= alphabet.Length)
                                  shiftIndex = 0;
                           keyChar[i] = alphabet[shiftIndex];
                           ++shiftIndex;
                     }                    
                     key = new String(keyChar);
                     
                     answer = getTarget();
                     answer = answer.ToUpper();//temp workaround for case sensitivity
                     char[] targetChar = answer.ToCharArray();                                         
                     char[] encryptTargetChar = targetChar;
                     
                     //uppercase case
                     for(int i=0; i< targetChar.Length; ++i){
                           int index = alphabet.IndexOf(targetChar[i]);
                           if(index > -1)//only update if a match is found
                                  encryptTargetChar[i]= key[index];                      
                     }
                     
                     orgCipher = new String(encryptTargetChar);
                     decodedCipher = orgCipher;
                     
                     //orgCypherText.Text = alphabet+ "\r\n" + key+ "\r\n"+ "\r\n" +answer;
                     orgCypherText.Text = orgCipher;
                     
                     userCypherText.Text = decodedCipher;
        }

		
		
		private string getTarget(){
			string target = "";
                     
            Random rand = new Random();
                     
            int caseSwitch = rand.Next(1,14);
            switch (caseSwitch)
            {
                case 1: 
                        target = "Laying Plans explores the five fundamental factors that define a successful outcome (the Way, seasons, terrain, leadership, and management)." 
                                    +"By thinking, assessing and comparing these points you can calculate a victory," 
                                    + "deviation from them will ensure failure. Remember that war is a very grave matter of state.";
                        break;
                case 2:
                        target = "Waging War explains how to understand the economy of war and how success requires making the winning play, "
                                    +"which in turn, requires limiting the cost of competition and conflict.";
                        break;					
				case 3:
						target = "Attack by Stratagem defines the source of strength as unity, not size, and the five ingredients that you need to succeed in any war.";
						break;
				case 4:	target = "Tactical Dispositions explains the importance of defending existing positions until you can advance them and how you must recognize opportunities, not try to create them.";
						break;
				case 5:	target = "Energy explains the use of creativity and timing in building your momentum.";
						 break;
				case 6:	target = "Weak Points & Strong explains how your opportunities come from the openings in the environment caused by the relative weakness of your enemy in a given area.";
						break;
				case 7:	target = "Maneuvering explains the dangers of direct conflict and how to win those confrontations when they are forced upon you."; 
						break;
				case 8:	target = "Variation in Tactics focuses on the need for flexibility in your responses. It explains how to respond to shifting circumstances successfully.";
						break;
				case 9:	target = "The Army on the March describes the different situations in which you find yourselves as you move into new enemy territories and how to"
									+"respond to them. Much of it focuses on evaluating the intentions of others.";
						break;
				case 10: target = "Terrain looks at the three general areas of resistance (distance, dangers, and barriers) and the six types of ground positions that arise"
									+"from them. Each of these six field positions offer certain advantages and disadvantages.";
						break;
				case 11: target = "The Nine Situations describe nine common situations (or stages) in a campaign, from scattering to deadly, and the specific focus you need"
									+" to successfully navigate each of them."; 
						break;
				case 12: target = "The Attack by Fire explains the use of weapons generally and the use of the environment as a weapon specifically. It examines the five targets"
									+" for attack, the five types of environmental attack, and the appropriate responses to such attack."; 
						break;
				case 13: target = "The Use of Spies focuses on the importance of developing good information sources, specifically the five types of sources and how to manage"
									+" them.";
						break;					
                default:
                        target = "http://en.wikipedia.org/wiki/The_Art_of_War";
                        break;
            }
                     
            return target;
		}
		
		private void decode(int index, string selctedValue){				
				//Scan origonal ecoding for positions then replace those positions in the decoeded value
				List<int> offsetsMatchFound = new List<int>();
				char[] orgCiChar = orgCipher.ToCharArray();
			for(int i=0; i<orgCiChar.Length;++i){
					if(orgCiChar[i].Equals(alphabet[index])){						
						offsetsMatchFound.Add(i);
					}
			}
			
			char[] decCiChar = decodedCipher.ToCharArray();
			int[] offsetsMatchValues = offsetsMatchFound.ToArray();
			for(int i=0; i<offsetsMatchValues.Length; ++i){
				decCiChar[offsetsMatchValues[i]] = selctedValue[0];
			}

				//decodedCipher = decodedCipher.Replace(selctedValue, alphabet[index].ToString());
				decodedCipher = new String(decCiChar);
				userCypherText.Text = decodedCipher;
		}

		private void displayAnswer(object sender, System.Windows.RoutedEventArgs e)
		{
			userCypherText.Text = answer;
		}

		/// <summary>
		/// Event handler to recieve the slection from the combo boxes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void selectIonChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if(userCypherText.Text.Length>0){
			ComboBox cb = (ComboBox) sender;
			int senderIndex = int.Parse(cb.Tag.ToString());
			
			ComboBoxItem cbValue = (ComboBoxItem) cb.SelectedItem;
			string cbContent = cbValue.Content.ToString();
			decode(senderIndex, cbContent);}
			else{
				userCypherText.Text = "Please Use New Button to start game";//repalce with message box
			}
		}

		private void resetCipher(object sender, System.Windows.RoutedEventArgs e)
		{
			userCypherText.Text = orgCipher;
		}
		
	}
}