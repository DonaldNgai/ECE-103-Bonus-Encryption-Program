using System;
using System.IO;
using System.Threading;

//65141 46723 p,q  old: 38321
//3043582943 n old: 2496268261
//539813707 e old: 960175367   539813707 
//4117380043 d old: 4857925303   4117380043
//initialize Bonus Class
class Bonus
{

//split chosen string into bytes of size 4
	static void SplitStringsByLength(int len, string message, string [] seperate)
	{
		int counter = 0;
		while (!String.IsNullOrEmpty(message))
		{

			try
			{
				seperate[counter] = message.Substring(0, len);
				message = message.Remove(0, len);
				counter ++;
			}
			catch 
			{
				while (message.Length !=4)
				{
					message = message+" ";
				}
				seperate[counter] = message;
				break;
			}
		}
	}
	
	//built in function to find a prime between two numbers
	public static int findPrime()
	{
		Random rnd = new Random();
		int odd = rnd.Next(32768, 65536); // creates a number between 2^15 and 2^16
		bool isPrime = false;

		while (!isPrime)
		{
			isPrime = true;
			while (odd%2 ==0 )
			{
				odd = rnd.Next(32768, 65536); // creates a number between 2^15 and 2^16
			}

			for (int i = 2; i < odd; i++)
			{
				if (odd % i == 0 && i != odd)
				isPrime = false;
			}
			
		}	

		return odd;

	}
	
	//built in function to find an e between a given p and q
	public ulong findE(){
		Random rnd = new Random();
		ulong e = (ulong)rnd.Next(1048579, 1073741824); // creates a number between 2^20 and 2^30
		ulong phi = (ulong)(65141-1)*(46723-1);
		ulong remainder;
		bool isCoprime = false;


		while (!isCoprime)
		{
			e = (ulong)rnd.Next(1048579, 1073741824); // creates a number between 2^20 and 2^30

			ulong v1=e;
			ulong v2=phi;
			while (v1%v2 != 0)
			{
				remainder = v1%v2;
				v1= v2;
				v2= remainder;
				
			}
			if (v2 == 1)
			{
				isCoprime = true;   
			}
		}
		return e;
	}
	
	//convert a size 4 byte string into an Ascii product between them
	static ulong ConvertToAscii(string messages)
	{
		return (ulong)((int)messages[0]*Math.Pow(256, 3) + 
		(int)messages[1]*Math.Pow(256, 2) + 
		(int)messages[2] * 256 + (int)messages[3]);
	}
	
	//function to find the necessary squares of a specific 4 byte string and store it in an array of ulong's
	static ulong[] FindSquares(ulong message, ulong d, ulong n)
	{
		string binary = Convert.ToString((long)d, 2);
		ulong [] squares = new ulong [binary.Length];
		squares[0] = message;

		for (int i = 1; i < binary.Length ; i++)
		{
			squares [i] = (squares[i-1]*squares[i-1])%n;
		}
		return squares;
	}
	
	//encrypt a message using repeated squaring from the function FindSquares
	static ulong Encrypt(ulong message, ulong d, ulong n)
	{
		ulong cipher = 1;
		string binary = Convert.ToString((long)d, 2);
		int counter =0;
		ulong [] squares = new ulong [binary.Length];
		squares = FindSquares(message,d,n);

//if the binary representation of the message contains a 1 at the end, find the remainder of the corresponding square with the variable n
//if not, remove the last zero and check again until all bits in the binary representation is checked
		while (!String.IsNullOrEmpty(binary))
		{
			if (binary.Substring(binary.Length-1, 1) == "1")
			{
				cipher = (cipher * squares[counter])% n;
			}
			counter ++;
			binary = binary.Remove(binary.Length-1, 1);
		}

		return cipher;
	}
	
	//function to output and save a file with the encrypted messages
	static void WriteCipher( StreamWriter outStream , ulong [] ciphers,ulong n,int splits)
	{
		outStream.Write( n + " " );
		outStream.Write( "539813707" + " " );
		outStream.Write( splits + " " );
		for ( int i = 0 ; i < ciphers.Length ; i ++ )
		{
			outStream.Write( ciphers[i] + " " );
		}
		//close outstream 
		outStream.Close( );
	}

	
	static void Main()
	{
	//main method
	//Select message to encrpye
		string text = "Hello D.E.A.R. I am student 20509579. This is my little secret : 65141 and 46723. Leggo My Eggo. ";
		text += "Fear is not real. The only place that fear can exist is in our thoughts of the future. It is a product of our imagination, ";
		text += "causing us to fear things that do not at present and may not ever exist. That is near insanity. ";
		text += "Do not misunderstand me danger is very real but fear is a choice. Will Smith. ";
		text += "Now I am in the place I call this wide wide Heaven because it includes all my simplest desires but also the most humble and grand. The word my grandfather uses is comfort. ";
		text+= "So there are cakes and pillows and colors galore, but underneath this more obvious patchwork quilt are places like a quiet room where you can go and hold someone's hand and not have to say anything. ";
		text+= "Give no story. Make no claim. Where you can live at the edge of your skin for as long as you wish. Alice Sebold"; 
		
		//decide how big the arrays of the program should be, based on the number of splits when splitting message into 4 byte pieces
		int numbersplits = text.Length /4;
		if (text.Length %4 != 0)
		{
			numbersplits++;
		}

		//initialize variables
		string [ ] splitbytes = new string [numbersplits];
		ulong [] ms = new ulong[numbersplits];
		ulong [] encrypted = new ulong[numbersplits];
		ulong [] xavierencrypted = new ulong[numbersplits];

		SplitStringsByLength(4,text,splitbytes);
		Console.WriteLine(numbersplits);

		//Console.WriteLine(Encrypt(1214606444,4117380043,3043582943));
		// square = FindSquares(539, 1633, 7031);
		//encrype data by calling functions
		for (int i = 0; i < numbersplits ; i++)
		{
			ms[i]=ConvertToAscii(splitbytes[i]);
			encrypted[i] = Encrypt(ms[i],4117380043,3043582943);
			xavierencrypted[i] = Encrypt(encrypted[i],52741219,3125033603);
			Console.Write(ms[i] + " ");
			Console.Write("'" + splitbytes[i] );
			Console.Write(" Cipher: " + encrypted[i] );
			Console.WriteLine(" Xavier: " + xavierencrypted[i] );

		}

		//create new file with corrected elevations
		StreamWriter outStream = new StreamWriter( "QuotesEncrypt.txt" );
		WriteCipher( outStream , xavierencrypted,3043582943,numbersplits);
		// Console.WriteLine(ms[0]%3043582943);
		//Console.WriteLine(Encrypt(ms[0],4117380043,3043582943));
	}
}