# MY OWN LANGUAGE

## Main Library:
Every line of the code file represents a different command to execute.\
The commands and the parameters are separated by a space, if one of the parameters have spaces (like arithmetic operations), you must need the **"|"** character to separate the parameters.\
The Main commands are the following ones: **[]** Indicates a parameter is optional.\
`var <name> <value>` Declares a new variable than can be used during the runtime.\
`print <values>` Prints the specified values in the console window.\
`printl <values>` Prints the specified values in the console window and a **\n** at the end.\
`pause [<message>]` Pauses the the app execution and waits for any user key input.\
`exit [<exit_code>]` Exit of the app.\
`reassign <var_name> <new_value>` Reassign the value of a specified variable with a new value.\
`import <library>` Imports the specified library into the runtime.\
`::<name>` Defines a new label during the runtime.\
`goto <label_name>` Jumps the code execution where the specified label is located if it exists.\
`If <conditions>` Define a code block that will only be executed if the conditions are met.\
`EndIf` Defines where the "If" code block ends.\
`Func <func_name>` Defines a code block that only can be executed with the "call" instruction.\
`EndFunc` Defines where the "Func" code block ends.\
`call` Calls a Func block if it exists.
### IMPORT COMMAND
Use `import <library>` to import a library into the app execution in order to use functions of the specified library. If you don't import them, you won't be able to use their functions.
### LABELS
Use `::<name>` (WITHOUT SPACES) in order to create a **label** than can be used to jump directly to the line where its written using the `goto <label_name>` command.
### IF BLOCKS
Use `If <conditions>` in order to define an If block, than only will be executed when the conditions are met. These conditions must be written with `<value> <operator> <value>`. An example of this it's `10 == 12` or `<var_name> != 10 + 2`. The number of conditions its undefined, you can write as much as you want. To separate every condition you can use `and` or `or`.\
To define where the If block ends write `EndIf` at the end of the block.
### FUNCTIONS
Use `Func <name>` to define a new function block, and `EndFunc` to define where it ends. These blocks ONLY will be executed when called by the `call <func_name>` command.\
**TODO:** Add parameters for the functions.
