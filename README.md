# String Calculator - TDD Test

_This should take you no more than 30 minutes. When you've finished create a pull request (PR) to "tests" branch to submit your code for review. Do not make any PR to Master branch._

Make sure you only test for **correct inputs**. there is no need to test for invalid inputs for this kata

1. Create a simple String calculator with a method **int Add(string numbers)**
  1. The method can take 0, 1 or 2 numbers, and will return their sum (for an empty string it will return 0) for example `“”` or **`“1”`** or **`“1,2”`**
  2. Start with the simplest test case of an empty string and move to one and two numbers
  3. Remember to solve things as simply as possible so that you force yourself to write tests you did not think about
  4. Remember to refactor after each passing test **and commit**
2. Allow the Add method to handle an unknown amount of numbers
3. Allow the Add method to handle new lines between numbers (instead of commas).
  1. The following input is ok:  `“1\n2,3”`  (will equal 6)
  2. The following input is NOT ok:  `“1,\n”` (no need to prove it - just clarifying)
4. Support different delimiters
  1. To change a delimiter, the beginning of the string will contain a separate line that looks like this:  
  `“//[delimiter]\n[numbers…]”` for example `“//;\n1;2”` should return 3 where the default delimiter is `‘;’`.
  2. The first line is optional. All existing scenarios should still be supported
5. Calling `Add` with a negative number will throw an exception `“negatives not allowed”` - and the negative that was passed. If there are multiple negatives, show all of them in the exception message 
6. Numbers bigger than 1000 should be ignored, so adding 2 + 1001  = 2
7. Delimiters can be of any length with the following format:  `“//[delimiter]\n” for example: “//[***]\n1***2***3”` should return 6
8. Allow multiple delimiters like this:  `“//[delim1][delim2]\n” for example “//[*][%]\n1*2%3”` should return 6.
9. Make sure you can also handle multiple delimiters with length longer than one char
