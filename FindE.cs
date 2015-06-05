using System;
using System.IO;

//initialize GridPoint Class
class FindE
{
public static bool isCoprime(ulong n1, ulong n2)
{
 ulong v1=n1;
 ulong v2=n2;
 ulong remainder;
    while (v1%v2 != 0)
    {
    remainder = v1%v2;
      v1= v2;
      v2= remainder;
      
    }
    if (v2 == 1)
    {
    return true;   
}
else
{
return false;
}    
}
static void Main()
{
Random rnd = new Random();
  ulong e = (ulong)rnd.Next(1048579, 1073741824); // creates a number between 1 and 12


while (!isCoprime(e,(ulong)(65141-1)*(46723-1) ))
{
e = (ulong)rnd.Next(1048579, 1073741824); // creates a number between 1 and 12
}

Console.WriteLine(e);




}


}
