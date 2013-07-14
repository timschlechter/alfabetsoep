using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Alfabetsoep {
	class Program {

		static void Main(string[] args) {
			var words = File.ReadAllLines(@"words.txt");
			var alphabet = CreateAlphabetUsingComparer(words);
			
			var matchingWords = GetWordsWithAscendingChars(alphabet, words);
			
			Console.WriteLine(alphabet + ": " + matchingWords.Count());
			Console.ReadKey();
		}

		private static IEnumerable<string> GetWordsWithAscendingChars(string alphabet, IEnumerable<string> words) {
			return words.Where(word => Enumerable.SequenceEqual(
													word.Select(chr => alphabet.IndexOf(chr)),
													word.Select(chr => alphabet.IndexOf(chr))
														.OrderBy(value => value)));
		}

		private static string CreateAlphabetUsingComparer(IEnumerable<string> words) {
			var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

			Array.Sort(alphabet, (char1, char2) => {
				var char1AndChar2 = words.Where(word => word.Contains(char1) && word.Contains(char2));
				var char1BeforeChar2 = char1AndChar2.Where(word => word.IndexOf(char1) < word.IndexOf(char2));

				return char1AndChar2.Count().CompareTo(char1BeforeChar2.Count() * 2);
			});

			return String.Join("", alphabet);
		}		
	}
}
