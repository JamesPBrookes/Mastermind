using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastermind
{
    class Program
    {
        static void Main()
        {
            StartNewGame(); //Call function to start the game
        }
            static void StartNewGame() //Runs the game
            {
                Console.Clear();//Print welcome message
                Console.WriteLine("Welcome to Mastermind AI Simulation");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine();

                Console.WriteLine("Please enter the number of Pegs: ");//Capture the number of pegs the user chooses to play with
                int iPegs = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter the Maximum Number of Guesses: ");//Capture the maximum number of turns/guesses the user chooses
                int iMaxGuesses = Convert.ToInt32(Console.ReadLine());

                Game MasterMind = new Game(iPegs, iMaxGuesses);//Create a new Game object

                MasterMind.GenerateSecretCode(iPegs);//Generate the secret code

                /*Console.Write("Secret Code is:");//Display the secret code (for debug only)
                for (int i = 0; i < iPegs; i++)
                {
                    Console.Write(" {0}", MasterMind.secretCode[i]);
                }
                Console.WriteLine();*/

                int[] results;//Integer array to hold the number of black and white pegs for each guess
                Board gameBoard = new Board(iPegs, iMaxGuesses);//Create a new Game Board object to display the results

                //Loop to generate guesses and evaluate the results until the secret code is guessed or number of turns runs out
                while (!MasterMind.bSolved && (MasterMind.iTurnsTaken < MasterMind.iMaxGuesses))
                {
                    MasterMind.MakeNextGuess();//Guess the secret code
                    results = MasterMind.currentGuess.Match(MasterMind.secretCode);//Evaluate the guess and store the number of black and white pegs in the result array
                    MasterMind.ReducePossibilities(results[0], results[1]);//Remove all codes that do not match the results from the list of all possible codes

                    gameBoard.DrawBoard(MasterMind.currentGuess, results, MasterMind.iTurnsTaken);//Display the board with all guesses and results so far

                    if (results[0] == MasterMind.iPegs)//If result is all black pegs, the game is solved
                    {
                        Console.WriteLine("Game Solved in {0} turns. Do you want to play again? (Y/N)", MasterMind.iTurnsTaken);
                    }
                    else//Keep guessing
                    {
                        Console.WriteLine("Press any key to make the next guess");
                        Console.ReadLine();
                    }
                }

                if (!MasterMind.bSolved)//Game failed to solve in the number of guesses allowed
                {
                    Console.WriteLine("You ran out of turns. Do you want to play again? (Y/N)");
                }
                
                string response = Console.ReadLine();

                if (response=="Y" || response=="y")
                {
                    StartNewGame();//Start a new game
                }
                else
                {
                    Console.WriteLine("Thanks for playing.");
                    Console.ReadLine();
                    return;//End game
                }
            }
    }
}
