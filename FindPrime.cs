using System;
using System.IO;

//initialize GridPoint Class
class GenerateRandom
{
public static bool isPrime(int number)
{
 
    for (int i = 2; i < number; i++)
    {
      if (number % i == 0 && i != number)
        return false;
    }
    return true;     
}
static void Main()
{
Random rnd = new Random();
 int odd = rnd.Next(46340, 65536); // creates a number between 1 and 12


while (odd%2 ==0 )
{
odd = rnd.Next(46340, 65536); // creates a number between 1 and 12
}
if (isPrime(odd))
{
Console.WriteLine(odd);

}

}


}
